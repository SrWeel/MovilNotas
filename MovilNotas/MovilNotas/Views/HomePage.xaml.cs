using System;
using System.Collections.Generic;
using MovilNotas.Models;
using MovilNotas.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovilNotas.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
        private int _idProfesor;

        public HomePage(List<CursoFlat> cursos, int idProfesor)
        {
            InitializeComponent();
            _idProfesor = idProfesor;

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
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ApiService apiService = new ApiService();

            var categorias = await apiService.ObtenerCategoriasAsync();
            var bimestres = await apiService.ObtenerBimestresAsync();
            var materias = await apiService.ObtenerMateriasAsync(_idProfesor);
            var jornadas = await apiService.ObtenerJornadasAsync();
            var niveles = await apiService.ObtenerNivelesAsync();
            var paralelos = await apiService.ObtenerParalelosAsync();

            // Ejemplo para llenar un Picker (ComboBox) en XAML:
            CategoriasPicker.ItemsSource = categorias;
            BimestresPicker.ItemsSource = bimestres;
            MateriasPicker.ItemsSource = materias;
            JornadasPicker.ItemsSource = jornadas;
            NivelesPicker.ItemsSource = niveles;
            ParalelosPicker.ItemsSource = paralelos;
        }
        private async void BtnCrearAporte_Clicked(object sender, EventArgs e)
        {
            if (CategoriasPicker.SelectedItem == null || BimestresPicker.SelectedItem == null ||
                MateriasPicker.SelectedItem == null || JornadasPicker.SelectedItem == null ||
                NivelesPicker.SelectedItem == null || ParalelosPicker.SelectedItem == null ||
                string.IsNullOrEmpty(TituloEntry.Text))
            {
                await DisplayAlert("Aviso", "Completa todos los campos obligatorios.", "OK");
                return;
            }

            var aporte = new AporteSrweel
            {
                id_categoria = ((CategoriaSrweel)CategoriasPicker.SelectedItem).id,
                id_bimestre = ((BimestreSrweel)BimestresPicker.SelectedItem).id,
                titulo = TituloEntry.Text,
                descripcion = DescripcionEditor.Text ?? "",
                id_materia = ((MateriaSrweel)MateriasPicker.SelectedItem).id,
                id_jornada = ((JornadaSrweel)JornadasPicker.SelectedItem).id,
                id_nivel = ((NivelSrweel)NivelesPicker.SelectedItem).id,
                id_paralelo = ((ParaleloSrweel)ParalelosPicker.SelectedItem).id
            };


            ApiService apiService = new ApiService();
            var creado = await apiService.CrearAporteAsync(aporte);

            if (creado)
            {
                await DisplayAlert("Éxito", "Aporte creado correctamente.", "OK");
                TituloEntry.Text = "";
                DescripcionEditor.Text = "";
            }
            else
            {
                await DisplayAlert("Error", "No se pudo crear el aporte.", "OK");
            }
        }
        // Añade estos métodos a tu clase HomePage

        private void ToggleFormButton_Clicked(object sender, EventArgs e)
        {
            // Cambiar la visibilidad del formulario y la lista de materias
            FormularioStackLayout.IsVisible = !FormularioStackLayout.IsVisible;
            MateriasStackLayout.IsVisible = !FormularioStackLayout.IsVisible;

            // Cambiar el texto del botón según el estado
            if (FormularioStackLayout.IsVisible)
            {
                ToggleFormButton.Text = "Cancelar";
                ToggleFormButton.BackgroundColor = Color.FromHex("#E74C3C");
            }
            else
            {
                ToggleFormButton.Text = "Crear Nuevo Aporte";
                ToggleFormButton.BackgroundColor = Color.FromHex("#2980B9");
            }
        }

        private void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            // Ocultar el formulario y mostrar la lista de materias
            FormularioStackLayout.IsVisible = false;
            MateriasStackLayout.IsVisible = true;

            // Restaurar el texto y color del botón
            ToggleFormButton.Text = "Crear Nuevo Aporte";
            ToggleFormButton.BackgroundColor = Color.FromHex("#2980B9");

            // Opcional: Limpiar los campos del formulario
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            // Limpia todos los campos del formulario
            TituloEntry.Text = string.Empty;
            DescripcionEditor.Text = string.Empty;

            // Restaurar selección por defecto en los pickers
            CategoriasPicker.SelectedIndex = -1;
            BimestresPicker.SelectedIndex = -1;
            MateriasPicker.SelectedIndex = -1;
            JornadasPicker.SelectedIndex = -1;
            NivelesPicker.SelectedIndex = -1;
            ParalelosPicker.SelectedIndex = -1;
        }
    }
}
