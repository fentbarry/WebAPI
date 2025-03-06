using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication1.BLL.Services.Account;
using WebApplication1.BLL.Services.Role;
using WebApplication1.DAL;
using WebApplication1.DAL.Init;
using static WebApplication1.DAL.Models.AppIdentity;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IRoleService, RoleService>();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            // builder.Services.AddOpenApi();

            // Add Db Context
            builder.Services.AddDbContext<AppDbContext>(p => {
                p.UseNpgsql("name=PostgresLocal");
            });


            // Add Identity
            builder.Services
                .AddIdentity<AppUser, AppRole>(options =>
                {
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireDigit = false;
                })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                //app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Seed();

            app.Run();
        }
    }
}
