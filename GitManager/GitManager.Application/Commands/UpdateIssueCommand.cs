using MediatR;

namespace GitManager.Application.Commands;

public record UpdateIssueCommand(Guid Id, string IssueName, string IssueDescription) : IRequest;

public class UpdateIssueCommandHandler : IRequestHandler<UpdateIssueCommand>
{
    public UpdateIssueCommandHandler()
    {
    }

    public async Task Handle(UpdateIssueCommand request, CancellationToken cancellationToken)
    {
        // var customer = await _customerRepository.GetByIdAsync(request.Input.CustomerId);
        //
        // customer.City = request.Input.City;
        //
        // await _customerRepository.UpdateAsync(customer);
    }
}
