using Microsoft.Extensions.DependencyInjection;

using OBD.FileService.Files.UseCases.Folders.Queries.GetFolderByIdQuery;

namespace OBD.FileService.Files.Integration;

public static class ServiceCollectionExtensionsFileModule
{
    public static IServiceCollection AddFilesModule(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetFolderByIdQuery).Assembly));

        return services;
    }
}
