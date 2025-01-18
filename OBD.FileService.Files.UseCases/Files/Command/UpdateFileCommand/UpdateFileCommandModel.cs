using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBD.FileService.Files.UseCases.Files.Command.UpdateFileCommand;

public class UpdateFileCommandModel
{

    public required string Name { get; set; }


    public string Description { get; set; } = string.Empty;
}
