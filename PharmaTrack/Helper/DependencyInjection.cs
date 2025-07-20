using Persistence.Extensions;
using PharmaTrack.Middlewares;
using Services.Extensions;

namespace PharmaTrack.Helper
{
    public static class DependencyInjection
    {
        public static void AddDependency(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddControllers();
            services.AddPersistenceServices(configuration);
            services.AddSwaggerServices();
            services.AddApplicationServices();
            services.RegisterMiddleware();

        }

        private static void AddSwaggerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        private static void RegisterMiddleware(this IServiceCollection services)
        {
            services.AddScoped<ExceptionMiddleware>();
        }
    }
}
