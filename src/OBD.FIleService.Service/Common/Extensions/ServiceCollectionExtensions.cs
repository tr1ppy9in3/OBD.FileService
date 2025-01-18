using Microsoft.OpenApi.Models;
using OBD.FileService.Files.UseCases.FileSystem.Models;

using OBD.FileService.Service.Common.SwaggerFilters;
using OBD.FIleService.Service.Common.SwaggerFilters;
using System.Reflection;

namespace OBD.FIleService.Service.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public new static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            options.OperationFilter<SecurityRequirementsOperationFilter>();
            options.OperationFilter<AuthorizeCheckOperationFilter>();

            options.SchemaFilter<PolymorphismSchemaFilter>();

            var basePath = AppContext.BaseDirectory;
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(basePath, xmlFile);

            options.IncludeXmlComments(xmlPath);

            options.UseAllOfToExtendReferenceSchemas();
            options.UseAllOfForInheritance();
            options.UseOneOfForPolymorphism();

            options.UseInlineDefinitionsForEnums();

            options.CustomSchemaIds(type => type.FullName);
        });

        return services;
    }

    public static IServiceCollection AddCorsDefault(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
        });

        return services;
    }

    public static IServiceCollection AddAuthorizationDefault(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
                .AddPolicy("AdminPolicy", policy =>
                {
                    policy.RequireRole("Admin");
                })
                .AddPolicy("RegularUserPolicy", policy =>
                {
                    policy.RequireRole("RegularUser");
                });

        return services;
    }
}
