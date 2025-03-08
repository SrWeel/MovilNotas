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
		private readonly string BaseUrl = "http://192.168.1.2:8081/WebServicesNotas/api";

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

        // Método genérico para obtener listas
        private async Task<List<T>> ObtenerListaAsync<T>(string endpoint)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{BaseUrl}/{endpoint}");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<T>>(json);
                }
                return new List<T>();
            }
        }

        public Task<List<CategoriaSrweel>> ObtenerCategoriasAsync()
     => ObtenerListaAsync<CategoriaSrweel>("categorias");

        // Repite lo mismo con los demás modelos:
        public Task<List<BimestreSrweel>> ObtenerBimestresAsync()
            => ObtenerListaAsync<BimestreSrweel>("bimestres");

        public Task<List<MateriaSrweel>> ObtenerMateriasAsync(int idProfesor)
            => ObtenerListaAsync<MateriaSrweel>($"materias/{idProfesor}");

        public Task<List<JornadaSrweel>> ObtenerJornadasAsync()
            => ObtenerListaAsync<JornadaSrweel>("jornadas");

        public Task<List<NivelSrweel>> ObtenerNivelesAsync()
            => ObtenerListaAsync<NivelSrweel>("niveles");

        public Task<List<ParaleloSrweel>> ObtenerParalelosAsync()
            => ObtenerListaAsync<ParaleloSrweel>("paralelos");


        public async Task<bool> CrearAporteAsync(AporteSrweel aporte)
        {
            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(aporte);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{BaseUrl}/aportes", content);

                return response.IsSuccessStatusCode;
            }
        }



    }
}
