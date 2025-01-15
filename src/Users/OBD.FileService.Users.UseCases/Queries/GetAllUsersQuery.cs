using MediatR;
using OBD.FileService.Users.Core;

namespace OBD.FileService.Users.UseCases.Queries;

/// <summary>
/// Запрос на получение всех пользоваталей.
/// </summary>
public record class GetAllUsersQuery : IStreamRequest<BaseUser> { }

/// <summary>
/// Обработчик запроса на получение всех пользователей.
/// </summary>
public class GetAllUsersQueryHandler(IUserRepository userRepository) : IStreamRequestHandler<GetAllUsersQuery, BaseUser>
{
    private readonly IUserRepository _userRepository = userRepository
        ?? throw new ArgumentNullException(nameof(userRepository));

    public IAsyncEnumerable<BaseUser> Handle(GetAllUsersQuery _, CancellationToken cancellationToken)
    {
        return _userRepository.GetAll();
    }
}