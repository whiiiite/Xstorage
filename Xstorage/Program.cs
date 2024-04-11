using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Xstorage.Data;
using Xstorage.Entities.Models;
using Xstorage.Repositories;
using Xstorage.Services;
using Xstorage.Services.Subscription;
using Xstorage.Shared;

namespace Xstorage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<XstorageDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("XstorageDbContext") ?? 
                throw new InvalidOperationException("Connection string 'XstorageDbContext' not found.")));

            builder.Services.AddIdentityCore<User>(options => {
                options.SignIn.RequireConfirmedAccount = true;
                options.User.RequireUniqueEmail = true;
                options.Password = new PasswordOptions()
                {
                    RequireDigit = false,
                    RequiredLength = 8,
                    RequireLowercase = true,
                    RequireUppercase = false,
                    RequireNonAlphanumeric = false,
                };
            })
           .AddRoles<IdentityRole>()
           .AddEntityFrameworkStores<XstorageDbContext>();


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsAllowAll_NONSECURE",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
                options.AddPolicy("CorsServerOnly",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:5028",
                                            "https://localhost:7201")
                               .WithMethods("GET", "POST")
                               .AllowAnyHeader();
                    });
            });

            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<JwtHandler>();
            builder.Services.AddScoped<StorageRepository>();
            builder.Services.AddScoped<SubscriptionRepository>();
            builder.Services.AddScoped<StorageService>();
            builder.Services.AddScoped<SubscriptionService>();

            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(7);
                options.SlidingExpiration = true;
                options.AccessDeniedPath = "/Forbidden/";
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                        .GetBytes(jwtSettings.GetSection("securityKey").Value))
                };
            });



            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}