

using ArquiteturaPadrao.Application.Interfaces;
using ArquiteturaPadrao.Application.Services;
using ArquiteturaPadrao.Domain.Core.Bus;
using ArquiteturaPadrao.Domain.Core.Events;
using ArquiteturaPadrao.Domain.Core.Interfaces;
using ArquiteturaPadrao.Domain.Core.Notifications;
using ArquiteturaPadrao.Domain.CustomerAggregate.CommandHandlers;
using ArquiteturaPadrao.Domain.CustomerAggregate.Commands;
using ArquiteturaPadrao.Domain.CustomerAggregate.EventHandlers;
using ArquiteturaPadrao.Domain.CustomerAggregate.Events;
using ArquiteturaPadrao.Domain.CustomerAggregate.Interfaces;
using ArquiteturaPadrao.Infra.CrossCutting.Bus;
using ArquiteturaPadrao.Infra.CrossCutting.JWT.Configurations;
using ArquiteturaPadrao.Infra.CrossCutting.JWT.Interfaces;
using ArquiteturaPadrao.Infra.CrossCutting.JWT.Models;
using ArquiteturaPadrao.Infra.CrossCutting.Providers;
using ArquiteturaPadrao.Infra.CrossCutting.Providers.Interfaces;
using ArquiteturaPadrao.Infra.Data.Context;
using ArquiteturaPadrao.Infra.Data.EventSourcing;
using ArquiteturaPadrao.Infra.Data.Repository;
using ArquiteturaPadrao.Infra.Data.Repository.EventSourcing;
using ArquiteturaPadrao.Infra.Data.UoW;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ArquiteturaPadrao.Infra.CrossCutting.DependencyInjection
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Application
            services.AddScoped<ICustomerAppService, CustomerAppService>();

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<CustomerRegisteredEvent>, CustomerEventHandler>();
            services.AddScoped<INotificationHandler<CustomerUpdatedEvent>, CustomerEventHandler>();
            services.AddScoped<INotificationHandler<CustomerRemovedEvent>, CustomerEventHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<RegisterNewCustomerCommand, bool>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCustomerCommand, bool>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveCustomerCommand, bool>, CustomerCommandHandler>();

            //// Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreSQLRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
            services.AddScoped<EventStoreSQLContext>();

            // Infra - Data
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ArquiteturaPadraoContext>();

            // Infra - Identity
            services.AddScoped<IUserProvider, UserProvider>();

            // Infra - JWT
            services.AddScoped<ICredentialsConfiguration, CredentialsConfiguration>();
            services.AddScoped<ITokenConfiguration, TokenConfiguration>();

            // Web
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Providers
            services.AddSingleton<IPathProvider, PathProvider>();
        }
    }
}
