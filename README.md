# singleton-scoped-transient-registrar
Common library for registering any component using ISingletonDependency, IScopedDependency and ITransientDependency

# Usage
Register your services like:
````
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterSingletonScopedTransientDependencies("Ppm");
````

Then use these interfaces, and tada! DI works!
````
    public interface IMetaDataService : IScopedDependency
    {

````
