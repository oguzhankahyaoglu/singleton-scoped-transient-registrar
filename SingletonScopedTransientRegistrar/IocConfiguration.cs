using Microsoft.Extensions.DependencyInjection;

namespace SingletonScopedTransientRegistrar
{
    public static class IocConfiguration
    {
        public static void RegisterSingletonScopedTransientDependencies(this IServiceCollection services, string projectAssemblyPrefix)
        {
            services.Scan(scan =>
            {
                scan
                    .FromApplicationDependencies(a => a.GetName()?.Name?.StartsWith(projectAssemblyPrefix) == true)
                    .AddClasses(classes => classes.AssignableTo<ITransient>())
                    .AsSelfWithInterfaces()
                    .WithTransientLifetime()
                    .AddClasses(classes => classes.AssignableTo<IScoped>())
                    .AsSelfWithInterfaces()
                    .WithScopedLifetime()
                    .AddClasses(classes => classes.AssignableTo<ISingleton>())
                    .AsSelfWithInterfaces()
                    .WithSingletonLifetime();
            });
        }
    }
}