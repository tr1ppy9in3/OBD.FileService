using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBD.FileService.Files.UseCases.FileSystem.Commands.Object.CreateObject.CreateFolderObject;

public class CreateFolderObjectCommandModel
{
    public Guid? ParentFolderId { get; set; } = default;

    public required string Name { get; set; }
}
