using AutoMapper;

using OBD.FileService.Files.Core;
using OBD.FileService.Files.UseCases.Tags.Models;

namespace OBD.FileService.Files.UseCases.Tags;

public class TagMappingProfile : Profile
{
    public TagMappingProfile()
    {
        CreateMap<TagInputModel, Tag>();
        CreateMap<Tag, TagOutputModel>();
    }
}
