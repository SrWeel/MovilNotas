using System;
using System.Collections.Generic;
using System.Text;

namespace MovilNotas.Models
{
    public class Categorias
    {
        public int CategoriaId { get; set; }
        public string CategoriaNombre { get; set; }
        public List<Aporte> Aportes { get; set; }
    }
}
