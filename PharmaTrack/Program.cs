
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Repositories;
using Microsoft.OpenApi.Models; 
using Service.Abstractions.IServices;
using Service.Abstractions.Services;
using Services.AutoMapper.CategoryAtoMapper;
using Services.Services;
using Services.AutoMapper.MedicineAtoMapper;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using PharmaTrack.Helper;
using System.Threading.Tasks;
using Persistence.DataSeeding;
using PharmaTrack.Notifications;
using Service.Abstractions.Notifications;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Persistence.AdminSeedData;
using Domain.SeedData;

namespace PharmaTrack
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDependency(builder.Configuration);
            builder.Services.AddScoped<ILowStockNotifier, SignalRLowStockNotifier>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                option.Password.RequireUppercase=true;
            }).AddEntityFrameworkStores<PharmaProjectContext>().AddDefaultTokenProviders();
            builder.Services.AddScoped<IAccountServices, AccountServices>();
        
            
            //[authorize]
            builder.Services.AddAuthentication(Options =>
            {
                Options.DefaultAuthenticateScheme="JWT";
                Options.DefaultChallengeScheme="JWT";
            }).AddJwtBearer("JWT", Options =>
            {
                //secrete key
                var SecretKeyString = builder.Configuration.GetValue<string>("SecratKey");
                var SecreteKeyBytes = Encoding.ASCII.GetBytes(SecretKeyString);
                SecurityKey securityKey = new SymmetricSecurityKey(SecreteKeyBytes);
                //--------------------------------------------------------------

                Options.TokenValidationParameters=new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    IssuerSigningKey=securityKey,
                    ValidateIssuer=false,
                    ValidateAudience=false,
                    ClockSkew=TimeSpan.Zero
                };
            });

            var app = builder.Build();
            await app.ConfigureMiddlewareServices();
            using (var scope = app.Services.CreateScope())
            {
                DataSeeder.Seed(scope.ServiceProvider);
                var services = scope.ServiceProvider;
                await IdentitySeedData.SeedRolesAsync(services);
                await SeedData.SeedAdminAsync(services);
            }
            app.Run();
        }
    }
}
