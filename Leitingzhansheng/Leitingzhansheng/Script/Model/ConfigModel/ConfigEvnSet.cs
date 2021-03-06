﻿using System;
using ProtoBuf;
using ZyGames.Framework.Game.Cache;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;
using ScutDemo.Model;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ScutDemo.Model.Enum;

namespace ScutDemo.Model.ConfigModel
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadOnly, DbConfig.Config, "ConfigEnvSet")]
    public class ConfigEnvSet : ShareEntity
    {

        public static int GetInt(string key, EnvType type)
        {
            var item = new ShareCacheStruct<ConfigEnvSet>().Find(m => (m._EnvKey == key && m._EnvType == (short)type));
            return item == null ? 0 : item.EnvValue.ToInt();
        }

        public static int GetInt(string key)
        {
            var item = new ConfigCacheSet<ConfigEnvSet>().FindKey(key);
            return item == null ? 0 : item.EnvValue.ToInt();
        }

        public static decimal GetDecimal(string key)
        {
            var item = new ConfigCacheSet<ConfigEnvSet>().FindKey(key);
            return item == null ? 0 : item.EnvValue.ToDecimal();
        }
        public static double GetDouble(string key)
        {
            var item = new ConfigCacheSet<ConfigEnvSet>().FindKey(key);
            return item == null ? 0 : item.EnvValue.ToDouble();
        }

        public static string GetString(string key)
        {
            var item = new ConfigCacheSet<ConfigEnvSet>().FindKey(key);
            return item == null ? "" : item.EnvValue.ToNotNullString();
        }
        
        public ConfigEnvSet()
            : base(AccessLevel.ReadOnly)
        {
        }
        
        #region auto-generated Property
        private String _EnvKey;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("EnvKey", IsKey = true)]
        public String EnvKey
        {
            get
            {
                return _EnvKey;
            }
            private set
            {
                SetChange("EnvKey", value);
            }
        }
        private String _EnvValue;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("EnvValue")]
        public String EnvValue
        {
            get
            {
                return _EnvValue;
            }
            private set
            {
                SetChange("EnvValue", value);
            }
        }
        private Int16 _EnvType;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("EnvType")]
        public Int16 EnvType
        {
            get
            {
                return _EnvType;
            }
            private set
            {
                SetChange("EnvType", value);
            }
        }
        private String _EnvDesc;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("EnvDesc")]
        public String EnvDesc
        {
            get
            {
                return _EnvDesc;
            }
            private set
            {
                SetChange("EnvDesc", value);
            }
        }
    
        protected override object this[string index]
		{
			get
			{
                #region
				switch (index)
				{
                    case "EnvKey": return EnvKey;
                    case "EnvValue": return EnvValue;
                    case "EnvType": return EnvType;
                    case "EnvDesc": return EnvDesc;
					default: throw new ArgumentException(string.Format("ConfigEnvSet index[{0}] isn't exist.", index));
				}
                #endregion
			}
			set
			{
                #region
				switch (index)
				{
                    case "EnvKey": 
                        _EnvKey = value.ToNotNullString(); 
                        break; 
                    case "EnvValue": 
                        _EnvValue = value.ToNotNullString(); 
                        break; 
                    case "EnvType": 
                        _EnvType = value.ToShort(); 
                        break; 
                    case "EnvDesc": 
                        _EnvDesc = value.ToNotNullString(); 
                        break; 
					default: throw new ArgumentException(string.Format("ConfigEnvSet index[{0}] isn't exist.", index));
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