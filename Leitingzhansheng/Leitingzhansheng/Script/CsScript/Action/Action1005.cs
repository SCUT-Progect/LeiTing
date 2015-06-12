using ScutDemo.CsScript.Action;
using ScutDemo.Lang;
using ScutDemo.Model.ConfigModel;
using ScutDemo.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using ZyGames.Framework.Game.Contract;


namespace ScutDemo.CsScript.Action
{

    /// <summary>
    /// 昵称修改
    /// </summary>
    public class Action1005 : BaseAction
    {
        private string _nickname;

        public Action1005(HttpGet httpGet)
            : base((short)ActionIDDefine.Cst_Action1005, httpGet)
        {

        }

        /// <summary>
        /// 客户端请求的参数较验
        /// </summary>
        /// <returns>false:中断后面的方式执行并返回Error</returns>
        public override bool GetUrlElement()
        {
            if (httpGet.GetString("NickName", ref _nickname))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 业务逻辑处理
        /// </summary>
        /// <returns>false:中断后面的方式执行并返回Error</returns>
        public override bool TakeAction()
        {
            ContextUser.ModifyLocked(() =>
            {
                try
                {
                    int maxLength = ConfigEnvSet.GetInt("User.MaxLength");

                    if (_nickname.Trim().Length == 0 || Encoding.Default.GetByteCount(_nickname) > maxLength)
                    {
                        ErrorCode = LanguageManager.GetLang().ErrorCode;
                        ErrorInfo = string.Format(LanguageManager.GetLang().St1005_KingNameTooLong, maxLength);
                        return;
                        //return false;
                    }

                    if (GameUser.IsNickName(_nickname))
                    {
                        ErrorCode = 1;
                        ErrorInfo = LanguageManager.GetLang().St1005_Rename;
                        return;
                        //return false;
                    }
                }
                catch (Exception ex)
                {
                    SaveLog(ex);
                    return;
                    //return false;
                }
                ContextUser.NickName = _nickname;
                ErrorCode = 1;
                ErrorInfo = LanguageManager.GetLang().St1005_ChangeNickNameSuccess;
            }
            );
            return true;
        }

        /// <summary>
        /// 下发给客户的包结构数据
        /// </summary>
        public override void BuildPacket()
        {
        }

    }
}
