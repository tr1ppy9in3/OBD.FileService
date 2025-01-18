using OBD.FileService.Files.Core;
using System.IO.Compression;

public class ZipCreator
{
    public static byte[] CreateZipFromFolderModel(Folder rootFolder)
    {
        using var memoryStream = new MemoryStream();
        using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
        {
            AddFolderToArchive(rootFolder, zipArchive, "");
        }

        return memoryStream.ToArray();
    }

    private static void AddFolderToArchive(Folder folder, ZipArchive zipArchive, string currentPath)
    {
        string folderPath = string.IsNullOrEmpty(currentPath) ? folder.Name : Path.Combine(currentPath, folder.Name);

        foreach (var file in folder.AttachedFiles)
        {
            string filePath = Path.Combine(folderPath, $"{file.Name}{file.Extension}");
            var zipEntry = zipArchive.CreateEntry(filePath);

            using var entryStream = zipEntry.Open();
            entryStream.Write(file.LastVersion!.Content, 0, file.LastVersion.Content.Length);
        }

        foreach (var subFolder in folder.AttachedFolders)
        {
            AddFolderToArchive(subFolder, zipArchive, folderPath);
        }
    }
}
