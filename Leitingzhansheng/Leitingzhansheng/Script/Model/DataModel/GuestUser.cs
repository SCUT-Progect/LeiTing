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
using ProtoBuf;
using ZyGames.Framework.Game.Context;
using ZyGames.Framework.Model;
using ZyGames.Framework.Common;
using ScutDemo.Model;
using ScutDemo.Model.ConfigModel;
using ScutDemo.Model.Enum;
using ZyGames.Framework.Game.Cache;
using ZyGames.Framework.Data;
using System.Data;
using ScutDemo.Model.Config;
namespace ScutDemo.Model.DataModel
{
    public delegate void AsyncDataChangeCallback(string property, string userID, object oldValue, object value);
    [Serializable, ProtoContract]
    [EntityTable(DbConfig.Data,"GameUser",DbConfig.PeriodTime,DbConfig.PersonalName)]
    
    public class GameUser : BaseUser
    {
        public static AsyncDataChangeCallback Callback { get; set; }
        public GameUser()
            : base(AccessLevel.ReadWrite)
        {
            UserExtend = new GameUserExtend();
        }
        private String _UserId;
        [ProtoMember(1)]
        [EntityField("UserId",IsKey = true)]
        public string UserId 
        {
            get
            {
                return _UserId;
            }
            set
            {
                SetChange("UserId", value);
            } 
        }

        private String _NickName;
        [ProtoMember(2)]
        [EntityField("NickName")]
        public String NickName
        {
            get
            {
                return _NickName;
            }
            set
            {
                SetChange("NickName", value);
            }
        }
        private String _PassportId;
        [ProtoMember(3)]
        [EntityField("PassportId")]
        public String PassportId
        {
            get
            {
                return _PassportId;
            }
            set
            {
                SetChange("PassportId", value);
            }
        }
        private String _RetailId;
        [ProtoMember(4)]
        [EntityField("RetailId")]
        public String RetailId
        {
            get
            {
                return _RetailId;
            }
            set
            {
                SetChange("RetailId", value);
            }
        }

        private Int16 _UserLv;
        /// <summary>
        /// 用户等级
        /// </summary>
        [ProtoMember(5)]
        [EntityField("UserLv")]
        public Int16 UserLv
        {
            get
            {
                return _UserLv;
            }
            set
            {
                if (Callback != null && !IsLoading)
                {
                    Callback.BeginInvoke("UserLv", UserId, _UserLv, value, null, this);
                }
                SetChange("UserLv", value);
            }
        }

        private Int16 _EnergyNum;
        /// <summary>
        /// 能量点数
        /// </summary>
        [ProtoMember(6)]
        [EntityField("EnergyNum")]
        public Int16 EnergyNum
        {
            get
            {
                return _EnergyNum;
            }
            set
            {
                if (Callback != null && !IsLoading)
                {
                    Callback.BeginInvoke("EnergyNum", UserId, _EnergyNum, value, null, this);
                }
                SetChange("EnergyNum", value);
            }
        }

        private Int32 _PayGold;
        /// <summary>
        /// 用户钻石
        /// </summary>
        [ProtoMember(7)]
        [EntityField("PayGold")]
        public Int32 PayGold
        {
            get
            {
                return _PayGold;
            }
            set
            {
                if (Callback != null && !IsLoading)
                {
                    Callback.BeginInvoke("PayGold", UserId, _PayGold, value, null, this);
                }
                SetChange("PayGold", value);
            }
        }

        private Int32 _ExpNum;
        /// <summary>
        /// 用户经验
        /// </summary>
        [ProtoMember(8)]
        [EntityField("ExpNum")]
        public Int32 ExpNum
        {
            get
            {
                return _ExpNum;
            }
            set
            {
                SetChange("ExpNum", value);
            }
        }

        private UserStatus _UserStatus;
        /// <summary>
        /// 用户状态（正在进行的活动）
        /// </summary>
        [ProtoMember(9)]
        [EntityField("UserStatus")]
        public UserStatus UserStatus
        {
            get
            {
                return _UserStatus;
            }
            set
            {
                SetChange("UserStatus", value);
            }
        }

        private DateTime _CreateDate;
        /// <summary>
        /// 创建时间
        /// </summary>
        [ProtoMember(10)]
        [EntityField("CreateDate")]
        public DateTime CreateDate
        {
            get
            {
                return _CreateDate;
            }
            set
            {
                SetChange("CreateDate", value);
            }
        }

        private DateTime _LoginTime;
        /// <summary>
        /// 登陆时间
        /// </summary>
        [ProtoMember(11)]
        [EntityField("LoginTime")]
        public DateTime LoginTime
        {
            get
            {
                return _LoginTime;
            }
            set
            {
                SetChange("LoginTime", value);
            }
        }

