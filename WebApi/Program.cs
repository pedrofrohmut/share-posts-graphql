using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace WebApi
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var host = CreateWebHostBuilder(args).Build();
      /* using (var scope = host.Services.CreateScope()) */
      /* { */
      /*   var servicesProvider = scope.ServiceProvider; */
      /*   try */
      /*   { */
      /*     var context = servicesProvider.GetRequiredService<SharePostsDbContext>(); */
      /*     context.Database.Migrate(); */
      /*     SharePostsDataSeed.InitializeDatabase(servicesProvider); */
      /*   } */
      /*   catch (Exception ex) */ 
      /*   { */
      /*     var logger = servicesProvider.GetRequiredService<ILogger<Program>>(); */
      /*     logger.LogError(ex, " ### An error occured seeding the database ### "); */
      /*   } */
      /* } */
      host.Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
  }
}
