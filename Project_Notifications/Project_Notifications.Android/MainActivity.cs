using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Firebase.Iid;
using Prism;
using Prism.Ioc;
using Project_Notifications.Droid.Notifications;
using Project_Notifications.Notifications;
using Xamarin.Forms;

namespace Project_Notifications.Droid
{
    [Activity(Label = "Project_Notifications", Icon = "@mipmap/ic_launcher", LaunchMode = LaunchMode.SingleInstance, Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    [IntentFilter(new[] { "onLocalNotificationReceived" })]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));

            if (Intent.Action == "onLocalNotificationReceived")
            {
                CreateNotificationFromIntent(Intent);
            }
            var token = FirebaseInstanceId.Instance.Token;
        }

        protected override void OnNewIntent(Intent intent)
        {
            CreateNotificationFromIntent(intent);
        }

        void CreateNotificationFromIntent(Intent intent)
        {
            if (intent?.Extras != null)
            {
                string title = intent.Extras.GetString(AndroidNotificationManager.TitleKey);
                string message = intent.Extras.GetString(AndroidNotificationManager.MessageKey);
                App.Resolve<INotificationManager>().ReceiveNotification(title, message);
            }
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<INotificationManager>(new AndroidNotificationManager());
        }
    }
}

