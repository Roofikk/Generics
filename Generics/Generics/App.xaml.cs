using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Generics
{
    public partial class App : Application
    {
        public static MedicamentRequest MedRequest = new MedicamentRequest();
        public App()
        {
            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.FromHex("1577d2")
            };

            //MainPage.SetValue(NavigationPage.BarBackgroundColorProperty, Color.Blue);
            InitializeComponent();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
