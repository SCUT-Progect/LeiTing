using ScutDemo.Model.Config;
using ScutDemo.Model.ConfigModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Game.Cache;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.Common;
using ScutDemo.Lang;
using ZyGames.Framework.Cache.Generic;

namespace ScutDemo.CsScript.Action
{
    class Action1011 : BaseAction
    {
        private List<PackageInfo> packagelist;
        private int pageIndex = 0;
        private int pageSize = 0;
        private int pageCount = 0;
        //private int _itemCount = 0;
        public Action1011(ZyGames.Framework.Game.Contract.HttpGet httpGet)
            : base(ActionIDDefine.Cst_Action1011, httpGet)
        {
            packagelist = new List<PackageInfo>();
        }

        public override void BuildPacket()
        {
            PushIntoStack(pageCount);
            PushIntoStack(packagelist.Count);
            foreach(PackageInfo item in packagelist)
            {
                DataStruct ds = new DataStruct();
                ItemBaseInfo itemInfo = new ConfigCacheSet<ItemBaseInfo>().FindKey(item.ItemID);
                ds.PushIntoStack(itemInfo == null ? 0 : itemInfo.ItemID);
                ds.PushIntoStack(itemInfo == null ? LanguageManager.GetLang().shortInt : (short)itemInfo.ItemType);
                ds.PushIntoStack(item.Num);
                ds.PushIntoStack(itemInfo == null ? string.Empty : itemInfo.HeadID.ToNotNullString());
                ds.PushIntoStack(itemInfo == null ? string.Empty : itemInfo.ItemName.ToNotNullString());
                ds.PushIntoStack(itemInfo == null ? string.Empty : itemInfo.ItemDesc.ToNotNullString());
                ds.PushIntoStack(itemInfo == null ? 0 : itemInfo.IsUse ? 1 : 0);
                PushIntoStack(ds);
            }        
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("PageIndex", ref pageIndex)
                && httpGet.GetInt("PageSize", ref pageSize))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            var pack = ContextUser.UserExtend.ItemList.FindAll(m=>(m.ItemID!=null));
            packagelist = pack.GetPaging(pageIndex, pageSize, out pageCount);
            return true;
        }


    }
}
