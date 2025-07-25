
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

namespace PharmaTrack
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDependency(builder.Configuration);
            builder.Services.AddScoped<ILowStockNotifier, SignalRLowStockNotifier>();

            var app = builder.Build();

            await app.ConfigureMiddlewareServices();
            // Seed Data
            using (var scope = app.Services.CreateScope())
            {
                DataSeeder.Seed(scope.ServiceProvider);
            }

            app.Run();
        }
    }
}
