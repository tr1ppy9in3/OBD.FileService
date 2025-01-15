using OBD.FileService.Users.Core;

namespace OBD.FileService.Users.Core.Auth;

public class Role
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public string Description { get; set; } = string.Empty;

    public List<BaseUser> Users { get; } = [];
}
