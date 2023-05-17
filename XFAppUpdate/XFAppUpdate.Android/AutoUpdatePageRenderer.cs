using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XFAppUpdate.Droid;
using XFAppUpdate.Views;

[assembly: ExportRenderer(typeof(AutoUpdateView), typeof(AutoUpdatePageRenderer))]
namespace XFAppUpdate.Droid
{
    public class AutoUpdatePageRenderer : PageRenderer
    {
        public AutoUpdatePageRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }

            var activity = this.Context;
            var intent = new Intent(activity, typeof(AutoUpdateActivity));

            try
            {
                AutoUpdateActivity.OnUpdateCompleted += () =>
                {
                    Element.Navigation.PopModalAsync();
                };

                activity.StartActivity(intent);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}