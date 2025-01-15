using System.Security.Claims;
using Microsoft.AspNetCore.Http;

using OBD.FileService.Users.UseCases.Auth;

namespace OBD.FileService.Users.Infrastructure;

/// <summary>
/// Сервис для доступа к данным пользователя.
/// </summary>
public class UserAccessor(IHttpContextAccessor httpContextAccessor) : IUserAccessor
{
    /// <summary>
    /// Авторизированный пользователь.
    /// </summary>
    private readonly ClaimsPrincipal _user = httpContextAccessor.HttpContext?.User
        ?? throw new ArgumentNullException(nameof(httpContextAccessor));

    /// <summary>
    /// Получить идентификатор пользователя.
    /// </summary>
    /// <returns> Идентификатор пользователя. </returns>
    public long GetUserId()
    {
        if (long.TryParse(_user.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value, out long id))
        {
            return id;
        }

        return -1;
    }

    /// <summary>
    /// Получить токен из заголовка Authorization.
    /// </summary>
    /// <returns> Токен. </returns>
    public string? GetToken()
    {
        var httpContext = httpContextAccessor.HttpContext
            ?? throw new ArgumentNullException(nameof(httpContextAccessor));

        var authorizationHeader = httpContext.Request.Headers["Authorization"].ToString();
 
        if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
        {
            return authorizationHeader["Bearer ".Length..].Trim();
        }

        return null;
    }

    /// <summary>
    /// Получить юзернейм пользователя.
    /// </summary>
    /// <returns> Юзернейм пользователя. </returns>
    public string GetUsername()
    {
        return _user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value ?? string.Empty;
    }

    /// <summary>
    /// Получить все клеймы пользователя.
    /// </summary>
    /// <returns> Список клеймов пользователя. </returns>
    public IEnumerable<Claim> GetAllClaims()
    {
        return _user.Claims;
    }
}