using System;
using System.Collections.Generic;
using System.Text;

namespace MovilNotas.Models
{
    public class CategoriaNotas
    {
        public int CategoriaId { get; set; }
        public string Categoria { get; set; }
        public List<AporteDetalle> Aportes { get; set; }
    }
}
