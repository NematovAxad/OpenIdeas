using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TestDomain.Repository;

namespace TestApplication;

public static class Start
{
    public static void ApplicationBuild(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(Start).Assembly);
        GeneralApplication.Start.BuildGeneralApplication(services);
        services.RegisterService();
        services.AddScoped<ICacheRepository, CacheRepository>();
    }
    
    public static void RegisterService(this IServiceCollection services)
    {
        Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(a => a.Name.EndsWith("Service") && !a.IsAbstract && !a.IsInterface)
            .Select(a => new { assignedType = a, serviceTypes = a.GetInterfaces().ToList() })
            .ToList()
            .ForEach(typesToRegister =>
            {
                typesToRegister.serviceTypes.ForEach(typeToRegister => services.AddScoped(typeToRegister, typesToRegister.assignedType));
            });
    }
}