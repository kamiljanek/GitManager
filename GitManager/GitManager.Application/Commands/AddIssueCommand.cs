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
        // var customer = await _customerRepository.GetByIdAsync(request.Input.CustomerId);
        //
        // customer.City = request.Input.City;
        //
        // await _customerRepository.UpdateAsync(customer);

        return "";
    }
}
