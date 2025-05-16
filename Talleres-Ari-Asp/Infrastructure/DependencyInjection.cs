using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Talleres_Ari_Asp.Infrastructure
{
    public static class DependencyInjection {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config) {
            services.AddScoped<IAuthService, AuthService>();
            services.Configure<JwtConfig>(config.GetSection("JwtConfig"));
            return services;
        }
}
}