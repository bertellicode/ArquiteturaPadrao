using ArquiteturaPadrao.Infra.CrossCutting.JWT.Configurations;
using ArquiteturaPadrao.Infra.CrossCutting.JWT.Interfaces;
using ArquiteturaPadrao.Infra.CrossCutting.JWT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;


namespace Security.Infra.CrossCutting.DependencyInjection
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {


            //// Infra - Data EventSourcing
            //services.AddScoped<IEventStoreRepository, EventStoreSQLRepository>();
            //services.AddScoped<IEventStore, SqlEventStore>();
            //services.AddScoped<EventStoreSQLContext>();

            // Infra - Identity
            services.AddScoped<IUserProvider, UserProvider>();

            // Infra - JWT
            services.AddScoped<ICredentialsConfiguration, CredentialsConfiguration>();
            services.AddScoped<ITokenConfiguration, TokenConfiguration>();

            // Web
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
