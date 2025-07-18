using Persistence.Extensions;
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
        }

        public static void AddSwaggerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }
}
