using MediatR;
using MNX.Application.UseCases.Results;

using OBD.FileService.Files.Core;
using OBD.FileService.Files.UseCases.Files.Queries.GetFileByIdQuery;

namespace OBD.FileService.Files.UseCases.Files.Queries.GetFileTagsByIdQuery;

public class GetFileTagsByIdQueryHandler(IMediator mediator) : IRequestHandler<GetFileTagsByIdQuery, Result<IEnumerable<Tag>>>
{
    private readonly IMediator _mediator = mediator
        ?? throw new ArgumentNullException(nameof(mediator));

    public async Task<Result<IEnumerable<Tag>>> Handle(GetFileTagsByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetFileByIdQuery.GetFileByIdQuery(request.FileId, request.UserId), cancellationToken);
        if (!result.IsSuccess)
            return Result<IEnumerable<Tag>>.Invalid($"Unable to unmark due to issues: {string.Join(" ", result.Errors!)}!");

        return Result<IEnumerable<Tag>>.Success(result.GetValue().Tags);
    }
}
