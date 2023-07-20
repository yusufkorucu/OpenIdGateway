using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using OpenIdGateway.Core.Services;
using OpenIdGateway.Core.Validator;
using OpenIdGateway.Domain.RequestModels;
using System.Reflection;

namespace OpenIdGateway.Core
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection collection)
        {
            collection.AddScoped<IIntegrationService, IntegrationService>();
            collection.AddFluentValidation(x => x.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
            collection.AddScoped<IValidator<OpenIdConnectRequestDto>, OpenIdRequestValidator>();

        }
    }
}
