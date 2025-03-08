using System;
using System.Collections.Generic;
using System.Text;

namespace MovilNotas.Models
{
    public class Estudiantes
    {
        public int EstudianteId { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Cedula { get; set; }
        public Dictionary<int, Bimestre> Notas { get; set; }
    }
}
