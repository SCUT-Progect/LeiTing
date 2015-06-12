using ScutDemo.Lang;
using ScutDemo.Model.ConfigModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScutDemo.CsScript.Action
{
    /// <summary>
    /// 充值接口
    /// </summary>
    class Action1010 : BaseAction
    {
        private int _buynum = 0;

        public Action1010(ZyGames.Framework.Game.Contract.HttpGet httpGet)
            : base(ActionIDDefine.Cst_Action1010, httpGet)
        {
        }

        public override void BuildPacket()
        {
        }

        public override bool GetUrlElement()
        {
            if(httpGet.GetInt("BuyNum", ref _buynum))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            if ( ContextUser.PayGold + _buynum > ConfigEnvSet.GetInt("User.MaxPayGold"))
            {
                ErrorCode = LanguageManager.GetLang().ErrorCode;
                ErrorInfo = String.Format(LanguageManager.GetLang().Wb1001_OverflowMaxPayGold,  ConfigEnvSet.GetInt("User.MaxPayGold"));
                return false;
            }
            ContextUser.ModifyLocked(() =>
            {
                ContextUser.PayGold += _buynum;
            });
            this.ErrorCode = LanguageManager.GetLang().ErrorCode;
            this.ErrorInfo = String.Format(LanguageManager.GetLang().PaySuccessMsg,_buynum); 
            return true;
        }
    }

}
