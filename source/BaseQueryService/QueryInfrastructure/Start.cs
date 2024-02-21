using Microsoft.Extensions.DependencyInjection;

namespace QueryInfrastructure;

public static class Start
{
    public static void InfrastructureBuild(this IServiceCollection services)
    {
        GeneralInfrastructure.Start.BuildgeneralInfrastructure(services);
    }
}