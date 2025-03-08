using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MovilNotas.Models
{
	public class CursoDetalles
	{
		[JsonProperty("materia")]
		public string Materia { get; set; }

		[JsonProperty("jornada")]
		public string Jornada { get; set; }

		[JsonProperty("paralelo")]
		public string Paralelo { get; set; }

		[JsonProperty("nivel")]
		public string Nivel { get; set; }

		[JsonProperty("carrera")]
		public string Carrera { get; set; }
	}

	public class ProfesorDetalles
	{
		[JsonProperty("nombres")]
		public string Nombres { get; set; }

		[JsonProperty("apellidos")]
		public string Apellidos { get; set; }

		[JsonProperty("cedula")]
		public string Cedula { get; set; }

		[JsonProperty("correo")]
		public string Correo { get; set; }
	}

	public class Aporte
	{
		[JsonProperty("nota_id")]
		public int NotaId { get; set; }

		[JsonProperty("nota")]
		public string Nota { get; set; }

		[JsonProperty("titulo")]
		public string Titulo { get; set; }

		[JsonProperty("descripcion")]
		public string Descripcion { get; set; }
	}

	public class Categoria
	{
		[JsonProperty("categoria_id")]
		public int CategoriaId { get; set; }

		[JsonProperty("categoria")]
		public string CategoriaNombre { get; set; }

		[JsonProperty("aportes")]
		public List<Aporte> Aportes { get; set; } = new List<Aporte>();
	}

	public class Bimestre
	{
		[JsonProperty("bimestre_id")]
		public int BimestreId { get; set; }

		[JsonProperty("bimestre")]
		public string BimestreNombre { get; set; }

		// El JSON entrega "categorias" como un objeto, no como un array.
		[JsonProperty("categorias")]
		private Dictionary<string, Categoria> CategoriasDict { get; set; } = new Dictionary<string, Categoria>();

		[JsonIgnore]
		public List<Categoria> Categorias
		{
			get { return new List<Categoria>(CategoriasDict.Values); }
		}
	}

	public class Estudiante
	{
		[JsonProperty("estudiante_id")]
		public int EstudianteId { get; set; }

		[JsonProperty("nombres")]
		public string Nombres { get; set; }

		[JsonProperty("apellidos")]
		public string Apellidos { get; set; }

		[JsonProperty("cedula")]
		public string Cedula { get; set; }

		// El JSON entrega "notas" como un objeto, no como un array.
		[JsonProperty("notas")]
		private Dictionary<string, Bimestre> NotasDict { get; set; } = new Dictionary<string, Bimestre>();

		[JsonIgnore]
		public List<Bimestre> Notas
		{
			get { return new List<Bimestre>(NotasDict.Values); }
		}
	}

	public class NotasCurso
	{
		[JsonProperty("curso")]
		public CursoDetalles Curso { get; set; }

		[JsonProperty("profesor")]
		public ProfesorDetalles Profesor { get; set; }

		[JsonProperty("estudiantes")]
		public List<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();
	}
}
