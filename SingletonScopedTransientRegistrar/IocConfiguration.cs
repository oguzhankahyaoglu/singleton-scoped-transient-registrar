using System;
using Microsoft.Extensions.DependencyInjection;

namespace SingletonScopedTransientRegistrar
{
    public static class IocConfiguration
    {
        public class Options
        {
            public string ProjectAssemblyPrefix { get; set; }
            
            /// <summary>
            /// Alternative to <see cref="ITransient"/>
            /// </summary>
            public Type[] TransientType { get; set; }
            
            /// <summary>
            /// Alternative to <see cref="IScoped"/>
            /// </summary>
            public Type[] ScopedType { get; set; }
            
            /// <summary>
            /// Alternative to <see cref="ISingleton"/>
            /// </summary>
            public Type[] SingletonType { get; set; }
            
            
        }
        public static void RegisterSingletonScopedTransientDependencies(this IServiceCollection services, Options options)
        {
            services.Scan(scan =>
            {
                var scanResult = scan
                    .FromApplicationDependencies(a => a.GetName()?.Name?.StartsWith(options.ProjectAssemblyPrefix) == true);
                var withTransientLifetime = scanResult
                    .AddClasses(classes => classes.AssignableTo<ITransient>())
                    .AsSelfWithInterfaces()
                    .WithTransientLifetime();
                foreach (var type in options.TransientType ?? new Type[0])
                {
                    scanResult
                        .AddClasses(classes => classes.AssignableTo(type))
                        .AsSelfWithInterfaces()
                        .WithTransientLifetime();
                }
                
                var withScopedLifetime = withTransientLifetime
                    .AddClasses(classes => classes.AssignableTo<IScoped>())
                    .AsSelfWithInterfaces()
                    .WithScopedLifetime();
                foreach (var type in options.ScopedType ?? new Type[0])
                {
                    scanResult
                        .AddClasses(classes => classes.AssignableTo(type))
                        .AsSelfWithInterfaces()
                        .WithScopedLifetime();
                }
                
                var withSingletonLifetime = withScopedLifetime
                    .AddClasses(classes => classes.AssignableTo<ISingleton>())
                    .AsSelfWithInterfaces()
                    .WithSingletonLifetime();
                foreach (var type in options.SingletonType ?? new Type[0])
                {
                    scanResult
                        .AddClasses(classes => classes.AssignableTo(type))
                        .AsSelfWithInterfaces()
                        .WithSingletonLifetime();
                }
            });
        }
    }
}