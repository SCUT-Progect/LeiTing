using System;
using ProtoBuf;
using ZyGames.Framework.Game.Cache;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;
using ScutDemo.Model.Config;
using ScutDemo.Model.Enum;
using ZyGames.Framework.Cache.Generic;

namespace ScutDemo.Model.ConfigModel
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable, ProtoContract, EntityTable(AccessLevel.ReadOnly, DbConfig.Config, "ItemBaseInfo")]
    public class ItemBaseInfo : ShareEntity
    {

        public const string Index_ItemType = "Index_ItemType";

        public ItemBaseInfo()
            : base(AccessLevel.ReadOnly)
        {

        }

        #region auto-generated Property
        private Int32 _ItemID;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("ItemID", IsKey = true)]
        public Int32 ItemID
        {
            get
            {
                return _ItemID;
            }
            private set
            {
                SetChange("ItemID", value);
            }
        }
        private String _ItemName;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("ItemName")]
        public String ItemName
        {
            get
            {
                return _ItemName;
            }
            private set
            {
                SetChange("ItemName", value);
            }
        }
        private String _HeadID;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("HeadID")]
        public String HeadID
        {
            get
            {
                return _HeadID;
            }
            private set
            {
                SetChange("HeadID", value);
            }
        }
        private ItemType _ItemType;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("ItemType")]
        public ItemType ItemType
        {
            get
            {
                return _ItemType;
            }
            private set
            {
                SetChange("ItemType", value);
            }
        }
 
        private Boolean _IsUse;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("IsUse")]
        public Boolean IsUse
        {
            get
            {
                return _IsUse;
            }
            private set
            {
                SetChange("IsUse", value);
            }
        }
        private String _itemDesc;
        [EntityField("ItemDesc")]
        public String ItemDesc
        {
            get
            {
                return _itemDesc;
            }
            set
            {
                SetChange("ItemDesc", value);
            }
        }
 
        protected override object this[string index]
        {
            get
            {
                #region
                switch (index)
                {
                    case "ItemID" : return ItemID;
                    case "ItemName" : return ItemName;
                    case "HeadID" : return HeadID;
                    case "ItemType" : return ItemType;
                    case "IsUse" : return IsUse;
                    case "ItemDesc": return ItemDesc;
                    default: throw new ArgumentException(string.Format("ItemBaseInfo index[{0}] isn't exist.", index));
                }
                #endregion
            }
            set
            {
                #region
                switch (index)
                {
                    case "ItemID":
                        _ItemID = value.ToInt();
                        break;
                    case "ItemName":
                        _ItemName = value.ToNotNullString();
                        break;
                    case "HeadID":
                        _HeadID = value.ToNotNullString();
                        break;
                    case "ItemType":
                        _ItemType = (ItemType)value.ToInt();
                        break;
                     case "IsUse":
                        _IsUse = value.ToBool();
                        break;
                    case"ItemDesc":
                        _itemDesc = value.ToNotNullString();
                        break;
                    default: throw new ArgumentException(string.Format("ItemBaseInfo index[{0}] isn't exist.", index));
                }
                #endregion
            }
        }

        #endregion

        protected override int GetIdentityId()
        {
            //allow modify return value
            return DefIdentityId;
        } /// <summary>
        /// 
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public static bool IsExist(int itemId)
        {
            return new ConfigCacheSet<ItemBaseInfo>().FindKey(itemId) != null;
        }
    }
}