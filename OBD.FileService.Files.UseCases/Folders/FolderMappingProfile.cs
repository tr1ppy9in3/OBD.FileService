
using AutoMapper;

using OBD.FileService.Files.Core;

namespace OBD.FileService.Files.UseCases.Folders;

public class FolderMappingProfile : Profile
{
    public FolderMappingProfile()
    {
        CreateMap<FolderInputModel, Folder>();
    }
}
