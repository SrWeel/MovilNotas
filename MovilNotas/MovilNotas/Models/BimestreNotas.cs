using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovilNotas.Models
{
    public class BimestreNotas
    {
        public int BimestreId { get; set; }
        public string Bimestre { get; set; }
        public Dictionary<int, CategoriaNotas> Categorias { get; set; }

        // Propiedad para binding que convierte el diccionario a lista
        public List<CategoriaNotas> CategoriasList => Categorias?.Values.ToList();
    }
}
