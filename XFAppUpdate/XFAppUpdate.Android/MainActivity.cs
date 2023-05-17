using System;

using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using AndroidX.Core.App;

namespace XFAppUpdate.Droid
{
    [Activity(Label = "AppUpdate", 
        Icon = "@mipmap/icon", 
        Theme = "@style/MainTheme", 
        MainLauncher = true, 
        ConfigurationChanges = ConfigChanges.ScreenSize 
        | ConfigChanges.Orientation 
        | ConfigChanges.UiMode 
        | ConfigChanges.ScreenLayout 
        | ConfigChanges.SmallestScreenSize, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait
        )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        static string[] PERMISSIONS = {
            Manifest.Permission.ReadExternalStorage,
            Manifest.Permission.WriteExternalStorage
            //Manifest.Permission.Camera
        };

        readonly int REQUEST_INSTALL_PERMISSION = 10;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.Window.AddFlags(Android.Views.WindowManagerFlags.KeepScreenOn);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());


            //알수 없는 소스 설치 여부 확인
            ///출처를 알 수 없는 앱 목록 가장 아래에 위치하면 자동으로 포커스 이동이 안된다.
            if (PackageManager.CanRequestPackageInstalls())
            {
                SettingPermission();
            }
            else
            {
                //알수 없는 소스 설치 설정 화면 이동
                var intent = new Intent(
                    Android.Provider.Settings.ActionManageUnknownAppSources,
                    Android.Net.Uri.Parse(string.Format("{0}{1}", "package:", Application.Context.PackageName)));
                //var intent = new Intent(Android.Provider.Settings.ActionManageUnknownAppSources);
                //intent.SetData(Android.Net.Uri.Parse(string.Format("{0}{1}", "package:", Application.Context.PackageName)));
                StartActivityForResult(intent, REQUEST_INSTALL_PERMISSION);
            }
        }


        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == REQUEST_INSTALL_PERMISSION)
            {
                SettingPermission();
            }
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void SettingPermission()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M) //23이상부터
            {
                ActivityCompat.RequestPermissions(this, PERMISSIONS, 0);
            }
        }
    }
}