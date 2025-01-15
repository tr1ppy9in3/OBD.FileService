using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using OBD.FileService.Files.Core;
using OBD.FileService.Files.UseCases.Folders.Queries.GetAllAvailableFolderQuery;
using OBD.FileService.Users.UseCases.Auth;

namespace OBD.FIleService.Service.Controllers;

/// <summary>
/// Контроллер для взаимодействия с файлами.
/// </summary>
[Route("api/folder")]
[ApiController]
public class FolderController
(
    IMediator mediator,
    IUserAccessor userAccessor
)
    : ControllerBase
{
    private readonly IMediator _mediator = mediator
        ?? throw new ArgumentNullException(nameof(mediator));

    private readonly IUserAccessor _userAccessor = userAccessor
        ?? throw new ArgumentNullException(nameof(userAccessor));

    /// <summary>
    /// Получить все доступные папки.
    /// </summary>
    /// <returns></returns>
    /// <response code="200"> Успешно. Возвращает все найденные папки, доступные пользователю. </response>
    /// <response code="400"> Некорректный запрос. </response>
    [HttpGet("folders")]
    [ProducesResponseType(typeof(IAsyncEnumerable<Folder>), 200)]
    [ProducesResponseType(400)]
    [Authorize(Roles = "RegularUser")]
    public IAsyncEnumerable<Folder> GetAllAvailableFolders()
    {
        var userId = _userAccessor.GetUserId();
        var result = _mediator.CreateStream(new GetAllAvailableFolderQuery(userId));

        return result;
    }
}
