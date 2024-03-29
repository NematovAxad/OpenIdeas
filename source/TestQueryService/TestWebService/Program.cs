
using GeneralApplication.Extensions;
using GeneralDomain.Configs;
using GeneralDomain.Extensions;
using Microsoft.OpenApi.Models;
using TestApplication;

public class Programm
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        RegisterAppConfig(builder.Configuration);
        builder.WebHost.UseUrls("http://*:5004");
        builder.Services.ApplicationBuild();
        TestInfrastructure.Start.BuildInfrastructure(builder.Services);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.OperationFilter<SwaggerSkipProperty>();
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
          Enter 'Bearer' [space] and then your token in the text input below.
          \r\n\r\nExample: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
        });
        
        builder.Services.AddHttpContextAccessor();

        var app = builder.Build();
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        if (app.Services.GetService<IHttpContextAccessor>() != null)
            HttpContextHelper.Accessor = app.Services.GetRequiredService<IHttpContextAccessor>();

        app.Run();
    }

    private static void RegisterAppConfig(ConfigurationManager configuration)
    {
        Configs.TestDatabaseConnection = configuration["ConnectionStrings:PostgresTestConnectionString"];
        
        Configs.SearchUrlOne = configuration["ConnectionStrings:SearchUrlOne"];
        Configs.SearchUrlTwo = configuration["ConnectionStrings:SearchUrlTwo"];
        Configs.CheckUrlOne = configuration["ConnectionStrings:CheckUrlOne"];
        Configs.CheckUrlTwo = configuration["ConnectionStrings:CheckUrlTwo"];
        
        Configs.RedisHost = configuration["RedisConnection:Host"];
        Configs.RedisPort = configuration["RedisConnection:Port"];
        Configs.RedisIsSsl = Convert.ToBoolean(configuration["RedisConnection:IsSSL"]);
        Configs.RedisPassword = configuration["RedisConnection:Password"];
    }
}