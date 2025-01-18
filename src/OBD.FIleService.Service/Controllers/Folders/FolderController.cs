using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MNX.Application.UseCases.Results;
using OBD.FileService.Files.Core;

using OBD.FileService.Files.UseCases.Files;
using OBD.FileService.Files.UseCases.Files.Command.UploadFileCommand;
using OBD.FileService.Files.UseCases.Folders;
using OBD.FileService.Files.UseCases.Folders.Command.CreateFolderCommand;
using OBD.FileService.Files.UseCases.Folders.Queries.GetAllAvailableFolderQuery;
using OBD.FileService.Files.UseCases.Folders.Queries.GetAttachedFilesByIdQuery;
using OBD.FileService.Files.UseCases.Folders.Queries.GetAttachedFoldersByIdQuery;
using OBD.FileService.Files.UseCases.Folders.Queries.GetFavoritesFoldersQuery;

using OBD.FileService.Users.UseCases.Auth;

namespace OBD.FIleService.Service.Controllers.Folders;

/// <summary>
/// Контроллер для взаимодействия с файлами.
/// </summary>
[Route("api/folders")]
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
    /// Получить все папки, доступные пользователю.
    /// </summary>
    /// <response code="200"> Успешно. Возвращает все найденные папки, доступные пользователю. </response>
    /// <response code="400"> Некорректный запрос. </response>
    [HttpGet("")]
    [ProducesResponseType(typeof(IAsyncEnumerable<Folder>), 200)]
    [Authorize(Roles = "RegularUser")]
    public IAsyncEnumerable<Folder> GetAllAvailableFolders()
    {
        var userId = _userAccessor.GetUserId();
        var result = _mediator.CreateStream(new GetAllAvailableFoldersQuery(userId));

        return result;
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(IAsyncEnumerable<Folder>), 201)]
    [Authorize(Roles = "RegularUser")]
    public async Task<IActionResult> CreateFolder(FolderInputModel model)
    {
        var userId = _userAccessor.GetUserId();
        var result = await _mediator.Send(new CreateFolderCommand(model, userId));

        return result.ToActionResult();
    }

    [HttpDelete("")]
    [ProducesResponseType(typeof(IAsyncEnumerable<Folder>), 201)]
    [Authorize(Roles = "RegularUser")]
    public Task<IActionResult> DeleteFolder()
    {
        throw new ArgumentNullException();
    }
}
