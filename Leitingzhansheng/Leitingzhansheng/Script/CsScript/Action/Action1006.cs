using ScutDemo.CsScript.Action;
using ScutDemo.Lang;
using ScutDemo.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Contract;

namespace ScutDemo.CsScript.Action
{
    /// <summary>
    /// 用户密码更新
    /// </summary>
    class Action1006 : BaseAction
    {
        private string _password = string.Empty;

        public Action1006(HttpGet httpGet)
            :base(ActionIDDefine.Cst_Action1006, httpGet)
        {

        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetString("Pwd", ref _password))
            {
                return true;
            }
            return false;
        }

        public override void BuildPacket()
        {
        }

        public override bool TakeAction()
        {
            if(_password.Length > 12 || _password.Length < 4)
            {
                this.ErrorCode = LanguageManager.GetLang().ErrorCode;
                this.ErrorInfo = LanguageManager.GetLang().St1006_PasswordTooLong;
                return false;
            }
            Regex re = new Regex(@"^[\u4e00-\u9fa5\w]+$");
            if (!re.IsMatch(_password))
            {
                ErrorCode = LanguageManager.GetLang().ErrorCode;
                ErrorInfo = LanguageManager.GetLang().St1006_PasswordExceptional;
                return false;
            }
            var cacheset = new ShareCacheStruct<PassportInfo>();
            PassportInfo passport = cacheset.FindKey(Current.UserId);
            passport.ModifyLocked(() =>
            {
                passport.Password = _password;
            });
            this.ErrorCode = 1;
            this.ErrorInfo = LanguageManager.GetLang().St1006_ChangePasswordSuccess;
            return true;
        }

    }
}
