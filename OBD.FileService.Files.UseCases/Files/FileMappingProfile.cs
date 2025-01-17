using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;

using OBD.FileService.Files.Core;

namespace OBD.FileService.Files.UseCases.Files;

public class FileMappingProfile : Profile
{
    public FileMappingProfile()
    {
        CreateMap<FileInputModel, Core.File>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => Path.GetFileNameWithoutExtension(src.Form.FileName))) // Извлекаем имя без расширения
            .ForMember(dest => dest.Extension, opt => opt.MapFrom(src => Path.GetExtension(src.Form.FileName))) // Извлекаем расширение
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description)) // Маппинг описания
            .ForMember(dest => dest.Versions, opt => opt.MapFrom(src => new List<FileVersion> // Создаём список версий
            {
                new()
                {
                    CreatedAt = DateTime.UtcNow, // Текущее время создания
                    Hash = ComputeFileHash(GetFileContent(src.Form)), // Хэш содержимого файла
                    Tag = "1.0.0", // Стартовый тег версии
                    Content = GetFileContent(src.Form), // Содержимое файла в байтах
                    SizeBytes = src.Form.Length // Размер файла в байтах
                }
            }));

    }

    private static string ComputeFileHash(byte[] fileContent)
    {
        byte[] hashBytes = SHA256.HashData(fileContent);
        return Convert.ToBase64String(hashBytes);
    }

    private static byte[] GetFileContent(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        file.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }
}
