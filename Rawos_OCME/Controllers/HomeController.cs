using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace Rawos_OCME.Controllers
{
    public class DogController : Controller
    {
        private readonly HttpClient _httpClient;

        
        public DogController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

       
        [HttpGet("/")]  // Ruta principal de la aplicación
        public async Task<IActionResult> Home()
        {
            try
            {
                
                var response = await _httpClient.GetStringAsync("https://dog.ceo/api/breeds/image/random");

               
                var imageUrl = JsonDocument.Parse(response)
                                        .RootElement
                                        .GetProperty("message")
                                        .GetString();

                ViewData["ImageUrl"] = imageUrl;

               
                return View("DogImage");
            }
            catch (Exception ex)
            {
                
                return Content($"Error al obtener la imagen: {ex.Message}");
            }
        }
    }
}
