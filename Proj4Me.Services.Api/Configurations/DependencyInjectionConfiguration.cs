using Microsoft.Extensions.DependencyInjection;
using Proj4Me.Infra.CrossCutting.IoC;

namespace Proj4Me.Services.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDIConfiguration(this IServiceCollection services)
        {
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}