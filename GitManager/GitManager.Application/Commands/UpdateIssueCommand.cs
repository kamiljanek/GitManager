using GitIssueBridge.Services;
using MediatR;

namespace GitManager.Application.Commands;

public record UpdateIssueCommand(int IssueId, string IssueName, string IssueDescription) : IRequest;

public class UpdateIssueCommandHandler : IRequestHandler<UpdateIssueCommand>
{
    private readonly IGitIssueService _gitIssueService;

    public UpdateIssueCommandHandler(IGitIssueService gitIssueService)
    {
        _gitIssueService = gitIssueService;
    }

    public async Task Handle(UpdateIssueCommand request, CancellationToken cancellationToken)
    {
        await _gitIssueService.UpdateIssue(request.IssueId, request.IssueName, request.IssueDescription);
    }
}
