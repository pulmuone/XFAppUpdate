using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XFAppUpdate.Views;

namespace XFAppUpdate.Services
{
    public class VersionCheck
    {
        private static readonly VersionCheck instance = new VersionCheck();
        Version versionServer;
        Version versionClient;

        string url = GlobalSetting.Instance.ApkVerUri;

        private VersionCheck()
        {
            VersionTracking.Track();

            GetVersionClient();
        }

        public static VersionCheck Instance
        {
            get
            {
                return instance;
            }
        }

        public bool IsNetworkAccess()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> IsUpdate()
        {
            await GetVersionServer();

            if (versionServer > versionClient)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task UpdateCheck()
        {
            if (versionServer == null)
            {
                await GetVersionServer();
            }

            if (versionServer > versionClient)
            {
                AutoUpdateView autoUpdatePage = new AutoUpdateView();
                await Application.Current.MainPage.Navigation.PushModalAsync(autoUpdatePage);
            }
        }

        /// <summary>
        /// Client 버전 확인
        /// </summary>
        /// <returns></returns>
        public Version GetVersionClient()
        {
            //var assembly = typeof(App).GetTypeInfo().Assembly;
            //var assemblyName = new AssemblyName(assembly.FullName);
            //versionClient = assemblyName.Version;
            versionClient = new Version(VersionTracking.CurrentVersion);

            return versionClient;
        }

        /// <summary>
        /// Server 버전 확인
        /// </summary>
        /// <returns></returns>
        private async Task<Version> GetVersionServer()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(new Uri(url));
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    versionServer = new Version(responseBody);

                    //if (response.IsSuccessStatusCode)
                    //{
                    //    versionServer = new Version(response.Content.ReadAsStringAsync().Result.ToString());
                    //}
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                var properties = new Dictionary<string, string>
                {
                    {"VersionCheck", "GetVersionServer" }
                };
                Crashes.TrackError(ex, properties);
            }

            return versionServer;
        }
    }
}
