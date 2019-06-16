using Microsoft.Extensions.DependencyInjection;
using Security.Infra.CrossCutting.DependencyInjection;

namespace ArquiteturaPadrao.Services.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDIConfiguration(this IServiceCollection services)
        {
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
