using System.Text;
using Microsoft.IdentityModel.Tokens;

using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MNX.Application.UseCases.DI;

using OBD.FileService.Users.Core.Auth.Options;
using OBD.FileService.Users.UseCases.Auth;
using OBD.FileService.Users.UseCases.Queries;
using OBD.FileService.Users.UseCases.Commands.UpdateInitialsCommand;
using OBD.FileService.Users.Infrastructure;

namespace OBD.FileService.Users.Intergration;

public static class UsersCollectionExtensions
{
    public static IServiceCollection AddJwtBearerAuthentication(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserAccessor, UserAccessor>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllUsersQuery).Assembly));
        services.AddAutoMapper(cfg => cfg.AddProfile(typeof(AuthMappingProfile)));
        services.AddValidationPipelines(typeof(UserInitialsModelValidator).Assembly);

        services.Configure<PasswordOptions>(configuration.GetSection("PasswordOptions"));
        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

        var jwtConfig = configuration.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["Key"]!));

        services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtConfig["Jwt:Issuer"],
                        ValidAudience = jwtConfig["Jwt:Audience"],
                        IssuerSigningKey = key
                    };
                });

        services.AddHttpContextAccessor();
        return services;
    }
}
