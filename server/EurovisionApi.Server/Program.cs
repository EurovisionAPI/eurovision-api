using EurovisionApi.Server.Database;

namespace EurovisionApi.Server;

public class Program
{
    public static async Task Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        if (builder.Environment.IsDevelopment())
        {
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
        }

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.Configure<Settings>(builder.Configuration.GetSection(nameof(Settings)));
        builder.Services.AddSingleton<DataContext>();

        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseCors(options => options.AllowAnyOrigin());
        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapStaticAssets();
        app.MapControllers();
        app.Map("api/{**slug}", HandleApiFallbackAsync); // Si no se encuentra la ruta en la API, devolver 404
        app.MapFallbackToFile("index.html"); // Configurar rutas para servir Angular

        await InitDatabaseAsync(app.Services);

        await app.RunAsync();
    }

    private static IResult HandleApiFallbackAsync(HttpContext context)
    {
        return Results.NotFound($"Cannot {context.Request.Method} {context.Request.Path}");
    }

    private static Task InitDatabaseAsync(IServiceProvider serviceProvider)
    {
        DataContext dataContext = serviceProvider.GetRequiredService<DataContext>();
        return dataContext.DownloadDatasetAsync();
    }
}
