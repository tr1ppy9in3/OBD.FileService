namespace OBD.FileService.Users.UseCases.Auth;

/// <summary>
/// Интерфейс для взаимодействия с аунтефицированным пользователем.
/// </summary>
public interface IUserAccessor
{
    /// <summary>
    /// Получить индетификатор пользователя.
    /// </summary>
    /// <returns> Индетификатор. </returns>
    public long GetUserId();

    /// <summary>
    /// Получить токен пользователя.
    /// </summary>
    /// <returns> Строку токена. </returns>
    public string? GetToken();

    /// <summary>
    /// Получить логин пользователя.
    /// </summary>
    /// <returns> Логин пользователя. </returns>
    public string GetUsername();}

