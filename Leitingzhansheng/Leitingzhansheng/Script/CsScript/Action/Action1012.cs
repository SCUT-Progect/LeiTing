using ScutDemo.CsScript.Action;
using ScutDemo.CsScript.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Game.Service;

namespace ScutDemo.CsScript.Action
{
    /// <summary>
    /// 物品使用
    /// </summary>
    class Action1012 : BaseAction
    {
        private int ItemId = 0;
        private List<string> content;

        public Action1012(ZyGames.Framework.Game.Contract.HttpGet httpGet)
            : base(ActionIDDefine.Cst_Action1012, httpGet)
        {
            content = new List<string>();
        }

        public override void BuildPacket()
        {
            this.PushIntoStack(content.Count);
            foreach(string info in content)
            {
                DataStruct ds = new DataStruct();
                ds.PushIntoStack(info);
                this.PushIntoStack(ds);
            }
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("ItemId", ref ItemId))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            content = ItemHelper.UseItemHelper(ContextUser, ItemId);
            return true;
        }
    }
}
