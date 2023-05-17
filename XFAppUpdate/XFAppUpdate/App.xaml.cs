using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFAppUpdate.Views;

namespace XFAppUpdate
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new LoginView();
        }

        protected override void OnStart()
        {
            AppCenter.Start("android=525c2b00-d035-4983-8855-70319a2cd2b5;" + "uwp={Your UWP App secret here};" + "ios={Your iOS App secret here}", typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
