using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

builder.Services.AddControllersWithViews();

var app = builder.Build();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dog}/{action=Home}/{id?}"); // Redirige a la acción 'Home' de DogController
app.Run();
