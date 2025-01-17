using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using OBD.FileService.Files.Core;

using OBD.FileService.Users.UseCases.Auth;
using OBD.FileService.Files.UseCases.Folders.Queries.GetFavoritesFoldersQuery;

namespace OBD.FIleService.Service.Controllers.Folders;

/// <summary>
/// Контроллер для взаимодействия с избранными папками.
/// </summary>
[Route("api/folders/favorites")]
[ApiController]
public class FolderFavoritesController
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
    /// Получить все папки, помеченные как избранные и доступные пользователю.
    /// </summary>
    /// <response code="200"> Успешно. Возвращает все найденные папки, помеченные как избранные доступные пользователю. </response>
    /// <response code="400"> Некорректный запрос. </response>
    [HttpGet("")]
    [ProducesResponseType(typeof(IAsyncEnumerable<Folder>), 200)]
    [Authorize(Roles = "RegularUser")]
    public IAsyncEnumerable<Folder> GetAllAvailableFavoritesFolders()
    {
        var userId = _userAccessor.GetUserId();

        var result = _mediator.CreateStream(new GetFavoritesFoldersQuery(userId));
        return result;
    }

    /// <summary>
    /// Добавить папку в избранное.
    /// </summary>
    [Authorize(Roles = "RegularUser")]
    [HttpPost("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> MarkFolderAsFavorite(Guid id)
    {
        var userId = _userAccessor.GetUserId();

        throw new NotImplementedException();
    }

    /// <summary>
    /// Убрать папку из избранного.
    /// </summary>
    [Authorize(Roles = "RegularUser")]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UnmarkFolderAsFavorite(Guid id)
    {
        var userId = _userAccessor.GetUserId();

        throw new NotImplementedException();
    }

}
