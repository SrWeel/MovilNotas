using System;
using System.Collections.Generic;
using System.Text;

namespace MovilNotas.Models
{
    public class AporteDetalle
    {
        public int NotaId { get; set; }
        public string Nota { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
    }
}
