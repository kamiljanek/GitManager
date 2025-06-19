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
        // var customer = await _customerRepository.GetByIdAsync(request.Input.CustomerId);
        //
        // customer.City = request.Input.City;
        //
        // await _customerRepository.UpdateAsync(customer);
    }
}
