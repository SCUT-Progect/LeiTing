using System;
using ProtoBuf;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;
using ScutDemo.Model.Config;

namespace ScutDemo.Model.ConfigModel
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable, ProtoContract, EntityTable(AccessLevel.ReadOnly, DbConfig.Config, "UserLvInfo")]
    public class UserLvInfo : ShareEntity
    {


        public UserLvInfo()
            : base(AccessLevel.ReadOnly)
        {
            Award = new CacheList<PrizeBaseInfo>();
        }

        #region auto-generated Property
        private Int16 _UserLv;
        /// <summary>
        /// 用户等级
        /// </summary>
        [EntityField("UserLv", IsKey = true)]
        public Int16 UserLv
        {
            get
            {
                return _UserLv;
            }
            private set
            {
                SetChange("UserLv", value);
            }
        }

        private Int32 _UpExperience;
        /// <summary>
        /// 升到下一级所需经验
        /// </summary>
        [EntityField("UpExperience")]
        public Int32 UpExperience
        {
            get
            {
                return _UpExperience;
            }
            private set
            {
                SetChange("UpExperience", value);
            }
        }

        private CacheList<PrizeBaseInfo> _Award;
        /// <summary>
        /// 升级奖励（升到当前级别）
        /// </summary>
        [EntityField(true, ColumnDbType.LongText)]
        public CacheList<PrizeBaseInfo> Award
        {
            get
            {
                return _Award;
            }
            private set
            {
                SetChange("Award", value);
            }
        }
        protected override object this[string index]
        {
            get
            {
                #region
                switch (index)
                {
                    case "UserLv": return UserLv;
                    case "UpExperience": return UpExperience;
                    case "Award": return Award;
                    default: throw new ArgumentException(string.Format("GeneralEscalateInfo index[{0}] isn't exist.", index));
                }
                #endregion
            }
            set
            {
                #region
                switch (index)
                {
                    case "UserLv":
                        _UserLv = value.ToShort();
                        break;
                    case "UpExperience":
                        _UpExperience = value.ToInt();
                        break;
                    case "Award":
                        _Award = ConvertCustomField<CacheList<PrizeBaseInfo>>(value, index);
                        break;
                    default: throw new ArgumentException(string.Format("GeneralEscalateInfo index[{0}] isn't exist.", index));
                }
                #endregion
            }
        }

        #endregion

        protected override int GetIdentityId()
        {
            //allow modify return value
            return DefIdentityId;
        }
    }
}