namespace OBD.FileService.Users.Core.Audit;

public class UserAction
{
    public Guid Id { get; set; }

    public UserActionType Type { get; set; } = default;

    public string? Description { get; set; } = default;

    public Guid UserId { get; set; }
}
