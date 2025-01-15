using OBD.FileService.Users.Core;
using OBD.FileService.Users.Core.Auth;

namespace OBD.FileService.Users.UseCases.Auth;

/// <summary>
/// Сервис для работы с токенами.
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Генерация токена доступа.
    /// </summary>
    /// <param name="user"> Пользователь. </param>
    /// <returns> Access токен. </returns>
    public Task<Token> GenerateToken(BaseUser user);
}
