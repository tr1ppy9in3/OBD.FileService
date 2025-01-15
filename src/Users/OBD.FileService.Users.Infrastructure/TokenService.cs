using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using OBD.FileService.Users.Core;
using OBD.FileService.Users.Core.Auth;
using OBD.FileService.Users.Core.Auth.Options;

using OBD.FileService.Users.UseCases.Auth;

namespace OBD.FileService.Users.Infrastructure;

/// <summary>
/// Реализация <see cref="ITokenService"/>.
/// </summary>
public class TokenService(IOptions<JwtOptions> jwtOptions, ITokenRepository tokenRepository) : ITokenService
{
    /// <summary>
    /// Параметры JWT токена.
    /// </summary>
    private readonly JwtOptions _jwtOptions = jwtOptions.Value
        ?? throw new ArgumentNullException(nameof(jwtOptions));

    /// <summary>
    /// Репозиторий для взаимодействия с токенами в базе данных.
    /// </summary>
    private readonly ITokenRepository _tokenRepository = tokenRepository
        ?? throw new ArgumentNullException(nameof(tokenRepository));

    /// <inheritdoc/>
    public async Task<Token> GenerateToken(BaseUser user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Login.ToString()),
        };

        foreach (var role in user.Roles)
        {
            if (role is not null)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiresTime = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiryMinutes);

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: expiresTime,
            signingCredentials: creds);

        var tokenModel = new Token
        {
            Value = new JwtSecurityTokenHandler().WriteToken(token),
            UserId = user.Id,
            Username = user.Login,
            ExpiresAt = expiresTime,
        };

        await _tokenRepository.Create(tokenModel);
        return tokenModel;
    }
}