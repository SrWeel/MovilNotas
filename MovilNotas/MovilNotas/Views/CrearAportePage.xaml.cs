using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MovilNotas.Services;

namespace MovilNotas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CrearAportePage : ContentPage
    {
        private ApiService _apiService;

        public CrearAportePage()
        {
            InitializeComponent();
            _apiService = new ApiService();
        }

        private async void OnCrearAporteClicked(object sender, EventArgs e)
        {
            // Obtener los valores ingresados
            string titulo = tituloEntry.Text;
            string descripcion = descripcionEditor.Text;
            string categoriaText = categoriaEntry.Text;
            string bimestreText = bimestreEntry.Text;
            string materiaText = materiaEntry.Text;
            string jornadaText = jornadaEntry.Text;
            string nivelText = nivelEntry.Text;
            string paraleloText = paraleloEntry.Text;

            // Validar campos requeridos
            if (string.IsNullOrEmpty(titulo) || string.IsNullOrEmpty(categoriaText) ||
                string.IsNullOrEmpty(bimestreText) || string.IsNullOrEmpty(materiaText) ||
                string.IsNullOrEmpty(jornadaText) || string.IsNullOrEmpty(nivelText) ||
                string.IsNullOrEmpty(paraleloText))
            {
                await DisplayAlert("Error", "Todos los campos marcados son requeridos", "OK");
                return;
            }

            // Convertir valores numéricos
            int idCategoria = Convert.ToInt32(categoriaText);
            int idBimestre = Convert.ToInt32(bimestreText);
            int idMateria = Convert.ToInt32(materiaText);
            int idJornada = Convert.ToInt32(jornadaText);
            int idNivel = Convert.ToInt32(nivelText);
            int idParalelo = Convert.ToInt32(paraleloText);

            // Llamar al servicio para crear el aporte
            var result = await _apiService.CrearAporteAsync(idCategoria, idBimestre, titulo, descripcion, idMateria, idJornada, idNivel, idParalelo);

            if (result)
            {
                await DisplayAlert("Éxito", "Aporte creado exitosamente", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "No se pudo crear el aporte", "OK");
            }
        }
    }
}
