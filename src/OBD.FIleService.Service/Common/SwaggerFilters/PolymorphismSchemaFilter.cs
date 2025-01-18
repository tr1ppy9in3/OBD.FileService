using Microsoft.OpenApi.Models;

using OBD.FileService.Files.UseCases.FileSystem.Models;
using OBD.FileService.Files.UseCases.FileSystem.Models.Abstractions;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace OBD.FileService.Service.Common.SwaggerFilters;

public class PolymorphismSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(BaseFileSystemObject))
        {

            schema.Discriminator = new OpenApiDiscriminator
            {
                PropertyName = "type"
            };


            schema.OneOf = new List<OpenApiSchema>
            {
                context.SchemaGenerator.GenerateSchema(typeof(FolderModel), context.SchemaRepository),
                context.SchemaGenerator.GenerateSchema(typeof(FileModel), context.SchemaRepository)
            };
        }
    }
}