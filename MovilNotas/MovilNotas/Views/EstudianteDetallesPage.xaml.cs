using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MovilNotas.Models;

namespace MovilNotas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstudianteDetallesPage : ContentPage
    {
        public EstudianteDetallesPage(EstudianteDetalle estudiante)
        {
            InitializeComponent();
            BindingContext = estudiante;
        }
    }
}
