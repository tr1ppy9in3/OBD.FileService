using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MNX.Application.UseCases.Results;

using OBD.FileService.Users.UseCases.Auth;

using OBD.FileService.Files.UseCases.Files.Command.Favorite.MarkFavoriteFileCommand;
using OBD.FileService.Files.UseCases.Files.Command.Favorite.UnmarkFileAsFavoriteCommand;

using OBD.FileService.Files.UseCases.Files.Queries.GetFavoritesFilesQuery;

namespace OBD.FIleService.Service.Controllers.Files;

/// <summary>
/// Контроллер для взаимодействия с избранными файлами.
/// </summary>
[Route("api/files/favorites")]
[ApiController]
public class FileFavoritesController
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
    ///  Получить все файлы, доступные пользователю и отмеченные как избранные.
    /// </summary>
    [ProducesResponseType(typeof(FileService.Files.Core.File), 200)]
    [Authorize(Roles = "RegularUser")]
    [HttpGet("")]
    [ProducesResponseType(typeof(IAsyncEnumerable<FileService.Files.Core.File>), 200)]
    public IAsyncEnumerable<FileService.Files.Core.File> GetAllAvailableFavoriteFiles()
    {
        long userId = _userAccessor.GetUserId();
        return _mediator.CreateStream(new GetFavoritesFilesQuery(userId));
    }

    /// <summary>
    /// Пометить файл как избранный.
    /// </summary>
    [Authorize(Roles = "RegularUser")]
    [HttpPost("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> MarkFileAsFavorite(Guid id)
    {
        long userId = _userAccessor.GetUserId();

        var result = await _mediator.Send(new MarkFileAsFavoriteCommand(id, userId));
        return result.ToActionResult();
    }

    /// <summary>
    /// Убрать файл из избранного.
    /// </summary>
    [Authorize(Roles = "RegularUser")]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UnmarkFileAsFavorite(Guid id)
    {
        long userId = _userAccessor.GetUserId();

        var result = await _mediator.Send(new UnmarkFileAsFavoriteCommand(id, userId));
        return result.ToActionResult();
    }
}
