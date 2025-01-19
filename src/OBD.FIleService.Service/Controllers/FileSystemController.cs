using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MNX.Application.UseCases.Results;

using OBD.FileService.Files.UseCases.Files.Command.UploadFileCommand;
using OBD.FileService.Files.UseCases.FileSystem.Commands.Favorite.MarkObjectAsFavorite;
using OBD.FileService.Files.UseCases.FileSystem.Commands.Favorite.UnmarkObjectAsFavorite;
using OBD.FileService.Files.UseCases.FileSystem.Commands.Object.CreateObject.CreateFileCommand;
using OBD.FileService.Files.UseCases.FileSystem.Commands.Object.CreateObject.CreateFileObject;
using OBD.FileService.Files.UseCases.FileSystem.Commands.Object.CreateObject.CreateFolderObject;
using OBD.FileService.Files.UseCases.FileSystem.Commands.Object.DeleteObject;
using OBD.FileService.Files.UseCases.FileSystem.Commands.Object.DownloadObjectCommand;
using OBD.FileService.Files.UseCases.FileSystem.Commands.Object.UpdateObject;
using OBD.FileService.Files.UseCases.FileSystem.Commands.Tags.AddTagToObject;
using OBD.FileService.Files.UseCases.FileSystem.Commands.Tags.DeleteTagFromObject;
using OBD.FileService.Files.UseCases.FileSystem.Models;
using OBD.FileService.Files.UseCases.FileSystem.Models.Abstractions;
using OBD.FileService.Files.UseCases.FileSystem.Queries.GetAllFavoritesObjects;
using OBD.FileService.Files.UseCases.FileSystem.Queries.GetContentQuery;
using OBD.FileService.Files.UseCases.FileSystem.Queries.GetRootContentQuery;
using OBD.FileService.Files.UseCases.Folders;
using OBD.FileService.Files.UseCases.Folders.Command.CreateFolderCommand;
using OBD.FileService.Users.UseCases.Auth;

namespace OBD.FileService.Service.Controllers;

