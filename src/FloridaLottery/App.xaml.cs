using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FloridaLottery.Services;
using FloridaLottery.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Analytics;
using Acr.UserDialogs;

namespace FloridaLottery
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockGemStore>();
            DependencyService.Register<MockDataStore>();
            DependencyService.Register<NavigationService>();
            DependencyService.Register<MessageService>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            
            AppCenter.Start($"android={Helpers.Secrets.AppCenterIdDroid}" +
                  "uwp={Your UWP App secret here};" +
                  "ios={Your iOS App secret here}",
                  typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
