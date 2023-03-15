using Microsoft.Extensions.DependencyInjection;

namespace CarTestTask.Repository
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ICarRepository, CarRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
