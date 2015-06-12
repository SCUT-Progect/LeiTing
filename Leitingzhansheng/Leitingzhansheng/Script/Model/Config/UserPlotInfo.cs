using System;
using ProtoBuf;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Event;
using ZyGames.Framework.Game.Cache;
using ScutDemo.Model.Enum;


namespace ScutDemo.Model.Config
{
    [Serializable, ProtoContract]
    public class UserPlotInfo : EntityChangeEvent, IComparable<UserPlotInfo>
    {
        public UserPlotInfo()
            : base(false)
        {

        }

        /// <summary>
        /// 副本编号
        /// </summary>
        [ProtoMember(1)]
        public Int32 PlotID { get; set; }

        /// <summary>
        /// 副本状态，PlotStatus枚举
        /// </summary>
        [ProtoMember(2)]
        public PlotStatus PlotStatus
        {
            get;
            set;
        }
        /// <summary>
        /// 副本评分
        /// </summary>
        [ProtoMember(3)]
        public short ScoreNum
        {
            get;
            set;
        }

        ///// <summary>
        ///// 通关经验
        ///// </summary>
        //[ProtoMember(4)]
        //public Int32 Experience
        //{
        //    get;
        //    set;
        //}

        ///// <summary>
        ///// 宝箱的晶石
        ///// </summary>
        //[ProtoMember(5)]
        //public Int32 GoldNum
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 通关时间
        /// </summary>
        [ProtoMember(6)]
        public DateTime CompleteDate
        {
            get;
            set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        [ProtoMember(7)]
        public DateTime CreateDate
        {
            get;
            set;
        }

        private bool _isFirstIn;

        /// <summary>
        /// 是否第一次进入地图
        /// </summary>
        public Boolean IsFirstIn
        {
            get { return _isFirstIn; }
            set
            {
                _isFirstIn = value;
                NotifyByModify();
            }
        }

        public int CompareTo(UserPlotInfo other)
        {
            if (this == null && other == null) return 0;
            if (this != null && other == null) return 1;
            if (this == null && other != null) return -1;

            return PlotID.CompareTo(other.PlotID);
        }
    }
}