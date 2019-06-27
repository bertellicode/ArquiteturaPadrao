using ArquiteturaPadrao.Infra.CrossCutting.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

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
