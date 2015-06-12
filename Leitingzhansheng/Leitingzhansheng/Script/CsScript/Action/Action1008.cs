using ScutDemo.CsScript.Action;
using ScutDemo.CsScript.Base;
using ScutDemo.Model.Config;
using ScutDemo.Model.ConfigModel;
using ScutDemo.Model.DataModel;
using ScutDemo.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Service;

namespace ScutDemo.CsScript.Action
{
    class Action1008 : BaseAction
    {
        private string _prizeId;
        private List<string> _prizeList;
        public Action1008(ZyGames.Framework.Game.Contract.HttpGet httpGet)
            : base(ActionIDDefine.Cst_Action1008, httpGet)
        {
            _prizeList = new List<string>();
        }

        public override void BuildPacket()
        {
            this.PushIntoStack(_prizeList.Count);
            foreach(string prizeinfo in _prizeList)
            {
                DataStruct dsItem = new DataStruct();
                dsItem.PushIntoStack(prizeinfo);
                this.PushIntoStack(dsItem);
            }

        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetString("PrizeId", ref _prizeId))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            AddPrizeInfo();
            var prize = ItemHelper.GetPrizeInfo(_prizeId);
            foreach(PrizeBaseInfo prizeinfo in prize.PrizeList)
            {
                _prizeList.Add(ItemHelper.GetPrize(prizeinfo, ContextUser));
            }
            return true;
        }
        public void AddPrizeInfo()
        {
            //PackageInfo package = new PackageInfo();
            //package.ItemID = 1;
            //package.Num = 1;
            //package.Type = ItemType.XinShouLiBao;
            //ContextUser.ModifyLocked(() =>
            //{
            //    ContextUser.UserExtend.ItemList.Add(package);
            //});
            //var c = new PersonalCacheStruct<GameUser>();
            //GameUser user = c.FindKey(ContextUser.UserId, ContextUser.UserId);
            //Console.WriteLine("{0}", user.UserExtend.ItemList.Count);

            Console.WriteLine("初始化奖励信息");
            var prizecache = new ShareCacheStruct<PrizeInfo>();
            //CacheList<PrizeBaseInfo> prizelist = new CacheList<PrizeBaseInfo>();
            PrizeBaseInfo baseprize = new PrizeBaseInfo();
            PrizeBaseInfo baseprize1 = new PrizeBaseInfo();
            PrizeBaseInfo baseprize2 = new PrizeBaseInfo();
            PrizeBaseInfo baseprize3 = new PrizeBaseInfo();
            PrizeBaseInfo baseprize4 = new PrizeBaseInfo();
            PrizeInfo prizeinfo = new PrizeInfo();
            PrizeInfo prizeinfo1 = new PrizeInfo();
            PrizeInfo prizeinfo2 = new PrizeInfo();
            PrizeInfo prizeinfo3 = new PrizeInfo();
            baseprize.Type = ScutDemo.Model.Enum.RewardType.Item;
            baseprize.ItemID = 1;
            baseprize.Num = 1;
            baseprize.Desc = "每日登陆礼包1个";

            //prizelist.Add(baseprize);

            prizeinfo.PrizeId = 1;
            prizeinfo.PrizeList.Add(baseprize);
            prizeinfo.PrizeDesc = "恭喜获得每日登陆礼包一个！";

            if (prizecache.Add(prizeinfo))
            {
                Console.WriteLine("初始化奖励信息成功");
            }
            else
            {
                Console.WriteLine("初始化奖励信息失败");
            }
            ///add 2
            baseprize1.Type = ScutDemo.Model.Enum.RewardType.EnergyNum;
            baseprize1.ItemID = 0;
            baseprize1.Num = 20;
            baseprize1.Desc = "吃了大力丸，你感觉好极了，能量+20";

            //prizelist.Add(baseprize);

            prizeinfo1.PrizeId = 2;
            prizeinfo1.PrizeList.Add(baseprize1);
            prizeinfo1.PrizeDesc = "使用大力丸获得能量20";

            if (prizecache.Add(prizeinfo1))
            {
                Console.WriteLine("初始化奖励信息成功");
            }
            else
            {
                Console.WriteLine("初始化奖励信息失败");
            }
            ///add 3
            baseprize2.Type = ScutDemo.Model.Enum.RewardType.Gold;
            baseprize2.ItemID = 0;
            baseprize2.Num = 20;
            baseprize2.Desc = "天降横财，晶石+20";

            //prizelist.Add(baseprize);

            prizeinfo2.PrizeId = 3;
            prizeinfo2.PrizeList.Add(baseprize2);

            baseprize3.Type = ScutDemo.Model.Enum.RewardType.Item;
            baseprize3.ItemID = 2;
            baseprize3.Num = 1;
            baseprize3.Desc = "恭喜您获得\"大力丸\"一枚";

            prizeinfo2.PrizeList.Add(baseprize3);

            prizeinfo2.PrizeDesc = "使用登陆礼包,获得晶石20,大力丸一个";

            if (prizecache.Add(prizeinfo2))
            {
                Console.WriteLine("初始化奖励信息成功");
            }
            else
            {
                Console.WriteLine("初始化奖励信息失败");
            }
            //add 4

            baseprize4.Type = ScutDemo.Model.Enum.RewardType.ExpNum;
            baseprize4.ItemID = 0;
            baseprize4.Num = 100;
            baseprize4.Desc = "金光一闪，你似乎体唔到了什么，经验+100";

            //prizelist.Add(baseprize);

            prizeinfo3.PrizeId = 4;
            prizeinfo3.PrizeList.Add(baseprize4);
            prizeinfo3.PrizeDesc = "使用金蝉王获得经验100";

            if (prizecache.Add(prizeinfo3))
            {
                Console.WriteLine("初始化奖励信息成功");
            }
            else
            {
                Console.WriteLine("初始化奖励信息失败");
            }
        }
    }
}
