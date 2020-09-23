using Microsoft.Extensions.DependencyInjection;
using PpmApi.DI;

namespace SingletonScopedTransientRegistrar
{
    public static class IocConfiguration
    {
        public static void RegisterSingletonScopedTransientDependencies(IServiceCollection services, string projectAssemblyPrefix)
        {
            services.Scan(scan =>
            {
                scan
                    .FromApplicationDependencies(a => a.GetName()?.Name?.StartsWith(projectAssemblyPrefix) == true)
                    .AddClasses(classes => classes.AssignableTo<ITransientDependency>())
                    .AsSelfWithInterfaces()
                    .WithTransientLifetime()
                    .AddClasses(classes => classes.AssignableTo<IScopedDependency>())
                    .AsSelfWithInterfaces()
                    .WithScopedLifetime()
                    .AddClasses(classes => classes.AssignableTo<ISingletonDependency>())
                    .AsSelfWithInterfaces()
                    .WithSingletonLifetime();
            });
        }
    }
}