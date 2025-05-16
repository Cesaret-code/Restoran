using Microsoft.EntityFrameworkCore;
using Restoran.DAL;

namespace Restoran
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(opt=>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("default"));
            });


            builder.Services.AddControllersWithViews();


            var app = builder.Build();

            app.UseStaticFiles();
            
            app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=DashBoard}/{action=Index}/{id?}"
            );


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}"
                );


            app.Run();
        }
    }
}
