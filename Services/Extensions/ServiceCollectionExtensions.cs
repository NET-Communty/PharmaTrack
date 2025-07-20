using Microsoft.Extensions.DependencyInjection;
using Service.Abstractions.IServices;
using Service.Abstractions.Services;
using Services.AutoMapper.CategoryAtoMapper;
using Services.AutoMapper.MedicineAtoMapper;
using Services.AutoMapper.SupplierAtoMapper;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            
            services.AddServiceLayerServices();
            services.AddAutoMapperServices();

        }

        private static void AddServiceLayerServices(this IServiceCollection services)
        {
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IMedicineSercive, MedicineSercive>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISupplierService, SupplierService>();


        }

        private static void AddAutoMapperServices(this IServiceCollection services)
        {
           services.AddAutoMapper(map => map.AddProfile(new CategoryMappingProfile()));
           services.AddAutoMapper(map => map.AddProfile(new MedicineMappingProfile()));
            services.AddAutoMapper(map => map.AddProfile(new SupplierMappingProfile()));
        }
    }
}
