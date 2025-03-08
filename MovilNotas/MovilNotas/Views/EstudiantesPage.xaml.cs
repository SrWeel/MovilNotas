using System.Collections.Generic;
using MovilNotas.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovilNotas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstudiantesPage : ContentPage
    {
        public EstudiantesPage(List<Estudiante> estudiantes)
        {
            InitializeComponent();
            EstudiantesCollectionView.ItemsSource = estudiantes;
        }
    }
}
