using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Registrar el servicio HttpClient para hacer solicitudes HTTP
builder.Services.AddHttpClient();

// Agregar controladores
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configurar las rutas para los controladores
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dog}/{action=Home}/{id?}"); // Redirige a la acción 'Home' de DogController

// Ejecutar la aplicación
app.Run();