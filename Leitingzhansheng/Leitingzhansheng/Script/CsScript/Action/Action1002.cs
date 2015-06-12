using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.Game.Sns;
using ScutDemo.Lang;
using ZyGames.Framework.Cache.Generic;
using ScutDemo.Model.DataModel;
using ScutDemo.Model.Enum;
using ScutDemo.Model.Config;
using ScutDemo.Model.ConfigModel;
using System.Text.RegularExpressions;

namespace ScutDemo.CsScript.Action
{
    public class Action1002 : BaseStruct
    {
        private string passportId = string.Empty;
        private string password = string.Empty;
        private int uid = 0;
        //private string deviceID = string.Empty;
        //private int mobileType = 0;
        //private int gameType = 0;
        //private string retailID = string.Empty;
        //private string clientAppVersion = string.Empty;
        //private int ScreenX = 0;
        //private int ScreenY = 0; 

        public Action1002(HttpGet httpGet)
            : base(ActionIDDefine.Cst_Action1002, httpGet)
        {

        }

        public override void BuildPacket()
        {
            PushIntoStack(passportId);
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetString("Pid", ref passportId)&&
                httpGet.GetString ("Pwd", ref password))
            {

            }
            else
            {
                return false;
            }
            return true;
        }

        public override bool TakeAction()
        {
            if (password.Length > 12 || password.Length < 4)
            {
                this.ErrorCode = LanguageManager.GetLang().ErrorCode;
                this.ErrorInfo = LanguageManager.GetLang().St1006_PasswordTooLong;
                return false;
            }
            Regex re = new Regex(@"^[\u4e00-\u9fa5\w]+$");
            if (!re.IsMatch(password))
            {
                ErrorCode = LanguageManager.GetLang().ErrorCode;
                ErrorInfo = LanguageManager.GetLang().St1006_PasswordExceptional;
                return false;
            }
            var cache = new ShareCacheStruct<PassportInfo>();
            uid = (int)cache.GetNextNo();
            PassportInfo passport;
            passport = cache.Find(m => (m.PassportId == passportId));
            if (passport == null)
            {
                passport = new PassportInfo() { PassportId = passportId, Password = password, UserId = uid };
                if (!GameUser.IsNickName(passportId)&&DoSuccess(passportId, uid) == true)
                {
                    cache.Add(passport);
                    Console.WriteLine("Add passport {0} success!", passportId);
                    return true;
                }
            }
            Console.WriteLine("Add passport {0} failed!", passportId);
            this.ErrorCode = LanguageManager.GetLang().ErrorCode;
            this.ErrorInfo = LanguageManager.GetLang().St1002_GetRegisterPassportIDError;
            return false;
         }

        protected bool DoSuccess(string pid, int uid)
        {
            var cache = new PersonalCacheStruct<GameUser>();
            GameUser user = cache.FindKey(uid.ToString());
            if (user != null)
            {
                Console.WriteLine("Add GameUser {0} failed!", passportId);
                return false;
            }
            else
            {
                user = new GameUser()
                {
                 /*1*/   UserId = uid.ToString(),
                 /*2*/   NickName = pid,
                 /*3*/   PassportId = pid,
                 /*4*/   RetailId = "0000",
                 /*5*/   UserLv = 1,
                 /*6*/   EnergyNum = (short)ConfigEnvSet.GetInt("User.OriginEnergyNum"),
                 /*7*/   PayGold = 0,
                 /*8*/   ExpNum = 0,
                 /*9*/   UserStatus = UserStatus.Normal,
                 /*10*/  CreateDate = DateTime.Now,
                 /*11*/  LoginTime = DateTime.MinValue,
                 /*12*/  /*UserExtend*/
                 /*13*/  HeadId = ConfigEnvSet.GetString("_head_001.png"),
                 /*14*/  IsRefreshing = false,
                 /*15*/  IsOnline = false, 
                };
                cache.Add(user);
                Console.WriteLine("Add GameUser {0} success!", passportId);
                return true;
            }
        }

    }

}
