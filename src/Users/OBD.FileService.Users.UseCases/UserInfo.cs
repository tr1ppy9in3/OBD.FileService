namespace OBD.FileService.Users.UseCases;

public class UserInfo
{
    public string Name { get; set; } = string.Empty;

    public string Surname { get; set; } = string.Empty;

    public required string Email { get; set; }

    public string Picture { get; set; } = string.Empty;
}
