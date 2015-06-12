using System;
using ProtoBuf;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Event;
using ScutDemo.Model;

namespace ScutDemo.Model.Config
{
    /// <summary>
    /// GameUser 新增字段表
    /// </summary>
    [Serializable, ProtoContract]
    public class GameUserExtend : EntityChangeEvent
    {
        public GameUserExtend()
            : base(false)
        {
            ItemList = new CacheList<PackageInfo>();
            UserPlotList = new CacheList<UserPlotInfo>();
        }

        private CacheList<PackageInfo> _itemList;

        /// <summary>
        /// 背包物品列表
        /// </summary>
        [ProtoMember(2)]
        public CacheList<PackageInfo> ItemList
        {
            get { return _itemList; }
            set
            {
                _itemList = value;
                NotifyByModify();
            }
        }

        private CacheList<UserPlotInfo> _userPlotList;
        /// <summary>
        /// 关卡信息列表
        /// </summary>
        public CacheList<UserPlotInfo> UserPlotList
        {
            get { return _userPlotList; }
            set
            {
                _userPlotList = value;
                NotifyByModify();
            }
        }
        private bool _noviceIsPase;

        /// <summary>
        /// 新手引导是否通过
        /// </summary>
        public bool NoviceIsPase
        {
            get { return _noviceIsPase; }
            set
            {
                _noviceIsPase = value;
                NotifyByModify();
            }
        }
    }
}