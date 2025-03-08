using System.Collections.Generic;
using MovilNotas.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovilNotas.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
		public HomePage(List<CursoFlat> cursos)
		{
			InitializeComponent();

			if (cursos != null && cursos.Count > 0)
			{
				CursosCollectionView.ItemsSource = cursos;
			}
			else
			{
				DisplayAlert("Error", "No hay cursos disponibles.", "OK");
			}
		}

		private async void CursosCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.CurrentSelection.Count == 0)
				return;

			var cursoSeleccionado = (CursoFlat)e.CurrentSelection[0];

			// Limpiar la selección para evitar problemas de UI
			CursosCollectionView.SelectedItem = null;

			// Navegar a la pantalla de notas del curso seleccionado
			await Navigation.PushAsync(new NotasPage(cursoSeleccionado.ProfesorMateriaId));
		}
	}
}
