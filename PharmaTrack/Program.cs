
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Repositories;
using Service.Abstractions.Repositories;
using Service.Abstractions.Services;
using Services.Services;

namespace PharmaTrack
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddDbContext<PharmaProjectContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("cs")));
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


            builder.Services.AddScoped<IStockRepository, StockRepository>();
            builder.Services.AddScoped<IMedicineBatchRepository, MedicineBatchRepository>();


            builder.Services.AddScoped<IStockService, StockService>();
            builder.Services.AddScoped<IMedicineService, MedicineService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
