using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();

var app = builder.Build();

app.MapGet("/random-dog", async (HttpClient httpClient) =>
{
    try
    {
        var response = await httpClient.GetStringAsync("https://dog.ceo/api/breeds/image/random");
        var data = JsonSerializer.Deserialize<JsonElement>(response);
        var imageUrl = data.GetProperty("message").GetString();
        return Results.Ok(new { ImageUrl = imageUrl });
    }
    catch (Exception ex)
    {
        return Results.Problem($"FALLA: {ex.Message}");
    }
});


app.MapGet("/", () =>
{
    return Results.Content(@"
        <html>
            <head>
                <title>Imagen Aleatoria de Perro</title>
            </head>
            <body>
                <h1>Imagen Aleatoria de Perro</h1>
                <button id='randomDogBtn'>Obtener Perro Aleatorio</button>
                <br><br>
                <img id='dogImage' src='' alt='Imagen de Perro' style='max-width: 100%; height: auto;' />
                <script>
                  
                    document.getElementById('randomDogBtn').onclick = async function() {
                        const response = await fetch('/random-dog');
                        const data = await response.json();
                        document.getElementById('dogImage').src = data.ImageUrl;
                    };
                </script>
            </body>
        </html>
    ", "text/html");
});

app.Run();