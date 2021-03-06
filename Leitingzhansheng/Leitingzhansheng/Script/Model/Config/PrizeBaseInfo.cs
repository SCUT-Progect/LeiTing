﻿/****************************************************************************
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
using ProtoBuf;
using ScutDemo.Model.Enum;
using ZyGames.Framework.Event;

namespace ScutDemo.Model.Config
{
    /// <summary>
    /// 奖励配置信息
    /// </summary>
    [Serializable, ProtoContract]
    public class PrizeBaseInfo : EntityChangeEvent
    {
        ///// <summary>
        ///// 奖励类型 1：随机奖励,2：概率奖励,3:全部奖励
        ///// </summary>
        //[ProtoMember(1)]
        //public int RewardId { get; set; }

        /// <summary>
        /// 奖励类型
        /// </summary>
        [ProtoMember(1)]
        public RewardType Type { get; set; }

        /// <summary>
        /// 物品ID
        /// </summary>
        [ProtoMember(2)]
        public int ItemID { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [ProtoMember(3)]
        public int Num { get; set; }

        /// <summary>
        /// 概率
        /// </summary>
        [ProtoMember(4)]
        public decimal Probability { get; set; }

        /// <summary>
        /// 获得奖励特殊提示语
        /// </summary>
        [ProtoMember(5)]
        public string Desc { get; set; }

    }
}