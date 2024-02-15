using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace GeneralApplication;

public static class Start
{
    public static void BuildGeneralApplication(IServiceCollection services)
    {
        services.RegisterGeneralServices();
    }
    public static void RegisterGeneralServices(this IServiceCollection services)
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