using System;
using System.Collections.Generic;
using System.Data;
using ProtoBuf;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Data;
using ZyGames.Framework.Game.Cache;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Context;
using ZyGames.Framework.Model;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.Cache.Generic;
using ScutDemo.Model;
using ScutDemo.Model.Enum;
using ScutDemo.Model.Config;

namespace ScutDemo.Model.DataModel
{
    [Serializable, ProtoContract, EntityTable(CacheType.Entity, DbConfig.Data)]
    public class PassportInfo:ShareEntity
    {
        public PassportInfo():base(AccessLevel.ReadWrite)
        {

        }
        private int _userId;
        [ProtoMember(1)]
        [EntityField("UserId", IsKey = true)]
        public int UserId
        {
            get 
            {
                return _userId;
            }
            set 
            {
                SetChange("UserId", value);
            }
        }
        private string _passportId;
        [ProtoMember(2)]
        [EntityField("PassportId")]
        public string PassportId
        {
            get
            {
                return _passportId;
            }
            set
            {
                SetChange("PassportId", value);
            }
        }
        private string _password;
        [ProtoMember(3)]
        [EntityField("Password")]
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                SetChange("Password", value);
            }
        }
               protected override object this[string index]
        {
            get
            {
                #region
                switch (index)
                {
                    case "UserId": return UserId;
                    case "PassportId": return PassportId;
                    case "Password" : return Password;
                    default: throw new ArgumentException(string.Format("GameUser index[{0}] isn't exist.", index));
                }
                #endregion
            }
            set
            {
                #region
                switch (index)
                {
                    case "UserId":
                        _userId = value.ToInt();
                        break;
                    case "PassportId" :
                        _passportId = value.ToNotNullString();
                        break;
                    case "Password":
                        _password = value.ToNotNullString();
                        break;
                    default: throw new ArgumentException(string.Format("GameUser index[{0}] isn't exist.", index));
                }
                #endregion
            }
        }

        public string GetPassportId()
        {
            return PassportId;
        }

        public int GetUserId()
        {
            return UserId;
        }

        protected override int GetIdentityId()
        {
            return UserId;
        }
    }
}
