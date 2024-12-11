using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace Rawos_OCME.Controllers
{
    public class DogController : Controller
    {
        private readonly HttpClient _httpClient;

        // Constructor que inyecta el HttpClient
        public DogController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Acción que obtiene una imagen aleatoria de perro desde la API
        [HttpGet("/")]  // Ruta principal de la aplicación
        public async Task<IActionResult> Home()
        {
            try
            {
                // Realizar la solicitud a la API de perros
                var response = await _httpClient.GetStringAsync("https://dog.ceo/api/breeds/image/random");

                // Parsear la respuesta JSON y obtener la URL de la imagen
                var imageUrl = JsonDocument.Parse(response)
                                        .RootElement
                                        .GetProperty("message")
                                        .GetString();

                // Pasar la URL de la imagen a la vista usando ViewData
                ViewData["ImageUrl"] = imageUrl;

                // Devolver la vista 'DogImage' ubicada en la carpeta 'Views/Dog'
                return View("DogImage");
            }
            catch (Exception ex)
            {
                // En caso de error, puedes mostrar un mensaje de error
                return Content($"Error al obtener la imagen: {ex.Message}");
            }
        }
    }
}