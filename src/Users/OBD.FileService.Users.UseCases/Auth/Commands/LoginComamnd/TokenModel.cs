namespace OBD.FileService.Users.UseCases.Auth.Commands.LoginComamnd;

public class TokenModel
{
    public string Value { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    public DateTime ExpiresAt { get; set; }

}
