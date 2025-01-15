using MediatR;
using AutoMapper;
using Microsoft.Extensions.Options;

using MNX.Application.UseCases.Results;

using OBD.FileService.Users.Core.Auth;
using OBD.FileService.Users.Core.Auth.Options;
using OBD.FileService.Users.Core.Auth.Services;

namespace OBD.FileService.Users.UseCases.Auth.Commands.LoginComamnd;

/// <summary>
/// Обработчик команды для авторизации пользователя.
/// </summary>
public class LoginCommandHandler(IUserRepository userRepository,
                                 ITokenService tokenService,
                                 IOptions<PasswordOptions> passwordOptions,
                                 IMapper mapper)
    : IRequestHandler<LoginCommand, Result<TokenModel>>
{
    private readonly IMapper _mapper = mapper
        ?? throw new ArgumentNullException(nameof(mapper));

    private readonly IUserRepository _userRepository = userRepository
        ?? throw new ArgumentNullException(nameof(userRepository));

    private readonly ITokenService _tokenService = tokenService
        ?? throw new ArgumentNullException(nameof(tokenService));

    private readonly string _salt = passwordOptions?.Value?.Salt
        ?? throw new ArgumentNullException(nameof(passwordOptions));

    public async Task<Result<TokenModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Resolve(request.Login, CryptographyService.HashPassword(request.Password, _salt));

        if (user is null)
        {
            return Result<TokenModel>.Invalid("Invalid password or login");
        }

        Token token = await _tokenService.GenerateToken(user);
        return Result<TokenModel>.Success(_mapper.Map<TokenModel>(token));
    }
}