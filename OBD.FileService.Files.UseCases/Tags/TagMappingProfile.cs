using AutoMapper;

using OBD.FileService.Files.Core;

namespace OBD.FileService.Files.UseCases.Tags;

public class TagMappingProfile : Profile
{
    public TagMappingProfile()
    {
        CreateMap<TagModel, Tag>();
    }
}
