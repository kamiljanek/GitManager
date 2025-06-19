using MediatR;

namespace GitManager.Application.Commands;

public record CloseIssueCommand(Guid Id) : IRequest;

public class CloseIssueCommandHandler : IRequestHandler<CloseIssueCommand>
{
    public CloseIssueCommandHandler()
    {
    }

    public async Task Handle(CloseIssueCommand request, CancellationToken cancellationToken)
    {

    }
}
