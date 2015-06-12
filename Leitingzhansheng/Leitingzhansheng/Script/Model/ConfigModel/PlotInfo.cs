using System;
using ProtoBuf;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;
using ScutDemo.Model.Config;
using ZyGames.Framework.Cache.Generic;
using ScutDemo.Model.Enum;
using ScutDemo.Model;

namespace ScutDemo.Model.ConfigModel
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable, ProtoContract, EntityTable(AccessLevel.ReadOnly, DbConfig.Config, "PlotInfo")]
    public class PlotInfo : ShareEntity
    {

        //public const string Index_CityID_PlotType = "Index_CityID_PlotType";
        public const string Index_PlotType = "Index_PlotType";
        public const string Index_LayerNum_PlotType = "Index_LayerNum_PlotType";

        public PlotInfo()
            : base(AccessLevel.ReadOnly)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        //public static int BattleGold = ConfigEnvSet.GetInt("Plot.BattleGold");

        /// <summary>
        /// 
        /// </summary>
        public static short BattleEnergyNum = (short)ConfigEnvSet.GetInt("Plot.BattleEnergyNum");


        #region auto-generated Property
        private Int32 _PlotID;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("PlotID", IsKey = true)]
        public Int32 PlotID
        {
            get
            {
                return _PlotID;
            }
            private set
            {
                SetChange("PlotID", value);
            }
        }

        private PlotType _PlotType;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("PlotType")]
        public PlotType PlotType
        {
            get
            {
                return _PlotType;
            }
            private set
            {
                SetChange("PlotType", value);
            }
        }
        private short _PlotSeqNo;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("PlotSeqNo")]
        public short PlotSeqNo
        {
            get
            {
                return _PlotSeqNo;
            }
            private set
            {
                SetChange("PlotSeqNo", value);
            }
        }
        private String _PlotName;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("PlotName")]
        public String PlotName
        {
            get
            {
                return _PlotName;
            }
            private set
            {
                SetChange("PlotName", value);
            }
        }
        private String _BossHeadID;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("BossHeadID")]
        public String BossHeadID
        {
            get
            {
                return _BossHeadID;
            }
            private set
            {
                SetChange("BossHeadID", value);
            }
        }
 
        private Int32 _PrePlotID;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("PrePlotID")]
        public Int32 PrePlotID
        {
            get
            {
                return _PrePlotID;
            }
            private set
            {
                SetChange("PrePlotID", value);
            }
        }
        private Int32 _AftPlotID;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("AftPlotID")]
        public Int32 AftPlotID
        {
            get
            {
                return _AftPlotID;
            }
            private set
            {
                SetChange("AftPlotID", value);
            }
        }
        private Int16 _DemandLv;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("DemandLv")]
        public Int16 DemandLv
        {
            get
            {
                return _DemandLv;
            }
            private set
            {
                SetChange("DemandLv", value);
            }
        }

        private Int32 _Gold;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("Gold")]
        public Int32 Gold
        {
            get
            {
                return _Gold;
            }
            private set
            {
                SetChange("Gold", value);
            }
        }
        private Decimal _GoldProbability;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("GoldProbability")]
        public Decimal GoldProbability
        {
            get
            {
                return _GoldProbability;
            }
            private set
            {
                SetChange("GoldProbability", value);
            }
        }

        private Int32 _ExpNum;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("ExpNum")]
        public Int32 ExpNum
        {
            get
            {
                return _ExpNum;
            }
            private set
            {
                SetChange("ExpNum", value);
            }
        }

        private String _ItemRank;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("ItemRank")]
        public String ItemRank
        {
            get
            {
                return _ItemRank;
            }
            private set
            {
                SetChange("ItemRank", value);
            }
        }
        private Decimal _ItemProbability;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("ItemProbability")]
        public Decimal ItemProbability
        {
            get
            {
                return _ItemProbability;
            }
            private set
            {
                SetChange("ItemProbability", value);
            }
        }

        private String _PlotDesc;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("PlotDesc")]
        public String PlotDesc
        {
            get
            {
                return _PlotDesc;
            }
            private set
            {
                SetChange("PlotDesc", value);
            }
        }

        protected override object this[string index]
        {
            get
            {
                #region
                switch (index)
                {
                    case "PlotID": return PlotID;
                    case "PlotSeqNo": return PlotSeqNo;
                    case "PlotName": return PlotName;
                    case "BossHeadID": return BossHeadID;
                    case "PrePlotID": return PrePlotID;
                    case "AftPlotID": return AftPlotID;
                    case "DemandLv": return DemandLv;
                    case "Gold": return Gold;
                    case "GoldProbability": return GoldProbability;
                    case "ItemRank": return ItemRank;
                    case "ItemProbability": return ItemProbability;
                    case "PlotDesc": return PlotDesc;
                    default: throw new ArgumentException(string.Format("PlotInfo index[{0}] isn't exist.", index));
                }
                #endregion
            }
            set
            {
                #region
                switch (index)
                {
                    case "PlotID":
                        _PlotID = value.ToInt();
                        break;
                    case "PlotSeqNo":
                        _PlotSeqNo = value.ToShort();
                        break;
                    case "PlotName":
                        _PlotName = value.ToNotNullString();
                        break;
                    case "BossHeadID":
                        _BossHeadID = value.ToNotNullString();
                        break;
                    case "PrePlotID":
                        _PrePlotID = value.ToInt();
                        break;
                    case "AftPlotID":
                        _AftPlotID = value.ToInt();
                        break;
                    case "DemandLv":
                        _DemandLv = value.ToShort();
                        break;
                    case "Gold":
                        _Gold = value.ToInt();
                        break;
                    case "GoldProbability":
                        _GoldProbability = value.ToDecimal();
                        break;
                    case "ExpNum":
                        _ExpNum = value.ToInt();
                        break;
                    case "ItemRank":
                        _ItemRank = value.ToNotNullString();
                        break;
                    case "ItemProbability":
                        _ItemProbability = value.ToDecimal();
                        break;;
                    case "PlotDesc":
                        _PlotDesc = value.ToNotNullString();
                        break;
                    default: throw new ArgumentException(string.Format("PlotInfo index[{0}] isn't exist.", index));
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
        public int GetRandomGold()
        {
            return RandomUtils.IsHit(this.GoldProbability) ? this.Gold : 0;
        }

    }
}