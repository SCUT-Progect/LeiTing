using System.Configuration;

namespace ScutDemo.Model
{
    public class DbConfig
    {
        public const string Config = "ScutDemoConfig";
        public const string Data = "ScutDemoData";
        public const string Log = "ScutDemoLog";
        public const int GlobalPeriodTime = 0;
        public const int PeriodTime = 86400;
        /// <summary>
        /// 
        /// </summary>
        public const string PersonalName = "UserId";

        public static string ConfigConnectString
        {
            get
            {
                return Config;
                //return ConfigurationManager.ConnectionStrings[Config].ConnectionString;
            }
        }

        public static string DataConnectString
        {
            get
            {
                return Data;
                //return ConfigurationManager.ConnectionStrings[Data].ConnectionString;
            }
        }

        public static string LogConnectString
        {
            get
            {
                return Log;
                //return ConfigurationManager.ConnectionStrings[Log].ConnectionString;
            }
        }
    }
}