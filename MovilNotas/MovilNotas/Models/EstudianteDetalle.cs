using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovilNotas.Models
{
    public class EstudianteDetalle
    {
        public int EstudianteId { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Cedula { get; set; }

        // Diccionario original de notas
        public Dictionary<int, BimestreNotas> Notas { get; set; }

        // Propiedad para binding que convierte el diccionario a lista
        public List<BimestreNotas> NotasList => Notas?.Values.ToList();
    }

}
