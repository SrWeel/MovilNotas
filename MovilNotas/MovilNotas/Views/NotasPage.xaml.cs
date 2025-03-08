using System;
using System.Linq;
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

        private async void OnVerDetallesClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var estudiante = button?.BindingContext as Estudiante;

            if (estudiante != null)
            {
                // Convertir Estudiante a EstudianteDetalle
                var estudianteDetalle = new EstudianteDetalle
                {
                    EstudianteId = estudiante.EstudianteId,
                    Nombres = estudiante.Nombres,
                    Apellidos = estudiante.Apellidos,
                    Cedula = estudiante.Cedula,
                    Notas = estudiante.Notas.ToDictionary(
                        b => b.BimestreId,
                        b => new BimestreNotas
                        {
                            BimestreId = b.BimestreId,
                            Bimestre = b.BimestreNombre,
                            Categorias = b.Categorias.ToDictionary(
                                c => c.CategoriaId,
                                c => new CategoriaNotas
                                {
                                    CategoriaId = c.CategoriaId,
                                    Categoria = c.CategoriaNombre,
                                    Aportes = c.Aportes.Select(a => new AporteDetalle
                                    {
                                        NotaId = a.NotaId,
                                        Nota = a.Nota,
                                        Titulo = a.Titulo,
                                        Descripcion = a.Descripcion
                                    }).ToList()
                                })
                        })
                };

                // Imprimir para depuración
                foreach (var bimestre in estudianteDetalle.Notas)
                {
                    Console.WriteLine($"Bimestre: {bimestre.Value.Bimestre}");
                    foreach (var categoria in bimestre.Value.Categorias)
                    {
                        Console.WriteLine($"  Categoria: {categoria.Value.Categoria}");
                        foreach (var aporte in categoria.Value.Aportes)
                        {
                            Console.WriteLine($"    Aporte: {aporte.Titulo}, Nota: {aporte.Nota}, Descripcion: {aporte.Descripcion}");
                        }
                    }
                }

                await Navigation.PushAsync(new EstudianteDetallesPage(estudianteDetalle));
            }
        }

        private async void OnCrearAporteButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CrearAportePage());
        }



    }
}
