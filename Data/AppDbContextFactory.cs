// using EcommerceApi;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Design;

// public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
// {
//     public AppDbContext CreateDbContext(string[] args)
//     {
//         Console.WriteLine("=== AppDbContextFactory används ===");
        
//         var configuration = new ConfigurationBuilder()
//             .AddUserSecrets<Program>()
//             .Build();

//         Console.WriteLine($"=== Connection: {configuration.GetConnectionString("DefaultConnection")} ===");

//         var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
//         optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

//         return new AppDbContext(optionsBuilder.Options);
//     }
// }