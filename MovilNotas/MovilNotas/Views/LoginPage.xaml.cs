using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MovilNotas.Services;
using System.Collections.Generic;

namespace MovilNotas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly ApiService _apiService = new ApiService();

        public LoginPage()
        {
            InitializeComponent();
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            string usuario = entryUsuario.Text;
            string clave = entryClave.Text;

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(clave))
            {
                await DisplayAlert("Error", "Ingrese usuario y clave", "OK");
                return;
            }

            var resultado = await _apiService.LoginAsync(usuario, clave);

            if (resultado.Success)
            {
                await DisplayAlert("Éxito", "Login correcto", "OK");
                // Pasar la lista de cursos a HomePage
                await Navigation.PushAsync(new HomePage(resultado.Cursos));
            }
            else
            {
                await DisplayAlert("Error", resultado.Message, "OK");
            }
        }

        
    }
}