using MediatR;

namespace GitManager.Application.Commands;

public record AddIssueCommand(string IssueName, string IssueDescription) : IRequest<string>;

public class AddIssueCommandHandler : IRequestHandler<AddIssueCommand, string>
{
    public AddIssueCommandHandler()
    {
    }

    public async Task<string> Handle(AddIssueCommand request, CancellationToken cancellationToken)
    {
        return "";
    }
}
