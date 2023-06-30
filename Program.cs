using Diploma;
using Diploma.Models;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        // Seed the database
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var dbContext = services.GetRequiredService<DiplomaContext>();
            DbInitializer.Seed(dbContext);
        }

        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}