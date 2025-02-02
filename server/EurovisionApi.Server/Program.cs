
using EurovisionApi.Server.Database;

namespace EurovisionApi.Server;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddSingleton(await DataContext.CreateAsync(builder.Configuration));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseCors(options => options.AllowAnyOrigin());
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.MapStaticAssets();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.Map("api/{**slug}", HandleApiFallbackAsync); // Si no se encuentra la ruta en la API, devolver 404
        app.MapFallbackToFile("index.html"); // Configurar rutas para servir Angular

        await app.RunAsync();
    }

    private static Task HandleApiFallbackAsync(HttpContext context)
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;
        return context.Response.WriteAsync($"Cannot {context.Request.Method} {context.Request.Path}");
    }
}
