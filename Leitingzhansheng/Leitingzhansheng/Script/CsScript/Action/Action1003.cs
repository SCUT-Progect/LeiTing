using ScutDemo.Lang;
using ScutDemo.Model.DataModel;
using ScutDemo.Model.Enum;
using ScutDemo.Scrtipt.Model;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Common.Security;
using ZyGames.Framework.Game.Context;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Runtime;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.Game.Sns;
using ZyGames.Framework.Model;

namespace ScutDemo.CsScript.Action
{
    /// <summary>
    /// Login action.
    /// </summary>
    public class Action1003 : BaseStruct
    {
        /// <summary>
        /// The passport identifier.
        /// </summary>
        protected string PassportId = string.Empty;
        /// <summary>
        /// The password.
        /// </summary>
        protected string Password = string.Empty;
        /// <summary>
        /// The name of the nick.
        /// </summary>
        protected string NickName = string.Empty;
        /// <summary>
        /// The head I.
        /// </summary>
        protected string HeadID = string.Empty;
        /// <summary>
        /// The type of the user.
        /// </summary>
        protected int UserType;

        protected bool IsFirstLogin = true;

        public Action1003(HttpGet httpGet)
            : base(ActionIDDefine.Cst_Action1003, httpGet)
        {
            //LoginProxy = new LoginProxy(httpGet);
        }
        /// <summary>
        /// 创建返回协议内容输出栈
        /// </summary>
        public override void BuildPacket()
        {
            PushIntoStack(PassportId);
            PushIntoStack(Current.UserId.ToNotNullString());
            PushIntoStack(Current.SessionId);
            PushIntoStack(MathUtils.Now.ToString("yyyy-MM-dd HH:mm"));
            PushIntoStack(IsFirstLogin);
           
        }
        /// <summary>
        /// 接收用户请求的参数，并根据相应类进行检测
        /// </summary>
        /// <returns></returns>
        public override bool GetUrlElement()
        {
            if (actionGetter.GetString("Pid", ref PassportId) &&
                actionGetter.GetString("Pwd", ref Password))
            {
                return true;
            }
            return false;
        }


        ///// <summary>
        ///// 子类实现Action处理
        ///// </summary>
        ///// <returns></returns>
        public override bool TakeAction()
        {
            //ILogin login = CreateLogin();
            //login.Password = DecodePassword(login.Password);
            Sid = string.Empty;
            var login = new ShareCacheStruct<PassportInfo>();
            PassportInfo passport = login.Find(m=>(m.PassportId==PassportId));
            if (passport!=null&&passport.Password==Password)
            {
                var cacheset = new PersonalCacheStruct<GameUser>();
                IUser user = null;
                Sid = Current.SessionId;
                PassportId = passport.PassportId;
                int userId = passport.UserId;
                if ( DoSuccess(passport.UserId, out user))
                {
                    var session = GameSession.Get(Sid);
                    if (session != null)
                    {
                        session.Bind(user ?? new SessionUser() { PassportId = PassportId, UserId = userId});
                        return true;
                    }

                }
            }
            else
            {
                DoLoginFail();
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void DoLoginFail()
        {
            ErrorCode = Language.Instance.ErrorCode;
            ErrorInfo = Language.Instance.PasswordError;
            Console.WriteLine("Longin Failed");
        }

        /// <summary>
        /// Dos the success.
        /// </summary>
        /// <returns><c>true</c>, if success was done, <c>false</c> otherwise.</returns>
        /// <param name="userId">User identifier.</param>
        /// <param name="user"></param>
        protected bool DoSuccess(int userId, out IUser user)
        {
            user = null;
            //原因：重登录时，数据会回档问题
            var cacheSet = new PersonalCacheStruct<GameUser>();
            GameUser userInfo = cacheSet.FindKey(userId.ToNotNullString());
            if (userInfo != null)
            {
                //原因：还在加载中时，返回
                if (userInfo.IsRefreshing)
                {
                    ErrorCode = LanguageManager.GetLang().ErrorCode;
                    ErrorInfo = LanguageManager.GetLang().ServerLoading;
                    return false;
                }
            }

            if (userInfo == null ||
                string.IsNullOrEmpty(userInfo.SessionId) ||
                !userInfo.IsOnline)
            {
                UserCacheGlobal.Load(userId.ToNotNullString()); //重新刷缓存
                userInfo = cacheSet.FindKey(userId.ToNotNullString());
            }
            if (userInfo != null)
            {
                //if (userInfo.UserStatus == UserStatus.FengJin)
                //{
                //    ErrorCode = LanguageManager.GetLang().ErrorCode;
                //    ErrorInfo = LanguageManager.GetLang().St1004_IDDisable;
                //    return false;
                //}
                //userLoginLog.UserId = userInfo.UserID;
                //userLoginLog.SessionID = Sid;
                //userLoginLog.AddTime = DateTime.Now;
                //userLoginLog.State = LoginStatus.Logined;
                //userLoginLog.Ip = this.GetRealIP();
                //userLoginLog.Pid = userInfo.Pid;
                //userLoginLog.UserLv = userInfo.UserLv;
                //var sender = DataSyncManager.GetDataSender();
                //sender.Send(userLoginLog);
                if (userInfo.LoginTime == DateTime.MinValue || 
                    userInfo.LoginTime.CompareTo(DateTime.Today) < 0)
                {
                    //Console.WriteLine("{0} {1}", userInfo.LoginTime, DateTime.Today);
                    IsFirstLogin = true;
                }
                else
                {
                    IsFirstLogin = false;
                }
                userInfo.LoginTime = DateTime.Now;
                userInfo.SessionId = Sid;
                userInfo.IsOnline = true;
                //userInfo.VipLv = vipLv;
            }
            else
            {
                user = new SessionUser()
                {
                    UserId = userId,
                    PassportId = PassportId
                };
                ErrorCode = 1005;
                ErrorInfo = LanguageManager.GetLang().St1005_RoleCheck;
            }
            return true;
        }
    }
}
