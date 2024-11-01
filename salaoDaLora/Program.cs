using Microsoft.EntityFrameworkCore;
using salaoDaLora.Data;

namespace salaoDaLora
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //padrão MVC 
            builder.Services.AddControllersWithViews();

            //configuração banco de dados
            builder.Services.AddDbContext<SalaoDaLoraContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //roteamento pra abrir direto no index agendamento
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Agendamento}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
