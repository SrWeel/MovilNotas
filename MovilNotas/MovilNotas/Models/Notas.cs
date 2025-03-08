using System;
using System.Text;
using System.Collections.Generic;

namespace MovilNotas.Models
{
	public class NotaResponse
	{
		public CursoDetalle Curso { get; set; }
		public ProfesorDetalle Profesor { get; set; }
		public List<EstudianteDetalle> Estudiantes { get; set; }
	}

	public class CursoDetalle
	{
		public string Materia { get; set; }
		public string Jornada { get; set; }
		public string Paralelo { get; set; }
		public string Nivel { get; set; }
		public string Carrera { get; set; }
	}

	public class ProfesorDetalle
	{
		public string Nombres { get; set; }
		public string Apellidos { get; set; }
		public string Cedula { get; set; }
		public string Correo { get; set; }
	}

	public class EstudianteDetalle
	{
		public int EstudianteId { get; set; }
		public string Nombres { get; set; }
		public string Apellidos { get; set; }
		public string Cedula { get; set; }
		public Dictionary<int, BimestreNotas> Notas { get; set; }
	}

	public class BimestreNotas
	{
		public int BimestreId { get; set; }
		public string Bimestre { get; set; }
		public Dictionary<int, CategoriaNotas> Categorias { get; set; }
	}

	public class CategoriaNotas
	{
		public int CategoriaId { get; set; }
		public string Categoria { get; set; }
		public List<AporteDetalle> Aportes { get; set; }
	}

	public class AporteDetalle
	{
		public int NotaId { get; set; }
		public string Nota { get; set; }
		public string Titulo { get; set; }
		public string Descripcion { get; set; }
	}
}

