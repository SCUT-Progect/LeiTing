using System;
using ProtoBuf;
using ZyGames.Framework.Event;
using ScutDemo.Model.Enum;

namespace ScutDemo.Model.Config
{
    /// <summary>
    /// 背包物品配置信息
    /// </summary>
    [Serializable, ProtoContract]
    public class PackageInfo : EntityChangeEvent
    {
        public PackageInfo()
            : base(false)
        {

        }
        /// <summary>
        /// 类型
        /// </summary>
        [ProtoMember(1)]
        public ItemType Type { get; set; }

        /// <summary>
        /// 物品ID
        /// </summary>
        [ProtoMember(2)]
        public int ItemID
        {
            get;
            set;
        }

        /// <summary>
        /// 数量
        /// </summary>
        [ProtoMember(3)]
        public int Num
        {
            get;
            set;
        }
    }
}