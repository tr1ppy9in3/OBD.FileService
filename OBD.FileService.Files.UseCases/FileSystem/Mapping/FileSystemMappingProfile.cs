using AutoMapper;

using OBD.FileService.Files.Core;
using OBD.FileService.Files.UseCases.FileSystem.Commands.Object.CreateObject.CreateFileObject;
using OBD.FileService.Files.UseCases.FileSystem.Commands.Object.CreateObject.CreateFolderObject;
using OBD.FileService.Files.UseCases.FileSystem.Models;
using OBD.FileService.Files.UseCases.FileSystem.Models.Abstractions;

namespace OBD.FileService.Files.UseCases.FileSystem.Mapping;

public class FileSystemMappingProfile : Profile
{
    public FileSystemMappingProfile()
    {
        CreateMap<Core.File, FileModel>()
            .ForMember(file => file.Name, opts => opts
            .MapFrom(src => $"{src.Name}{src.Extension}"))

            .ForMember(file => file.SizeInBytes, opts => opts
            .MapFrom(src => src.LastVersion!.SizeBytes))

            .ForMember(file => file.CreatedAt, opts => opts
            .MapFrom(src => src.LastVersion!.CreatedAt))

            .ForMember(file => file.Tags, opts => opts
            .MapFrom(src => src.Tags));

        CreateMap<Folder, FolderModel>()
            .ForMember(folder => folder.SizeInBytes, opts => opts
            .MapFrom(src => src.TotalSizeBytes))

            .ForMember(dest => dest.Content, opts => opts
            .MapFrom(src => CombineFilesAndFolders(src.AttachedFiles, src.AttachedFolders)))

            .ForMember(file => file.Tags, opts => opts
            .MapFrom(src => src.Tags));

        CreateMap<CreateFolderObjectCommandModel, Folders.FolderInputModel>()
            .ForMember(folder => folder.ParentFolderId, opts => opts
            .MapFrom(src => src.ParentFolderId))

            .ForMember(folder => folder.Name, opts => opts
            .MapFrom(src => src.Name));

        CreateMap<CreateFileObjectCommandModel, Files.FileInputModel>()
            .ForMember(file => file.ParentFolderId, opts => opts
            .MapFrom(src => src.ParentFolderId))

            .ForMember(file => file.Description, opts => opts
            .MapFrom(src => src.Description))

            .ForMember(file => file.Form, opts => opts
            .MapFrom(src => src.Form));

    }

    private IEnumerable<BaseFileSystemObject> CombineFilesAndFolders(List<Core.File> files, List<Folder> folders)
    {
        var fileModels = files.Select(file => new FileModel
        {
            Id = file.Id,
            Name = file.Name,
            IsFavorite = file.IsFavorite,
            CreatedAt = file.LastVersion!.CreatedAt,
            SizeInBytes = file.LastVersion!.SizeBytes,
        }).Cast<BaseFileSystemObject>();

        var folderModels = folders.Select(folder => new FolderModel
        {
            Id = folder.Id,
            Name = folder.Name,
            IsFavorite = folder.IsFavorite,
            CreatedAt = folder.CreatedAt
        }).Cast<BaseFileSystemObject>();

        return fileModels.Concat(folderModels);
    }
}