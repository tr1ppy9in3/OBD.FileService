using MediatR;
using Microsoft.Extensions.Options;

using OBD.FileService.Users.Core.Auth.Options;
using MNX.Application.UseCases.Results;

namespace OBD.FileService.Users.UseCases.Commands.ChangePasswordCommand;

public class ChangePasswordCommandHandler(IUserRepository userRepository,
                                          IOptions<PasswordOptions> passwordOptions)
    : IRequestHandler<ChangePasswordCommand, Result<Unit>>
{
    private readonly IUserRepository _userRepository = userRepository
        ?? throw new ArgumentNullException(nameof(userRepository));

    private readonly PasswordOptions _passwordOptions = passwordOptions?.Value
        ?? throw new ArgumentNullException(nameof(passwordOptions));

    public async Task<Result<Unit>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.UserId);

        if (user is null)
        {
            return Result<Unit>.Invalid($"User with {request.UserId} doesn't exist");
        }

        await _userRepository.ChangePassword(user, request.Password, _passwordOptions);
        return Result<Unit>.Empty();
    }
}
