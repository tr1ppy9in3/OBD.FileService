using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using OBD.FileService.Files.UseCases.Files.Command.UploadFileCommand;
using OBD.FileService.Files.UseCases.Folders.Queries.GetAttachedFilesByIdQuery;

using OBD.FileService.Files.UseCases.Files;
using OBD.FileService.Users.UseCases.Auth;

using MNX.Application.UseCases.Results;

namespace OBD.FIleService.Service.Controllers.Folders;

/// <summary>
/// Контроллер для взаимодействия с файлами в папке.
/// </summary>
[Route("api/folders/{id:Guid}/files/")]
[ApiController]
public class FolderFilesController
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
    /// Получить файлы в папке.
    /// </summary>
    /// <param name="id"> Идентификатор папки. </param>
    [Authorize(Roles = "RegularUser")]
    [ProducesResponseType(typeof(IAsyncEnumerable<FileService.Files.Core.File>), 200)]
    [HttpGet("")]
    public async Task<IActionResult> GetFilesInFolder(Guid id)
    {
        var userId = _userAccessor.GetUserId();
        var result = await _mediator.Send(new GetAttachedFilesByIdQuery(id, userId));

        return result.ToActionResult();
    }

    /// <summary>
    /// Загрузить файл в папку.
    /// </summary>
    /// <param name="id"> Идентификатор папки. </param>
    /// <param name="model"> Модель файла. </param>
    [Authorize(Roles = "RegularUser")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(FileService.Files.Core.File),201)]
    [HttpPost("")]
    public async Task<IActionResult> UploadFileToFolder(FileInputModel model)
    {
        long userId = _userAccessor.GetUserId();

        if (model.Form == null || model.Form.Length == 0)
            return BadRequest("Файл не загружен");

        var result = await _mediator.Send(new UploadFileCommand(userId, model));
        return result.ToActionResult();
    }

}
