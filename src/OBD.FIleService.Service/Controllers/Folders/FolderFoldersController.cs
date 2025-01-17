using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MNX.Application.UseCases.Results;

using OBD.FileService.Files.Core;
using OBD.FileService.Files.UseCases.Folders.Queries.GetAttachedFilesByIdQuery;
using OBD.FileService.Files.UseCases.Folders.Queries.GetAttachedFoldersByIdQuery;
using OBD.FileService.Users.UseCases.Auth;

namespace OBD.FIleService.Service.Controllers.Folders;

public class FolderFoldersController
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
    /// Получить child-папки в папке.
    /// </summary>
    /// <param name="id"> Идентификатор папки. </param>
    [Authorize(Roles = "RegularUser")]
    [ProducesResponseType(typeof(IAsyncEnumerable<Folder>), 200)]
    [ProducesResponseType(400)]
    [HttpGet("{id:Guid}/folders")]
    public async Task<IActionResult> GetAttachedFolders(Guid id)
    {
        var userId = _userAccessor.GetUserId();
        var result = await _mediator.Send(new GetAttachedFoldersByIdQuery(id, userId));

        return result.ToActionResult();
    }
}
