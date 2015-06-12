using ScutDemo.Model.ConfigModel;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Game.Cache;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Contract.Action;
using ZyGames.Framework.Game.Service;


namespace ScutDemo.CsScript.Action
{

    /// <summary>
    /// 用户基本信息获取
    /// </summary>
    /// <remarks>继续BaseStruct类:允许无身份认证的请求;AuthorizeAction:需要身份认证的请求</remarks>
    public class Action1007 : BaseAction
    {
        private string _nickName = string.Empty;
        private int _userLv = 0;
        private string _headId = string.Empty;
        private int _goldNum = 0;
        private int _energyNum = 0;
        private int _curExpNum = 0;
        private int _nextExpNum = 0;
        private int _MaxLv = 0;


        public Action1007(HttpGet httpGet)
            : base(ActionIDDefine.Cst_Action1007, httpGet)
        {

        }

        /// <summary>
        /// 检查的Action是否需要授权访问
        /// </summary>
        protected override bool IgnoreActionId
        {
            get { return true; }
        }

        /// <summary>
        /// 客户端请求的参数较验
        /// </summary>
        /// <returns>false:中断后面的方式执行并返回Error</returns>
        public override bool GetUrlElement()
        {
            return true;
        }

        /// <summary>
        /// 业务逻辑处理
        /// </summary>
        /// <returns>false:中断后面的方式执行并返回Error</returns>
        public override bool TakeAction()
        {
            _nickName = ContextUser.NickName;
            _userLv = ContextUser.UserLv;
            _headId = ContextUser.HeadId;
            _goldNum = ContextUser.PayGold;
            _energyNum = ContextUser.EnergyNum;
            _curExpNum = ContextUser.ExpNum;
            var lvinfo = new ConfigCacheSet<UserLvInfo>().FindKey(_userLv.ToString());
            _nextExpNum = lvinfo.UpExperience;
            _MaxLv = (short)ConfigEnvSet.GetInt("User.MaxLv");
            return true;
        }

        /// <summary>
        /// 下发给客户的包结构数据
        /// </summary>
        public override void BuildPacket()
        {
            this.PushIntoStack(_nickName);
            this.PushIntoStack(_userLv);
            this.PushIntoStack(_headId);
            this.PushIntoStack(_goldNum);
            this.PushIntoStack(_energyNum);
            this.PushIntoStack(_curExpNum);
            this.PushIntoStack(_nextExpNum);
            this.PushIntoStack(_MaxLv);
        }

    }
}
