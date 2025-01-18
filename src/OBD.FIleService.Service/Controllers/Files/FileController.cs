using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MNX.Application.UseCases.Results;

using OBD.FileService.Files.Core;

using OBD.FileService.Files.UseCases.Files;
using OBD.FileService.Files.UseCases.Files.Command.DeleteFileCommand;
using OBD.FileService.Files.UseCases.Files.Command.DownloadFileCommand;
using OBD.FileService.Files.UseCases.Files.Command.UploadFileCommand;

using OBD.FileService.Files.UseCases.Files.Queries.GetAllAvailableFilesQuery;
using OBD.FileService.Files.UseCases.Files.Queries.GetFavoritesFilesQuery;
using OBD.FileService.Files.UseCases.Files.Queries.GetFileByIdQuery;
using OBD.FileService.Files.UseCases.Files.Queries.GetFileTagsByIdQuery;

using OBD.FileService.Files.UseCases.Files.Command.Tags.MarkTagToFileCommand;
using OBD.FileService.Files.UseCases.Files.Command.Tags.UnmarkTagFromFileCommand;

using OBD.FileService.Files.UseCases.Files.Command.Favorite.MarkFavoriteFileCommand;
using OBD.FileService.Files.UseCases.Files.Command.Favorite.UnmarkFileAsFavoriteCommand;

using OBD.FileService.Users.UseCases.Auth;

namespace OBD.FIleService.Service.Controllers.Files;

/// <summary>
/// Контроллер для взаимодействия с файлами.
/// </summary>
[Route("api/files")]
[ApiController]
public class FileController
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

    #region CRUD

    /// <summary>
    ///  Получить все файлы, доступные пользователю.
    /// </summary>
    [ProducesResponseType(typeof(FileService.Files.Core.File), 200)]
    [Authorize(Roles = "RegularUser")]
    [HttpGet("")]
    public IAsyncEnumerable<FileService.Files.Core.File> GetAllAvailableFiles()
    {
        long userId = _userAccessor.GetUserId();
        return _mediator.CreateStream(new GetAllAvailableFilesQuery(userId));
    }

    /// <summary>
    ///  Загрузить файл без папки в корневную директорию.
    /// </summary>
    /// <param name="model"> Модель. </param>
    [ProducesResponseType(typeof(FileService.Files.Core.File), 201)]
    [Authorize(Roles = "RegularUser")]
    [HttpPost("")]
    public async Task<IActionResult> UploadFile([FromForm] FileInputModel model)
    {
        long userId = _userAccessor.GetUserId();

        if (model.Form == null || model.Form.Length == 0)
            return BadRequest("Файл не загружен");

        var result = await _mediator.Send(new UploadFileCommand(userId, model));
        return result.ToActionResult();
    }

    /// <summary>
    /// Получить доступный пользователю файл по идентификатору.
    /// </summary>
    /// <param name="id"> Идентификатор файла. </param>
    [ProducesResponseType(typeof(FileService.Files.Core.File), 200)]
    [Authorize(Roles = "RegularUser")]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetFileById(Guid id)
    {
        long userId = _userAccessor.GetUserId();

        var result = await _mediator.Send(new GetFileByIdQuery(id, userId));
        return result.ToActionResult();
    }

    /// <summary>
    /// Удалить доступный пользователю файл по идентификатору.
    /// </summary>
    /// <param name="id"> Идентификатор файла. </param>
    /// <returns></returns>
    [ProducesResponseType(204)]
    [Authorize(Roles = "RegularUser")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteFile(Guid id)
    {
        long userId = _userAccessor.GetUserId();

        var result = await _mediator.Send(new DeleteFileCommand(id, userId));
        return result.ToActionResult();
    }

    #endregion

    /// <summary>
    /// Скачать файл, доступных текущему пользователю.
    /// </summary>
    /// <param name="id"> Идентификатор файла. </param>
    [Authorize(Roles = "RegularUser")]
    [HttpGet("{id:guid}/download")]
    public async Task<IActionResult> DownloadFile(Guid id)
    {
        long userId = _userAccessor.GetUserId();

        var result = await _mediator.Send(new DownloadFileCommand(id, userId));

        if (!result.IsSuccess)
            return result.ToActionResult();

        var file = result.GetValue();

        return File
        (
            file.Content,
            file.MimeType,
            file.FileName
        );
    }
}