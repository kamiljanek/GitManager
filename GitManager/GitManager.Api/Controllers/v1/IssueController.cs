using Asp.Versioning;
using GitManager.Application.Commands;
using GitManagerApi.Inputs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GitManagerApi.Controllers.v1;

[ApiVersion("1.0")]
public class IssueController : BaseController
{
    private readonly IMediator _mediator;

    public IssueController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Add new issue.
    /// </summary>
    /// <param name="input">Simple issue data.</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Url to new issue.</returns>
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<string>> Add([FromBody] AddIssueInput input, CancellationToken ct = default)
    {
        var command = new AddIssueCommand(input.IssueName, input.IssueDescription);
        var result = await _mediator.Send(command, ct);
        
        return Ok(result);
    }
    
    /// <summary>
    /// Modify existing issue.
    /// </summary>
    /// <param name="id">Existing issue identifier.</param>
    /// <param name="input">Issue data.</param>
    /// <param name="ct">Cancellation token</param>
    [HttpPut("{id:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateIssueInput input, CancellationToken ct = default)
    {
        var command = new UpdateIssueCommand(id, input.IssueName, input.IssueDescription);
        await _mediator.Send(command, ct);
        
        return Ok();
    }
    
    /// <summary>
    /// Close existing issue.
    /// </summary>
    /// <param name="id">Existing issue identifier.</param>
    /// <param name="ct">Cancellation token</param>
    [HttpPatch("{id:guid}/close")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Close([FromRoute] Guid id, CancellationToken ct = default)
    {
        var command = new CloseIssueCommand(id);
        await _mediator.Send(command, ct);
        
        return Ok();
    }
}
