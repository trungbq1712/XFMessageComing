using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using DryIoc;
using Prism.DryIoc;
using XFMessageComing.Droid.Services;
using XFMessageComing.Droid.Specific;

namespace XFMessageComing.Droid
{
    [Activity(Label = "XFMessageComing", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            
            global::Xamarin.Forms.Forms.Init(this, bundle);
            UserDialogs.Init(this);
            LoadApplication(new App(new PlatformInitializer()));
        }

        protected override void OnStart()
        {
            base.OnStart();
            RegisterReceiver(AndroidReceiverService.Instance, new IntentFilter("android.provider.Telephony.SMS_RECEIVED"));
        }
        protected override void OnStop()
        {
            UnregisterReceiver(AndroidReceiverService.Instance);
            base.OnStop();
        }
    }

    
}

