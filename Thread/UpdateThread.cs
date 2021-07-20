﻿using GeekDesk.Constant;
using GeekDesk.Util;
using GeekDesk.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeekDesk.Thread
{
    public class UpdateThread
    {
        private static AppConfig appConfig = MainWindow.appData.AppConfig;
        public static void Update()
        {
            System.Threading.Thread t = new System.Threading.Thread(new ThreadStart(UpdateApp))
            {
                IsBackground = true
            };
            t.Start();
        }

        private static void UpdateApp()
        {
            try
            {
                string updateUrl;
                string nowVersion = ConfigurationManager.AppSettings["Version"];
                switch (appConfig.UpdateType)
                {
                    case UpdateType.GitHub:
                        updateUrl = ConfigurationManager.AppSettings["GitHubUpdateUrl"];
                        break;
                    default:
                        updateUrl = ConfigurationManager.AppSettings["GiteeUpdateUrl"];
                        break;
                }
                string updateInfo = HttpUtil.Get(updateUrl);
                if (!StringUtil.IsEmpty(updateInfo))
                {
                    JObject jo = JObject.Parse(updateInfo);
                    string onlineVersion = jo["version"].ToString();
                    if (onlineVersion.CompareTo(nowVersion) > 0)
                    {
                        //检测到版本更新
                    }
                }
            } catch (Exception)
            {

            }
            
            
        }
    }
}