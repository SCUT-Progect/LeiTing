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
using ZyGames.Framework.Game.Service;
using ScutDemo.Lang;
using ScutDemo.Model;
using System.Text;
using ScutDemo.Model.ConfigModel;
using ScutDemo.Model.DataModel;


namespace ScutDemo.CsScript.Action
{
    /// <summary>
    /// 用户检测接口
    /// </summary>
    public class Action1004 : BaseAction
    {
        private string nickName = string.Empty;
        public Action1004(ZyGames.Framework.Game.Contract.HttpGet httpGet)
            : base(ActionIDDefine.Cst_Action1004, httpGet)
        {

        }

        public override void BuildPacket()
        {
            //PushIntoStack(true);
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetString("NickName", ref nickName))
            {
                nickName = nickName.Trim();
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            try
            {
                int maxLength = ConfigEnvSet.GetInt("User.MaxLength");

                if (nickName.Trim().Length == 0 || Encoding.Default.GetByteCount(nickName) > maxLength)
                {
                    ErrorCode = LanguageManager.GetLang().ErrorCode;
                    ErrorInfo = string.Format(LanguageManager.GetLang().St1005_KingNameTooLong, maxLength);
                    return false;
                }

                if (GameUser.IsNickName(nickName))
                {
                    ErrorCode = 1;
                    ErrorInfo = LanguageManager.GetLang().St1005_Rename;
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                SaveLog(ex);
                return false;
            }
        }
    }
}