using ProtoBuf;
using ScutDemo.Model.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Model;
using ZyGames.Framework.Common;

namespace ScutDemo.Model.ConfigModel
{
     [Serializable, ProtoContract, EntityTable(CacheType.Entity, DbConfig.Config, "PrizeInfo")]
    public class PrizeInfo : ShareEntity
    {
        public PrizeInfo()
             : base(AccessLevel.ReadWrite)
        {
            //_PrizeList = new CacheList<PrizeBaseInfo>();
            PrizeList = new CacheList<PrizeBaseInfo>();
        }
        //  ///<summary>
        //  ///奖励Id
        //  ///</summary>
        //private Int32 _PrizeId;
        //[EntityField("PrizeId", IsKey = true)]
        //public Int32 PrizeId
        //{
        //    get
        //    {
        //        return _PrizeId;
        //    }
        //    set
        //    {
        //        SetChange("PrizeId", value);
        //    }
        //}

        // /// <summary>
        // /// 奖励列表
        // /// </summary>
        //private CacheList<PrizeBaseInfo> _PrizeList;
        // [EntityField(true, ColumnDbType.LongText)]
        // public CacheList<PrizeBaseInfo> PrizeList
        //{
        //    get
        //    {
        //        return _PrizeList;
        //    }
        //     set
        //    {
        //        SetChange("PrizeList", value);
        //    }
        //}

        // /// <summary>
        // /// 奖励描述
        // /// </summary>
        // private string _PrizeDesc;
        // [EntityField("PrizeDesc")]
        // public string PrizeDesc
        // {
        //     get
        //     {
        //         return _PrizeDesc;
        //     }
        //     set
        //     {
        //         SetChange("PrizeDesc", value);
        //     }
        // }

        // #region
        // protected override object this[string index]
        // {
        //     get
        //     {
        //         #region
        //         switch (index)
        //         {
        //             case "PrizeId": return PrizeId;
        //             case "PrizeList": return PrizeList;
        //             case "PrizeDesc": return PrizeDesc;
        //             default: throw new ArgumentException(string.Format("ExpeditionInfo index[{0}] isn't exist.", index));
        //         }
        //         #endregion
        //     }
        //     set
        //     {
        //         #region
        //         switch (index)
        //         {
        //             case "PrizeId":
        //                 _PrizeId = value.ToInt();
        //                 break;
        //             case "PrizeList":
        //                 _PrizeList = ConvertCustomField<CacheList<PrizeBaseInfo>>(value, index);
        //                 break;
        //             case "PrizeDesc":
        //                 _PrizeDesc = value.ToNotNullString();
        //                 break;
        //             default: throw new ArgumentException(string.Format("ExpeditionInfo index[{0}] isn't exist.", index));
        //         }
        //         #endregion
        //     }
        // }
        // #endregion
         [ProtoMember(1)]
         [EntityField(true)]
         public Int32 PrizeId
         {
             get;
             set;
         }
         [ProtoMember(2)]
         [EntityField(true, ColumnDbType.LongText)]
         public CacheList<PrizeBaseInfo> PrizeList
         {
             get;
             set;
         }
         [ProtoMember(3)]
         [EntityField]
         public string PrizeDesc
         {
             get;
             set;
         }
         protected override int GetIdentityId()
         {
             return PrizeId;
         }
    }
}
