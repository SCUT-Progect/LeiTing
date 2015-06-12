using ScutDemo.Lang;
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

namespace ScutDemo.CsScript.Base
{
    public class ItemHelper
    {
        public static PrizeInfo GetPrizeInfo(string key)
        {
            var cacheset = new ShareCacheStruct<PrizeInfo>();
            PrizeInfo prize = null;
            switch(key)
            {
                case "LoginPrize":
                    Console.WriteLine("{0}", cacheset.FindAll(true).Count());
                    //prize = cacheset.FindKey(1);
                    prize = cacheset.Find(m =>(m.PrizeId == ConfigEnvSet.GetInt("Prize.FirstLogin")));
                    if( prize == null)
                    {
                        Console.WriteLine("Can't find prize");
                    }
                    break;
                default: throw new ArgumentException(string.Format("GetPrizeInfo index[{0}] isn't exist.", key));
                  
            }
            return prize;
        }

        public static string GetPrize(PrizeBaseInfo prizeinfo, GameUser ContextUser)
        {
            switch (prizeinfo.Type)
            {
                case RewardType.ExpNum:
                    if (ContextUser.UserLv == 99)
                    {
                    }
                    else
                    {
                        var lvinfo = new ShareCacheStruct<UserLvInfo>().FindKey(ContextUser.UserLv);
                        if (ContextUser.ExpNum + prizeinfo.Num < lvinfo.UpExperience)
                        {
                            ContextUser.ModifyLocked(() =>
                            {
                                ContextUser.ExpNum = ContextUser.ExpNum + prizeinfo.Num;
                            });
                            break;
                        }
                        while(ContextUser.ExpNum + prizeinfo.Num >= lvinfo.UpExperience)
                        {
                            ContextUser.ModifyLocked(() =>
                                {
                                    ContextUser.UserLv += 1;
                                    ContextUser.ExpNum = ContextUser.ExpNum + prizeinfo.Num - lvinfo.UpExperience;
                                });
                            lvinfo = new ShareCacheStruct<UserLvInfo>().FindKey(ContextUser.UserLv);
                        }
                    }
                    break;
                case RewardType.EnergyNum :
                    ContextUser.ModifyLocked(() =>
                    {
                        ContextUser.EnergyNum += (short)prizeinfo.Num;
                    });
                    break;
                case RewardType.Gold :
                    if (ContextUser.PayGold + prizeinfo.Num > 
                        ConfigEnvSet.GetInt("User.MaxPayGold"))
                    {
                        return LanguageManager.GetLang().Wb1001_OverflowMaxPayGold;
                    }
                    else
                    {
                        ContextUser.ModifyLocked(() =>
                            {
                                ContextUser.PayGold += prizeinfo.Num;
                            });
                    }
                    break;
                case RewardType.Item :
                    PackageInfo packageitem = ContextUser.UserExtend
                        .ItemList.Find(m => (m.ItemID == prizeinfo.ItemID));
                    if (packageitem == null)
                    {
                        var cacheset = new ShareCacheStruct<ItemBaseInfo>();
                        ItemBaseInfo item = cacheset.FindKey(prizeinfo.ItemID);
                        if (item == null)
                        {
                            return LanguageManager.GetLang().St1217_NotItem;
                        }
                        else
                        {
                            packageitem = new PackageInfo()
                            {
                                Type = item.ItemType,
                                ItemID = item.ItemID,
                                Num = prizeinfo.Num
                            };
                            ContextUser.ModifyLocked(() =>{
                                ContextUser.UserExtend.ItemList.Add(packageitem);
                            });
                            
                        }
                    }
                    else
                    {
                        ContextUser.ModifyLocked(() =>
                        {
                            packageitem.Num += prizeinfo.Num;
                        });
                    }
                    break;
                default: throw new ArgumentException(string.Format("GetPrize index[{0}] isn't exist.", prizeinfo.Type));
            }
            return prizeinfo.Desc;
        }

        public static List<string> UseItemHelper(GameUser ContextUser, int ItemId)
        {
            List<string> content = new List<string>();
            var itemlist = ContextUser.UserExtend.ItemList;
            var item = itemlist.Find(m => (m.ItemID == ItemId));
            if (null == item)
            {
                content.Add(LanguageManager.GetLang().St1107_UserItemNotEnough);
            }
            else
            {
                if (item.Num <= 0)
                {
                    ContextUser.ModifyLocked(() =>
                    {
                        itemlist.Remove(item);
                    });
                    content.Add(LanguageManager.GetLang().St1107_UserItemNotEnough);
                }
                else
                {
                    ContextUser.ModifyLocked(() =>
                    {
                        item.Num -= 1;
                        if (item.Num == 0)
                        {
                            itemlist.Remove(item);
                        }
                    });
                    content = UseItem(ContextUser, ItemId);//使用物品产生效果
                }
            }
            return content;
        }

        private static List<string> UseItem(GameUser ContextUser, int ItemId)
        {
            List<string> content = new List<string>();
            var item = new ShareCacheStruct<ItemBaseInfo>().Find(m=>
                (m.ItemID==ItemId));
            if (null == item)
            {
                content.Add(LanguageManager.GetLang().St1107_UserItemNotEnough);
            }
            else
            {
                int prizeId = ConfigEnvSet.GetInt(ItemId.ToString(),EnvType.Item);
                var prizeinfo = new ShareCacheStruct<PrizeInfo>().FindKey(prizeId);
                foreach(PrizeBaseInfo prize in prizeinfo.PrizeList)
                {
                    content.Add(GetPrize(prize, ContextUser));
                }
            }
            return content;
        }

        public static void AddUserItem(GameUser ContextUser, int _itemId, int _num)
        {
            var item = new ShareCacheStruct<ItemBaseInfo>().FindKey(_itemId);
            if(item == null)
            {
                return;
            }
            var packageitem = ContextUser.UserExtend.ItemList.Find(m=>
                (m.ItemID == _itemId));
            if( packageitem == null)
            {
                PackageInfo packageinfo = new PackageInfo() {
                    Type = item.ItemType,
                    ItemID = _itemId,
                    Num = _num
                };
                    ContextUser.ModifyLocked(() =>
                {
                     ContextUser.UserExtend.ItemList.Add(packageinfo);
                });
            }
            else
            {
                var curitem = ContextUser.UserExtend.ItemList.Find(m =>
                    (m.ItemID == _itemId));
                ContextUser.ModifyLocked(() =>
                {
                    curitem.Num += _num;
                });
            }

        }
    }
}
