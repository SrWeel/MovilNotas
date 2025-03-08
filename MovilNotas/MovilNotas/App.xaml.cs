using System;
using MovilNotas.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovilNotas
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Asegúrate de envolver la página principal en un NavigationPage
            MainPage = new NavigationPage(new LoginPage());
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
