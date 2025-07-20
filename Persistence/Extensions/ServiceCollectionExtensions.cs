using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Repositories;
using Service.Abstractions.IServices;
using Service.Abstractions.Repositories;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
           
            services.AddDbContextServices(configuration);
            services.AddPersistenceRepositories();
        }

        private static void AddDbContextServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PharmaProjectContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("RemoteConnection")));
        }

        private static void AddPersistenceRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IMedicineBatchRepository, MedicineBatchRepository>();
            services.AddScoped<IMedicineRepository, MedicineRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
           services.AddScoped<ISupplierRepository, SupplierRepository>();


        }
    }
}
