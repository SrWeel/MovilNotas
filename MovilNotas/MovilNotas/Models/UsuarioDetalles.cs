using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MovilNotas.Models
{
	public class UsuarioDetalles
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("cedula")]
		public string Cedula { get; set; }

		[JsonProperty("nombres")]
		public string Nombres { get; set; }

		[JsonProperty("apellidos")]
		public string Apellidos { get; set; }

		[JsonProperty("sexo")]
		public string Sexo { get; set; }

		[JsonProperty("nacionalidad")]
		public string Nacionalidad { get; set; }

		[JsonProperty("correo")]
		public string Correo { get; set; }

		[JsonProperty("estado")]
		public string Estado { get; set; }

		[JsonProperty("rol")]
		public string Rol { get; set; }
	}
}
