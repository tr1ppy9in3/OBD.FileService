using MediatR;
using MNX.Application.UseCases.Results;

namespace OBD.FileService.Users.UseCases.Queries;

/// <summary>
/// Запрос на получение почты пользователя.
/// </summary>
/// <param name="UserId"> Идентификатор пользователя. </param>
public record GetEmailQuery(long UserId) : IRequest<Result<string>>;

/// <summary>
/// Обработчик запроса на получение почты пользователя.
/// </summary>
public record GetEmailQueryHandler(IUserRepository userRepository) : IRequestHandler<GetEmailQuery, Result<string>>
{
    private readonly IUserRepository _userRepository = userRepository
        ?? throw new ArgumentNullException(nameof(userRepository));

    public async Task<Result<string>> Handle(GetEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.UserId);

        if (user == null)
        {
            return Result<string>.Invalid("User not found.");
        }

        return Result<string>.Success(user.Email);
    }
}