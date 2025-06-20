using GitIssueBridge.Services;
using MediatR;

namespace GitManager.Application.Commands;

public record AddIssueCommand(string IssueName, string IssueDescription) : IRequest<string>;

public class AddIssueCommandHandler : IRequestHandler<AddIssueCommand, string>
{
    private readonly IGitIssueService _gitIssueService;

    public AddIssueCommandHandler(IGitIssueService gitIssueService)
    {
        _gitIssueService = gitIssueService;
    }

    public async Task<string> Handle(AddIssueCommand request, CancellationToken cancellationToken)
    {
        var response = await _gitIssueService.AddIssue(request.IssueName, request.IssueDescription);
        return response;
    }
}
