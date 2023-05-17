using System;
using System.Collections.Generic;
using System.Text;

namespace XFAppUpdate
{
    public class GlobalSetting
    {
        public static GlobalSetting Instance { get; } = new GlobalSetting();

        public GlobalSetting() 
        {
            ApkUri = "http://192.168.0.129/com.gwise.xfappupdate.apk";
            ApkVerUri   = "http://192.168.0.129/version.txt";
        }

        public string ApkUri { get; set; }
        public string ApkVerUri { get; set; }
    }
}
