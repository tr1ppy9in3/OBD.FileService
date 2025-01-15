using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using MNX.Application.UseCases.Results;

using OBD.FileService.Users.Core.Auth;

using OBD.FileService.Users.UseCases.Auth;
using OBD.FileService.Users.UseCases.Auth.Commands.LoginComamnd;
using OBD.FileService.Users.UseCases.Auth.Commands.LogoutCommand;
using OBD.FileService.Users.UseCases.Auth.Commands.RegistrationCommand;

using OBD.FileService.Users.Infrastructure;

namespace IAD.TodoListApp.Service.Controllers;

/// <summary>
/// Контроллер для аунтефикации.
/// </summary>
[Route("api/auth")]
[ApiController]
public class AuthController(IMediator mediator,
                            IUserAccessor userAccessor) : ControllerBase
{
    /// <summary>
    /// Посредник.
    /// </summary>
    private readonly IMediator _mediator = mediator 
        ?? throw new ArgumentNullException(nameof(mediator));

    private readonly IUserAccessor _userAccessor = userAccessor
        ?? throw new ArgumentNullException(nameof(userAccessor));

    /// <summary>
    /// Регистрация.
    /// </summary>
    /// <response code="200"> Успешно. </response>
    /// <response code="400"> Некоретный запрос. </response>
    [AllowAnonymous]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [HttpPost("registration")]
    public async Task<IActionResult> 
    Registration(RegistrationCommand request)
    {
        var result = await _mediator.Send(request);
        return result.ToActionResult();
    }
    /// <summary>
    /// Авторизация.
    /// </summary>
    /// <response code="200"> Успешно. </response>
    /// <returns> Токен. </returns>
    /// <response code="400"> Некоретный запрос. </response>
    [AllowAnonymous]
    [ProducesResponseType(typeof(Token), 200)]
    [ProducesResponseType(400)]
    [HttpPost("login")]
    public async Task<IActionResult> 
    Login(LoginCommand request)
    {
        var result = await _mediator.Send(request);
        return result.ToActionResult();
    }


    /// <summary>
    /// Выход.
    /// </summary>
    /// <returns></returns>
    [Authorize(Policy = "RegularUserPolicy")]
    [ProducesResponseType(204)]
    [HttpPost("logout")]
    public async Task<IActionResult>
    Logout()
    {
        var token = _userAccessor.GetToken();
        
        var result = await _mediator.Send(new LogoutCommand(token!));
        return result.ToActionResult();
    }
}
