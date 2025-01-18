using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MNX.Application.Data.DI;

using OBD.FileService.DataAccess.Files;
using OBD.FileService.Files.UseCases.Files;
using OBD.FileService.Files.UseCases.Folders;

using OBD.FileService.DataAccess.Users;
using OBD.FileService.DataAccess.Users.Auth;
using OBD.FileService.Users.UseCases;
using OBD.FileService.Users.UseCases.Auth;
using OBD.FileService.Files.UseCases.Tags.Abstractions;

namespace OBD.FileService.DataAccess.Integration;

public static class ServiceCollectionDataExtesnsions
{
    public static IServiceCollection AddDataAccessModule(this IServiceCollection services, IConfigurationManager configuration)
    {
        services.AddDataContext<Context>(configuration)
                .AddUsersModuleRepositories()
                .AddFilesModuleRepositories();

        return services;
    }

    private static IServiceCollection AddUsersModuleRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IRoleRepostiory, RoleRepository>()
                .AddScoped<ITokenRepository, TokenRepository>();

        return services;
    }

    private static IServiceCollection AddFilesModuleRepositories(this IServiceCollection services)
    {
        services.AddScoped<IFileRepository, FileRepository>()
                .AddScoped<IFolderRepository, FolderRepository>()
                .AddScoped<ITagRepository, TagRepository>();

        return services;
    }
}
