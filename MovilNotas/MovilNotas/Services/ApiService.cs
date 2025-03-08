using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using MovilNotas.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MovilNotas.Services
{
	public class ApiService
	{
		private readonly string BaseUrl = "http://192.168.1.3/WebServicesNotas/api";

		public async Task<LoginResponse> LoginAsync(string correo, string password)
		{
			try
			{
				using (HttpClient client = new HttpClient())
				{
					var datos = new { correo = correo, password = password };
					string json = JsonConvert.SerializeObject(datos);
					StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

					HttpResponseMessage response = await client.PostAsync($"{BaseUrl}/login", content);
					string respuestaJson = await response.Content.ReadAsStringAsync();

					Console.WriteLine($"Respuesta del servidor: {respuestaJson}");

					if (response.IsSuccessStatusCode)
					{
						var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(respuestaJson);

						if (data.ContainsKey("mensaje") && data["mensaje"].ToString() == "Inicio de sesión exitoso")
						{
							int id = Convert.ToInt32(data["id"]);
							string correoUsuario = data["correo"].ToString();

							// Obtener los cursos del usuario con su ID
							var cursos = await ObtenerCursosAsync(id);

							return new LoginResponse
							{
								Success = true,
								Message = data["mensaje"].ToString(),
								Id = id,
								Correo = correoUsuario,
								Cursos = cursos
							};
						}
						else
						{
							return new LoginResponse { Success = false, Message = "Usuario no encontrado." };
						}
					}
					else
					{
						var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(respuestaJson);
						if (data.ContainsKey("error"))
						{
							return new LoginResponse { Success = false, Message = data["error"].ToString() };
						}
						return new LoginResponse { Success = false, Message = "Error desconocido." };
					}
				}
			}
			catch (Exception ex)
			{
				return new LoginResponse { Success = false, Message = $"Error inesperado: {ex.Message}" };
			}
		}

		private async Task<List<CursoFlat>> ObtenerCursosAsync(int idProfesor)
		{
			try
			{
				using (HttpClient client = new HttpClient())
				{
					HttpResponseMessage response = await client.GetAsync($"{BaseUrl}/cursos/flat?id_profesor={idProfesor}");
					string respuestaJson = await response.Content.ReadAsStringAsync();

					Console.WriteLine($"Respuesta JSON: {respuestaJson}"); // 🔹 Depuración

					if (response.IsSuccessStatusCode)
					{
						var cursos = JsonConvert.DeserializeObject<List<CursoFlat>>(respuestaJson);
						return cursos;
					}
					else
					{
						Console.WriteLine($"Error en la respuesta: {response.StatusCode} - {respuestaJson}");
						return new List<CursoFlat>();
					}
				}
			}
			catch (HttpRequestException httpEx)
			{
				Console.WriteLine($"Error de conexión: {httpEx.Message}");
			}
			catch (JsonException jsonEx)
			{
				Console.WriteLine($"Error al procesar JSON: {jsonEx.Message}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error inesperado: {ex.Message}");
			}

			return new List<CursoFlat>();
		}
		public async Task<NotasCurso> ObtenerNotasCursoAsync(int profesorMateriaId)
		{
			try
			{
				using (HttpClient client = new HttpClient())
				{
					HttpResponseMessage response = await client.GetAsync($"{BaseUrl}/cursos/{profesorMateriaId}/notas");
					string respuestaJson = await response.Content.ReadAsStringAsync();

					Console.WriteLine($"Respuesta JSON: {respuestaJson}"); // 🔹 Para depuración

					if (response.IsSuccessStatusCode)
					{
						return JsonConvert.DeserializeObject<NotasCurso>(respuestaJson);
					}
					return null;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error al obtener notas: {ex.Message}");
				return null;
			}
		}
		public async Task<UsuarioDetalles> ObtenerUsuarioAsync(int idUsuario)
		{
			try
			{
				using (HttpClient client = new HttpClient())
				{
					HttpResponseMessage response = await client.GetAsync($"{BaseUrl}/usuarios/{idUsuario}");
					string respuestaJson = await response.Content.ReadAsStringAsync();

					Console.WriteLine($"Respuesta JSON: {respuestaJson}"); // 🔹 Para depuración

					if (response.IsSuccessStatusCode)
					{
						return JsonConvert.DeserializeObject<UsuarioDetalles>(respuestaJson);
					}
					return null;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error al obtener usuario: {ex.Message}");
				return null;
			}
		}

        public async Task<bool> CrearAporteAsync(int idCategoria, int idBimestre, string titulo, string descripcion, int idMateria, int idJornada, int idNivel, int idParalelo)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var data = new
                    {
                        id_categoria = idCategoria,
                        id_bimestre = idBimestre,
                        titulo = titulo,
                        descripcion = descripcion,
                        id_materia = idMateria,
                        id_jornada = idJornada,
                        id_nivel = idNivel,
                        id_paralelo = idParalelo
                    };
                    string json = JsonConvert.SerializeObject(data);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync($"{BaseUrl}/aportes", content);

                    // Retornar true si la respuesta es exitosa (201)
                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al crear aporte: " + ex.Message);
                return false;
            }
        }





    }
}
