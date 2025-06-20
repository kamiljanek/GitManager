using GitIssueBridge.Services;
using MediatR;

namespace GitManager.Application.Commands;

public record CloseIssueCommand(int IssueId) : IRequest;

public class CloseIssueCommandHandler : IRequestHandler<CloseIssueCommand>
{
    private readonly IGitIssueService _gitIssueService;

    public CloseIssueCommandHandler(IGitIssueService gitIssueService)
    {
        _gitIssueService = gitIssueService;
    }

    public async Task Handle(CloseIssueCommand request, CancellationToken cancellationToken)
    {
        await _gitIssueService.CloseIssue(request.IssueId);
    }
}
