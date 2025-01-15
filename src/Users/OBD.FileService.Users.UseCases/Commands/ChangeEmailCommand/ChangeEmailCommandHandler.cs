using MediatR;

using MNX.Application.UseCases.Results;

namespace OBD.FileService.Users.UseCases.Commands.ChangeEmailCommand;

/// <summary>
/// Обработчик команды смены почты пользователя.
/// </summary>
public class ChangeEmailCommandHandler(IUserRepository userRepository) : IRequestHandler<ChangeEmailCommand, Result<Unit>>
{
    private readonly IUserRepository _userRepository = userRepository
        ?? throw new ArgumentNullException(nameof(userRepository));

    public async Task<Result<Unit>> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.UserId);

        if (user == null)
        {
            return Result<Unit>.Invalid($"User with {request.UserId} doesn't exist");
        }

        await _userRepository.ChangeEmail(user, request.Email);

        return Result<Unit>.Empty();
    }
}