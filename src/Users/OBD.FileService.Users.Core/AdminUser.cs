namespace OBD.FileService.Users.Core;

/// <summary>
/// Пользователь-админ.
/// </summary>
public class AdminUser : BaseUser
{
    /// <inheritdoc/>
    public override UserType Type { get; set; } = UserType.Admin;
}
