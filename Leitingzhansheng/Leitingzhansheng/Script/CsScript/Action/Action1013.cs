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
using System.Collections.Generic;
using ZyGames.Framework.Game.Cache;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.Common;
using ScutDemo.CsScript.Base;
using ScutDemo.Lang;
using ScutDemo.Model;
using ScutDemo.Model.Enum;
using ScutDemo.Model.ConfigModel;


namespace ScutDemo.CsScript.Action
{

    /// <summary>
    /// 1013商店物品列表接口
    /// </summary>
    public class Action1013 : BaseAction
    {
        private int _pageIndex;
        private int _pageSize;
        private int _pageCount;
        private List<MallItemsInfo> _mallItemsInfoArray = new List<MallItemsInfo>();

        public Action1013(ZyGames.Framework.Game.Contract.HttpGet httpGet)
            : base(ActionIDDefine.Cst_Action1013, httpGet)
        {

        }
        public override void BuildPacket()
        {
            PushIntoStack(_pageCount);
            PushIntoStack(ContextUser.PayGold);
            PushIntoStack(_mallItemsInfoArray.Count);
            foreach (MallItemsInfo mallItems in _mallItemsInfoArray)
            {
                ItemBaseInfo itemInfo = new ConfigCacheSet<ItemBaseInfo>().FindKey(mallItems.ItemID);
                int mallPrice = 0;
                if (itemInfo != null)
                {
                    mallPrice = mallItems.Price;
                }
                DataStruct dsItem = new DataStruct();
                dsItem.PushIntoStack(mallItems.ItemID);
                dsItem.PushIntoStack(itemInfo == null ? string.Empty : itemInfo.ItemName.ToNotNullString());
                dsItem.PushIntoStack(itemInfo == null ? string.Empty : itemInfo.HeadID.ToNotNullString());
                dsItem.PushIntoStack(itemInfo == null ? string.Empty : itemInfo.ItemDesc.ToNotNullString());
                dsItem.PushIntoStack(mallPrice);
                dsItem.PushIntoStack(mallItems.SeqNO);
                PushIntoStack(dsItem);
            }
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("PageIndex", ref _pageIndex)
                 && httpGet.GetInt("PageSize", ref _pageSize))
            {
                return true;
            }
            return false;

        }

        public override bool TakeAction()
        {
            _mallItemsInfoArray = new ConfigCacheSet<MallItemsInfo>().FindAll();
            _mallItemsInfoArray.GetPaging(_pageIndex, _pageSize, out _pageCount);
            return true;
        }
    }
}