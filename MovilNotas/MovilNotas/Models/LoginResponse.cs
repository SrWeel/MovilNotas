using System;
using System.Collections.Generic;
using System.Text;

namespace MovilNotas.Models
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int Id { get; set; }
        public string Correo { get; set; }
        public List<CursoFlat> Cursos { get; set; }
    }
}
