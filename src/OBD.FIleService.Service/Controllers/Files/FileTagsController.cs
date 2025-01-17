using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MNX.Application.UseCases.Results;

using OBD.FileService.Files.Core;
using OBD.FileService.Users.UseCases.Auth;

using OBD.FileService.Files.UseCases.Files.Command.Tags.MarkTagToFileCommand;
using OBD.FileService.Files.UseCases.Files.Command.Tags.UnmarkTagFromFileCommand;

using OBD.FileService.Files.UseCases.Files.Queries.GetFileTagsByIdQuery;

namespace OBD.FIleService.Service.Controllers.Files;

/// <summary>
/// Контроллер для взаимодействия с метками файлов.
/// </summary>
[Route("api/files/{id:guid}/tags")]
[ApiController]
public class FileTagsController
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

    #region Tags

    /// <summary>
    /// Получить все метки файла. 
    /// </summary>
    /// <param name="id"> Идентификатор файла.</param>
    [ProducesResponseType(typeof(IEnumerable<Tag>), 200)]
    [Authorize(Roles = "RegularUser")]
    [HttpGet("")]
    public async Task<IActionResult> GetFileTags(Guid id)
    {
        long userId = _userAccessor.GetUserId();

        var result = await _mediator.Send(new GetFileTagsByIdQuery(id, userId));
        return result.ToActionResult();
    }

    /// <summary>
    /// Добавить метку к файлу.
    /// </summary>
    /// <param name="id"> Идентификатор файла. </param>
    /// <param name="tagId"> Идентификатор метки. </param>
    [ProducesResponseType(204)]
    [Authorize(Roles = "RegularUser")]
    [HttpPost("")]
    public async Task<IActionResult> MarkFileByTag(Guid id, Guid tagId)
    {
        long userId = _userAccessor.GetUserId();

        var result = await _mediator.Send(new MarkTagToFileCommand(userId, tagId, id));
        return result.ToActionResult();
    }

    /// <summary>
    /// Удалить метку с файла.
    /// </summary>
    /// <param name="id"> Идентифиатор файла. </param>
    [ProducesResponseType(204)]
    [Authorize(Roles = "RegularUser")]
    [HttpDelete("")]
    public async Task<IActionResult> UnmarkTagFromFile(Guid id, Guid tagId)
    {
        long userId = _userAccessor.GetUserId();

        var result = await _mediator.Send(new UnmarkTagFromFileCommand(tagId, id, userId));
        return result.ToActionResult();
    }

    #endregion
}
