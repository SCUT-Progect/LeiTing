using ScutDemo.Lang;
using ScutDemo.Model.ConfigModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Game.Contract;

namespace ScutDemo.CsScript.Action
{
    /// <summary>
    /// 头像修改
    /// </summary>
    class Action1009 : BaseAction
    {
        private string _headId;

        public Action1009(HttpGet httpGet)
            : base(ActionIDDefine.Cst_Action1009, httpGet)
        {

        }

        public override void BuildPacket()
        {
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetString("HeadId", ref _headId))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            _headId = ConfigEnvSet.GetString(_headId);
            if(_headId!=string.Empty)
            {
                ContextUser.ModifyLocked(() =>
                {
                    ContextUser.HeadId = _headId;
                });
                ErrorCode = 1;
                ErrorInfo = LanguageManager.GetLang().St1009_ChangeUserHeadSuccess;
                return true;
            }
            ErrorCode = LanguageManager.GetLang().ErrorCode;
            ErrorInfo = LanguageManager.GetLang().St10006_DoesNotExistTheGeneral;
            return false;              
        }
    }
}
