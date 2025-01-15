using MediatR;

using MNX.Application.UseCases.Results;
using OBD.FileService.Users.Core;

namespace OBD.FileService.Users.UseCases.Queries;

/// <summary>
/// Запрос на получение пользователя по идентификатору.
/// </summary>
/// <param name="Id"> Идентификатор пользователя. </param>
public record class GetUserByIdQuery(long Id) : IRequest<Result<BaseUser>>;

/// <summary>
/// Обработчик запроса на получение пользователя по идентификатору.
/// </summary>
public class GetUserByIdQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserByIdQuery, Result<BaseUser>>
{
    private readonly IUserRepository _userRepository = userRepository
        ?? throw new ArgumentNullException(nameof(userRepository));

    public async Task<Result<BaseUser>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.Id);

        if (user is null)
        {
            return Result<BaseUser>.Invalid($"User with Id {request.Id} doesn't exist!");
        }

        return Result<BaseUser>.Success(user);
    }
}