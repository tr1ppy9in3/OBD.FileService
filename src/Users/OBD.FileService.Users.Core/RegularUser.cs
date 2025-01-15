namespace OBD.FileService.Users.Core;

/// <summary>
/// Обычный пользователь.
/// </summary>
public class RegularUser : BaseUser
{
    /// <inheritdoc/>
    public override UserType Type { get; set; } = UserType.Regular;

    /// <summary>
    /// Фотография
    /// </summary>
    public byte[]? ProfilePic { get; set; }

    /// <summary>
    /// Имя
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Фамилия
    /// </summary>
    public string Surname { get; set; } = string.Empty;
}