/// <summary>
/// Контроллер для взаимодействия с метками.
/// </summary>
[Route("api/file-system")]
[ApiController]
public class FileSystemController
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
    /// Получает содержимое корневой папки.
    /// </summary>
    [Authorize(Roles = "RegularUser")]
    [HttpGet("")]
    [ProducesResponseType(typeof(IAsyncEnumerable<BaseFileSystemObject>), StatusCodes.Status200OK)]
    public IAsyncEnumerable<BaseFileSystemObject> GetRootContent()
    {
        long userId = _userAccessor.GetUserId();

        var result = _mediator.CreateStream(new GetRootContentQuery(userId));
        return result;
    }

    /// <summary>
    /// Получает информацию о системной объекте.
    /// </summary>
    /// <param name="id"> Идентификатор объекта. </param>
    [Authorize(Roles = "RegularUser")]
    [ProducesResponseType(typeof(BaseFileSystemObject), StatusCodes.Status200OK)]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetContentById(Guid id)
    {
        long userId = _userAccessor.GetUserId();

        var result = await _mediator.Send(new GetContentByIdQuery(id, userId));
        return result.ToActionResult();
    }

    /// <summary>
    /// Создает файл в системе.
    /// </summary>
    [Authorize(Roles = "RegularUser")]
    [HttpPost("files")]
    [ProducesResponseType(typeof(FileModel), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateFile(CreateFileObjectCommandModel model)
    {
        long userId = _userAccessor.GetUserId();
        var result = await _mediator.Send(new CreateFileObjectCommand(model, userId));

        return result.ToActionResult();

    }

    /// <summary>
    /// Создает папку в системе.
    /// </summary>
    [Authorize(Roles = "RegularUser")]
    [HttpPost("folders")]
    [ProducesResponseType(typeof(FolderModel), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateFolder(CreateFolderObjectCommandModel model)
    {
        long userId = _userAccessor.GetUserId();
        var result = await _mediator.Send(new CreateFolderObjectCommand(model, userId));

        return result.ToActionResult();

    }

    /// <summary>
    /// Обновляет системный объект.
    /// </summary>
    /// <param name="id"> Идентификатор объекта. </param>
    [Authorize(Roles = "RegularUser")]
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateObject(Guid id, BaseFileSystemObjectInput model)
    {
        long userId = _userAccessor.GetUserId();

        var result = await _mediator.Send(new UpdateObjectCommand(id, model, userId));
        return result.ToActionResult();
    }

    /// <summary>
    /// Удаляет системный объект.У
    /// </summary>
    /// <param name="id"> Идентификатор объекта. </param>
    [Authorize(Roles = "RegularUser")]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteObject(Guid id)
    {
        long userId = _userAccessor.GetUserId();

        var result = await _mediator.Send(new DeleteObjectCommand(id, userId));
        return result.ToActionResult();
    }

    /// <summary>
    /// Скачивает системный объект.
    /// </summary>
    /// <param name="id"> Идентификатор объекта. </param>
    [Authorize(Roles = "RegularUser")]
    [HttpGet("{id:guid}/download")]
    [Produces("application/octet-stream")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DownloadObject(Guid id)
    {
        long userId = _userAccessor.GetUserId();

        var result = await _mediator.Send(new DownloadObjectCommand(id, userId));
        if (result.IsSuccess)
            return result.GetValue();
        
        return result.ToActionResult();
    }

    /// <summary>
    /// Добавляет метку к системному объекту.
    /// </summary>
    /// <param name="id"> Идентификатор объекта. </param>
    /// <param name="tagId"> Идентификатор метки. </param>
    [Authorize(Roles = "RegularUser")]
    [HttpPost("{id:guid}/tags")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> AddTagToObject(Guid id, Guid tagId)
    {
        long userId = _userAccessor.GetUserId();

        var result = await _mediator.Send(new AddTagToObjectCommand(id, tagId, userId));
        return result.ToActionResult();
    }

    /// <summary>
    /// Добавляет метку к системному объекту.
    /// </summary>
    /// <param name="id"> Идентификатор объекта. </param>
    /// <param name="tagId"> Идентификатор метки. </param>
    [Authorize(Roles = "RegularUser")]
    [HttpDelete("{id:guid}/tags")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteTagFromObject(Guid id, Guid tagId)
    {
        long userId = _userAccessor.GetUserId();

        var result = await _mediator.Send(new DeleteTagFromObject(id, tagId, userId));
        return result.ToActionResult();
    }


    /// <summary>
    /// Получить все объекты, отмеченные как избранные.
    /// </summary>
    [Authorize(Roles = "RegularUser")]
    [HttpGet("favorites")]
    [ProducesResponseType(typeof(IAsyncEnumerable<BaseFileSystemObject>), StatusCodes.Status200OK)]
    public IAsyncEnumerable<BaseFileSystemObject> GetAllFavoritedObjects()
    {
        long userId = _userAccessor.GetUserId();
        return _mediator.CreateStream(new GetAllFavoritesObjectsQuery(userId));
    }

    /// <summary>
    /// Добавляет отметку что объект избранный.
    /// </summary>
    /// <param name="id"> Идентификатор объекта. </param>
    [Authorize(Roles = "RegularUser")]
    [HttpPost("favorites/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> MarkSystemObjectAsFavorite(Guid id)
    {
        long userId = _userAccessor.GetUserId();

        var result = await _mediator.Send(new MarkObjectAsFavoriteCommand(id, userId));
        return result.ToActionResult();
    }

    /// <summary>
    /// Убирает отметку что объект избранный.
    /// </summary>
    /// <param name="id"> Идентификатор объекта. </param>
    [Authorize(Roles = "RegularUser")]
    [HttpDelete("favorites/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UnmarkSystemObjectAsFavorite(Guid id)
    {
        long userId = _userAccessor.GetUserId();

        var result = await _mediator.Send(new UnmarkObjectAsFavoriteCommand(id, userId));
        return result.ToActionResult();
    }
}
