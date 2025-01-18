using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MNX.Application.UseCases.Results;

using OBD.FileService.Files.UseCases.Tags.Command.CreateTagCommand;
using OBD.FileService.Files.UseCases.Tags.Command.DeleteTagCommand;
using OBD.FileService.Files.UseCases.Tags.Command.UpdateTagCommand;

using OBD.FileService.Files.UseCases.Tags.Queries.GetAllTagsAvailableQuery;
using OBD.FileService.Files.UseCases.Tags.Models;

using OBD.FileService.Users.UseCases.Auth;

namespace OBD.FIleService.Service.Controllers;

/// <summary>
/// Контроллер для взаимодействия с метками.
/// </summary>
[Route("api/tags")]
[ApiController]
public class TagController
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
    /// Получить все доступные пользователю метки.
    /// </summary>
    [ProducesResponseType(typeof(IAsyncEnumerable<TagOutputModel>), 200)]
    [Authorize(Roles = "RegularUser")]
    [HttpGet("")]
    public IAsyncEnumerable<TagOutputModel> GetAllAvailableTags()
    {
        long userId = _userAccessor.GetUserId();
        return  _mediator.CreateStream(new GetAllTagsAvailableQuery(userId));
    }

    /// <summary>
    /// Создать метку.
    /// </summary>
    /// <param name="model"> Модель метки. </param>
    /// <returns></returns>
    [ProducesResponseType(typeof(TagOutputModel), 201)]
    [Authorize(Roles = "RegularUser")]
    [HttpPost("")]
    public async Task<IActionResult> CreateTag(TagInputModel model)
    {
        long userId = _userAccessor.GetUserId();

        var result = await _mediator.Send(new CreateTagCommand(model, userId));
        return result.ToActionResult();
    }

    /// <summary>
    /// Обновить метку.
    /// </summary>
    /// <param name="tagId"> Идентификатор метки. </param>
    /// <param name="model"> Модель метки. </param>
    [ProducesResponseType(204)]
    [Authorize(Roles = "RegularUser")]
    [HttpPut("{tagId:Guid}")]
    public async Task<IActionResult> UpdateTag(Guid tagId, TagInputModel model)
    {
        long userId = _userAccessor.GetUserId();

        var result = await _mediator.Send(new UpdateTagCommand(model, tagId, userId));
        return result.ToActionResult();
    }

    /// <summary>
    /// Удалить метку.
    /// </summary>
    /// <param name="tagId"> Идентификатор метки. </param>
    [ProducesResponseType(204)]
    [Authorize(Roles = "RegularUser")]
    [HttpDelete("{tagId:Guid}")]
    public async Task<IActionResult> DeleteTag(Guid tagId)
    {
        long userId = _userAccessor.GetUserId();

        var result = await _mediator.Send(new DeleteTagCommand(tagId, userId));
        return result.ToActionResult();
    }
}