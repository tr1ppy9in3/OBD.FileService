using MediatR;
using Microsoft.Extensions.Options;

using MNX.Application.UseCases.Results;

using OBD.FileService.Users.Core;
using OBD.FileService.Users.Core.Auth;
using OBD.FileService.Users.Core.Auth.Options;

namespace OBD.FileService.Users.UseCases.Auth.Commands.RegistrationCommand;

/// <summary>
/// Обработчик команды для регистрации пользователя.
/// </summary>
public class RegistrationCommandHandler(IUserRepository userRepository,
                                        IRoleRepostiory roleRepostiory,
                                        IOptions<PasswordOptions> passwordOptions)
    : IRequestHandler<RegistrationCommand, Result<Unit>>
{
    private readonly IUserRepository _userRepository = userRepository
        ?? throw new ArgumentNullException(nameof(userRepository));

    private readonly IRoleRepostiory _roleRepository = roleRepostiory
        ?? throw new ArgumentNullException(nameof(roleRepostiory));

    private readonly PasswordOptions _passwordOptions = passwordOptions?.Value
        ?? throw new ArgumentNullException(nameof(passwordOptions));

    public async Task<Result<Unit>> Handle(RegistrationCommand request, CancellationToken cancellationToken)
    {
        var existingUserWithEmail = await _userRepository.GetByEmail(request.Email);
        if (existingUserWithEmail is not null)
        {
            return Result<Unit>.Conflict("User with this email already exists!");
        }

        var existingUserWithLogin = await _userRepository.GetByLogin(request.Login);
        if (existingUserWithLogin is not null)
        {
            return Result<Unit>.Conflict("User with this login already exists!");
        }

        BaseUser user = new RegularUser
        {
            Login = request.Login,
            Email = request.Email
        };

        user.SetPassword(request.Password, _passwordOptions);
        await _userRepository.Add(user);

        Role role = await _roleRepository.FindOrCreate("RegularUser");
        await _roleRepository.AddRoleToUser(role.Id, user.Id);

        return Result<Unit>.SuccessfullyCreated(Unit.Value);
    }
}