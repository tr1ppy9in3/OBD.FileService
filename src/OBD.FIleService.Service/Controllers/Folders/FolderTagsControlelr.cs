using MediatR;
using Microsoft.AspNetCore.Mvc;

using OBD.FileService.Files.Core;
using OBD.FileService.Users.UseCases.Auth;

namespace OBD.FIleService.Service.Controllers.Folders;

/// <summary>
/// Контроллер для взаимодействия с метками папки.
/// </summary>
[Route("api/folders/{id:Guid}/tags")]
[ApiController]
public class FolderTagsControlelr
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
    /// Получить все метки папки.
    /// </summary>
    /// <param name="id"> Идентификатор папки.</param>
    [ProducesResponseType(typeof(IAsyncEnumerable<Tag>),200)]
    [HttpGet("")]
    public IAsyncEnumerable<Tag> GetFolderTags(Guid id)
    {
        var userId = _userAccessor.GetUserId();

        throw new NotImplementedException();
    }

    /// <summary>
    /// Добавить к папке метку.
    /// </summary>
    /// <param name="id"> Идентификатор папки. </param>
    [ProducesResponseType(204)]
    [HttpPost("")]
    public async Task<IActionResult> MarkFolderByTag(Guid id)
    {
        var userId = _userAccessor.GetUserId();

        throw new NotImplementedException();
    }

    /// <summary>
    /// Удалить метку с файла.
    /// </summary>
    /// <param name="id"> Идентификатор папки. </param>
    [ProducesResponseType(204)]
    [HttpDelete("")]
    public async Task<IActionResult> UnmarkTagFromFolder(Guid id)
    {
        var userId = _userAccessor.GetUserId();

        throw new NotImplementedException();
    }
}
