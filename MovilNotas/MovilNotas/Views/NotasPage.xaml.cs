using System;
using System.Threading.Tasks;
using MovilNotas.Models;
using MovilNotas.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovilNotas.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NotasPage : ContentPage
	{
		private ApiService _apiService = new ApiService();
		private int _profesorMateriaId;

		public NotasPage(int profesorMateriaId)
		{
			InitializeComponent();
			_profesorMateriaId = profesorMateriaId;
			CargarNotas();
		}

		private async void CargarNotas()
		{
			try
			{
				var notasCurso = await _apiService.ObtenerNotasCursoAsync(_profesorMateriaId);

				if (notasCurso != null)
				{
					BindingContext = notasCurso;
				}
				else
				{
					await DisplayAlert("Error", "No se pudieron cargar las notas.", "OK");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error al cargar notas: {ex.Message}");
				await DisplayAlert("Error", "Ocurrió un problema al cargar los datos.", "OK");
			}
		}
	}
}
