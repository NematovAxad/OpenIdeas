using GeneralDomain;
using GeneralDomain.Configs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TestInfrastructure.DbContext;

namespace TestInfrastructure;

public static class Start
{
    public static void BuildInfrastructure(IServiceCollection services)
    {
        services.RegisterPostgres();
        services.RegisterJwt();
    }

    static void RegisterPostgres(this IServiceCollection services)
    {
        services.AddDbContext<DataContext>(options =>
        {
            options.UseNpgsql(Configs.TestDatabaseConnection).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
    }

    public static void RegisterJwt(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // указывает, будет ли валидироваться издатель при валидации токена
                    ValidateIssuer = true,
                    // строка, представляющая издателя
                    ValidIssuer = AuthOptions.Issuer,
                    // будет ли валидироваться потребитель токена
                    ValidateAudience = true,
                    // установка потребителя токена
                    ValidAudience = AuthOptions.Audience,
                    // будет ли валидироваться время существования
                    ValidateLifetime = false,
                    // установка ключа безопасности
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    // валидация ключа безопасности
                    ValidateIssuerSigningKey = true,
                };
            });
    }
}