        private GameUserExtend _UserExtend;
        /// <summary>
        /// 用户拓展（背包，关卡）
        /// </summary>
        [ProtoMember(12)]
        [EntityField(true, ColumnDbType.LongText)]
        public GameUserExtend UserExtend
        {
            get
            {
                return _UserExtend;
            }
            set
            {
                SetChange("UserExtend", value);
            }
        }
        private string _HeadId;
        [ProtoMember(13)]
        [EntityField("HeadId")]
        public string HeadId
        {
            get
            {
                return _HeadId;
            }
            set
            {
                SetChange("HeadId", value);
            }
        }

        private string _sessionId;
        /// <summary>
        /// SId 下发的通信识别
        /// </summary>
        public string SessionId
        {
            get { return _sessionId; }
            set
            {
                _sessionId = value;
            }
        }

        protected override object this[string index]
        {
            get
            {
                #region
                switch (index)
                {
                    case "UserId": return UserId;
                    case "UserLv": return UserLv;
                    case "EnergyNum": return EnergyNum;
                    case "PayGold": return PayGold;
                    case "RetailId": return RetailId;
                    case "ExpNum": return ExpNum;
                    case "UserStatus": return UserStatus;
                    case "PassportId": return PassportId;
                    case "CreateDate": return CreateDate;
                    case "LoginTime": return LoginTime;
                    case "UserExtend": return UserExtend;
                    case "HeadId": return HeadId;
                    default: throw new ArgumentException(string.Format("GameUser index[{0}] isn't exist.", index));
                }
                #endregion
            }
            set
            {
                #region
                switch (index)
                {
                    case "UserId":
                        _UserId = value.ToNotNullString();
                        break;
                    case "NickName":
                        _NickName = value.ToNotNullString();
                        break;
                    case "UserLv":
                        _UserLv = value.ToShort();
                        break;
                    case "EnergyNum":
                        _EnergyNum = value.ToShort();
                        break;
                    case "RetailId":
                        _RetailId = value.ToNotNullString();
                        break;
                    case "PayGold":
                        _PayGold = value.ToInt();
                        break;
                    case "ExpNum":
                        _ExpNum = value.ToInt();
                        break;
                    case "UserStatus":
                        _UserStatus = value.ToEnum<UserStatus>();
                        break;
                    case "PassportId":
                        _PassportId = value.ToNotNullString();
                        break;
                    case "CreateDate":
                        _CreateDate = value.ToDateTime();
                        break;
                    case "LoginTime":
                        _LoginTime = value.ToDateTime();
                        break;
                    case "UserExtend":
                        _UserExtend = ConvertCustomField<GameUserExtend>(value, index);
                        break;
                    case "HeadId":
                        _HeadId = value.ToNotNullString();
                        break;
                    default: throw new ArgumentException(string.Format("GameUser index[{0}] isn't exist.", index));
                }
                #endregion
            }
        }

        /// <summary>
        /// 是否刷新信息（待解决？？？）
        /// </summary>
        [ProtoMember(14)]
        public bool IsRefreshing { get; set; }

        /// <summary>
        /// 是否在线（判断重复登陆）
        /// </summary>
        [ProtoMember(15)]
        public bool IsOnline
        {
            get;
            set;
        }

        /// <summary>
        /// 在线时间
        /// </summary>
        //[ProtoMember(15)]
        //public override DateTime OnlineDate
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 在线时间秒数（似乎有错误，更正）
        /// </summary>
        public int DayTime
        {
            get { return (int)(DateTime.Now - LoginTime).TotalSeconds; }
        }

        /// <summary>
        /// 昵称是否重名判断，类方法
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool IsNickName(string name)
        {
            //return new GameDataCacheSet<GameUser>().IsExist(u => u.NickName.ToLower() == name.ToLower().Trim());
            bool bl = false;
            bl = new GameDataCacheSet<GameUser>().IsExist(u => u.NickName.ToLower() == name.ToLower().Trim());
            if (!bl)
            {
                var dbProvider = DbConnectionProvider.CreateDbProvider(DbConfig.Data);

                var command = dbProvider.CreateCommandStruct("GameUser", CommandMode.Inquiry, "NickName");
                command.Filter = dbProvider.CreateCommandFilter();
                command.Filter.Condition = command.Filter.FormatExpression("NickName");
                command.Filter.AddParam("NickName", name);
                command.Parser();
                using (var reader = dbProvider.ExecuteReader(CommandType.Text, command.Sql, command.Parameters))
                {
                    while (reader.Read())
                    {
                        bl = true;
                    }
                }
            }

            return bl;
        }

        /// <summary>
        /// 下面都是必须的重写
        /// </summary>
        protected override int GetIdentityId()
        {
            return UserId.ToInt();
        }
        
        public override int GetUserId()
        {
            return UserId.ToInt();
        }

        public override string GetNickName()
        {
            return NickName;
        }

        public override string GetPassportId()
        {
            return PassportId;
        }

        public override string GetRetailId()
        {
            return RetailId;
        }

        public override bool IsLock
        {
            get { return UserStatus == UserStatus.FengJin; }
        }
    }

}