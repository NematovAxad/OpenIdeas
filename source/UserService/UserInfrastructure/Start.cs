namespace UserInfrastructure;
using Microsoft.Extensions.DependencyInjection;

public static class Start
{
    public static void InfrastructureBuild(this IServiceCollection services)
    {
        GeneralInfrastructure.Start.BuildgeneralInfrastructure(services);
    }
}