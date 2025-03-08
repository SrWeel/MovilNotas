using System;
using System.Collections.Generic;
using System.Text;

namespace MovilNotas.Models
{
    public class Bimestres
    {
        public int BimestreId { get; set; }
        public string BimestreNombre { get; set; }
        public Dictionary<int, Categoria> Categorias { get; set; }
    }
}
