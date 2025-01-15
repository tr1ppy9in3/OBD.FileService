﻿using MediatR;

using MNX.Application.UseCases.Results;

namespace OBD.FileService.Users.UseCases.Auth.Commands.DeactivateTokenCommand;

/// <summary>
/// Обработчик команды деактивации токена.
/// </summary>
public class DeactivateTokenCommandHandler(ITokenRepository tokenRepository) : IRequestHandler<DeactivateTokenCommand, Result<Unit>>
{
    private readonly ITokenRepository _tokenRepository = tokenRepository
        ?? throw new ArgumentNullException(nameof(tokenRepository));

    public async Task<Result<Unit>> Handle(DeactivateTokenCommand request, CancellationToken cancellationToken)
    {
        var existingToken = await _tokenRepository.GetTokenByValue(request.Token);

        if (existingToken is not null)
        {
            if (existingToken.IsBlacklisted)
            {
                return Result<Unit>.Invalid("Token is already blacklisted.");
            }

            existingToken.IsBlacklisted = true;
            await _tokenRepository.Update(existingToken);
        }

        return Result<Unit>.Success(Unit.Value);
    }
}
