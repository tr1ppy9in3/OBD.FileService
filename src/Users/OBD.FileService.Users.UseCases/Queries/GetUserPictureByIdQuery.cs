using MediatR;

using MNX.Application.UseCases.Results;
using OBD.FileService.Users.Core;

using System.Collections;

namespace OBD.FileService.Users.UseCases.Queries;

/// <summary>
/// Запрос на получение картинки пользователя.
/// </summary>
/// <param name="Id"> Идентификатор пользователя. </param>
public record class GetUserPictureByIdQuery(long Id) : IRequest<Result<byte[]>>;

/// <summary>
/// Обработчик запроса на получение картинки пользователя.
/// </summary>
/// <param name="userRepository"></param>
public class GetUserPictureByIdQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserPictureByIdQuery, Result<byte[]>>
{
    private readonly IUserRepository _userRepository = userRepository
        ?? throw new ArgumentNullException(nameof(userRepository));

    public async Task<Result<byte[]>> Handle(GetUserPictureByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.Id);

        if (user is null)
        {
            return Result<byte[]>.Invalid($"User with Id {request.Id} doesn't exist!");
        }

        if (user is not RegularUser regularUser)
        {
            return Result<byte[]>.Invalid($"User role doesn't allow to interact with profile picture!");
        }

        if (regularUser.ProfilePic is null)
        {
            return Result<byte[]>.Success(Array.Empty<byte>());
        }

        return Result<byte[]>.Success(regularUser.ProfilePic);
    }
}
