using MediatR;
using MNX.Application.UseCases.Results;

namespace OBD.FileService.Users.UseCases.Auth.Commands.DeactivateTokenCommand;

/// <summary>
/// Команда деактивации токена.
/// </summary>
/// <param name="Token"> Токен. </param>
public record DeactivateTokenCommand(string Token) : IRequest<Result<Unit>>;
