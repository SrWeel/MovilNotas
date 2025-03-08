using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovilNotas.Models
{
	public class CursoFlat
	{
		[JsonProperty("profesor_materia_id")]
		public int ProfesorMateriaId { get; set; }

		[JsonProperty("carrera")]
		public string Carrera { get; set; }

		[JsonProperty("nivel")]
		public string Nivel { get; set; }

		[JsonProperty("jornada")]
		public string Jornada { get; set; }

		[JsonProperty("paralelo")]
		public string Paralelo { get; set; }

		[JsonProperty("materia")]
		public string Materia { get; set; }
	}


    public class ParaleloSrweel
    {
        public int id { get; set; }
        public string nombre { get; set; }
    }

    public class Nivel
	{
		public int NivelId { get; set; }
		public string Nombre { get; set; }
		public List<Jornada> Jornadas { get; set; }
	}

	public class Jornada
	{
		public int JornadaId { get; set; }
		public string Nombre { get; set; }
		public List<Paralelo> Paralelos { get; set; }
	}

	public class Paralelo
	{
		public int ParaleloId { get; set; }
		public string Nombre { get; set; }
		public List<Materia> Materias { get; set; }
	}

	public class Materia
	{
		public int MateriaId { get; set; }
		public string Nombre { get; set; }
	}

}

