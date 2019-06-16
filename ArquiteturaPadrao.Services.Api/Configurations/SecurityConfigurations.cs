using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using ArquiteturaPadrao.Infra.CrossCutting.Identity.Data;
using ArquiteturaPadrao.Infra.CrossCutting.Identity.Models;
using ArquiteturaPadrao.Infra.CrossCutting.JWT.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ArquiteturaPadrao.Services.Api.Configurations
{
    public static class SecurityConfiguration
    {
        public static void AddSecurity(this IServiceCollection services, IConfigurationRoot configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            //Add identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            var serviceProvider = services.BuildServiceProvider();
            var tokenConfiguration = serviceProvider.GetService<ITokenConfiguration>();

            //Add authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(bearerOptions =>
            {

                bearerOptions.SaveToken = true;

                bearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = tokenConfiguration.Issuer,
                    ValidAudience = tokenConfiguration.Audience,
                    IssuerSigningKey = tokenConfiguration.SymmetricKeySigningCredentials,
                    TokenDecryptionKey = tokenConfiguration.SymmetricKeyEncryptingCredentials
                };

                bearerOptions.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                        tokenConfiguration.Bearer = ((JwtSecurityToken)context.SecurityToken).RawData;
                        return Task.CompletedTask;
                    }
                };

            });

        }
    }
}
