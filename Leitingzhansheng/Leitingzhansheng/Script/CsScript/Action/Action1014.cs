/****************************************************************************
Copyright (c) 2013-2015 scutgame.com

http://www.scutgame.com

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
****************************************************************************/
using System;
using ZyGames.Framework.Game.Cache;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.Common;
using ZyGames.Framework.Net;
using ScutDemo.Lang;
using ScutDemo.Model;
using ScutDemo.Model.Config;
using ScutDemo.Model.Enum;
using ScutDemo.CsScript.Base;
using ScutDemo.Model.ConfigModel;


namespace ScutDemo.CsScript.Action
{

    /// <summary>
    /// 7004_商店物品购买接口
    /// </summary>
    public class Action1014 : BaseAction
    {
        private int _itemId;
        private int _num = 1;

        public Action1014(ZyGames.Framework.Game.Contract.HttpGet httpGet)
            : base(ActionIDDefine.Cst_Action1014, httpGet)
        {
        }

        public override void BuildPacket()
        {

        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("ItemID", ref _itemId))
            {
                httpGet.GetInt("Num", ref _num, 0, 1000);
                if (_num <= 0)
                    return false;
                return true;
            }
            return false;

        }


        public override bool TakeAction()
        {
            ItemBaseInfo itemInfo = new ConfigCacheSet<ItemBaseInfo>().FindKey(_itemId);
            //UserItemHelper.AddUserItem(ContextUser.UserID, 1702, 1);
            //UserItemHelper.AddUserItem(ContextUser.UserID, 1701, 1);
            //UserItemHelper.AddUserItem(ContextUser.UserID, 1213, 1);
            if (itemInfo == null)
            {
                return false;
            }
            //读取物品信息
            MallItemsInfo mallItemInfo = new ConfigCacheSet<MallItemsInfo>().FindKey(_itemId);
            if (mallItemInfo == null)
            {
                return false;
            }
            //物品价格
            int mallPrice = mallItemInfo.Price;
            int useGold = mallPrice*_num;
            //根据物品类型进行扣钱
            if (ContextUser.PayGold < useGold)
            {
                ErrorCode = 3;
                ErrorInfo = LanguageManager.GetLang().St_GoldNotEnough;
                return false;
            }
            ContextUser.PayGold = MathUtils.Addition(ContextUser.PayGold, useGold);
            ItemHelper.AddUserItem(ContextUser, _itemId, _num);
            ErrorCode = 1;
            ErrorInfo = String.Format(LanguageManager.GetLang().St1014_BuySuccess, itemInfo.ItemName, _num);
            return true;
        }
    }
}