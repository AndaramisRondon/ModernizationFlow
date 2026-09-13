using Microsoft.AspNetCore.Mvc;
using ModernizationFlow.Application.Requests.Approve;
using ModernizationFlow.Application.Requests.Create;
using ModernizationFlow.Application.Requests.GetById;
using ModernizationFlow.Application.Requests.Reject;
using ModernizationFlow.Application.Requests.Search;
using ModernizationFlow.Application.Requests.Submit;
using ModernizationFlow.Application.Requests.Update;

namespace ModernizationFlow.Api.Controllers;

[ApiController]
[Route("api/requests")]
public sealed class RequestsController : ControllerBase
{
    private readonly CreateRequestHandler _createHandler;
    private readonly GetRequestByIdHandler _getByIdHandler;
    private readonly SearchRequestsHandler _searchHandler;
    private readonly UpdateRequestHandler _updateHandler;
    private readonly SubmitRequestHandler _submitHandler;
    private readonly ApproveRequestHandler _approveHandler;
    private readonly RejectRequestHandler _rejectHandler;

    public RequestsController(
        CreateRequestHandler createHandler,
        GetRequestByIdHandler getByIdHandler,
        SearchRequestsHandler searchHandler,
        UpdateRequestHandler updateHandler,
        SubmitRequestHandler submitHandler,
        ApproveRequestHandler approveHandler,
        RejectRequestHandler rejectHandler)
    {
        _createHandler = createHandler;
        _getByIdHandler = getByIdHandler;
        _searchHandler = searchHandler;
        _updateHandler = updateHandler;
        _submitHandler = submitHandler;
        _approveHandler = approveHandler;
        _rejectHandler = rejectHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateRequestCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _createHandler.ExecuteAsync(
            command,
            cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest(new
            {
                error = result.Error
            });
        }

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Value!.Id },
            result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> Search(
        CancellationToken cancellationToken)
    {
        var result = await _searchHandler.ExecuteAsync(
            cancellationToken);

        return Ok(result.Value);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _getByIdHandler.ExecuteAsync(
            id,
            cancellationToken);

        if (!result.IsSuccess)
        {
            return NotFound(new
            {
                error = result.Error
            });
        }

        return Ok(result.Value);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateRequestCommand command,
        CancellationToken cancellationToken)
    {
        var updateCommand = command with
        {
            Id = id
        };

        var result = await _updateHandler.ExecuteAsync(
            updateCommand,
            cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest(new
            {
                error = result.Error
            });
        }

        return NoContent();
    }

    [HttpPost("{id:guid}/submit")]
    public async Task<IActionResult> Submit(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _submitHandler.ExecuteAsync(
            id,
            cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest(new
            {
                error = result.Error
            });
        }

        return NoContent();
    }

    [HttpPost("{id:guid}/approve")]
    public async Task<IActionResult> Approve(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _approveHandler.ExecuteAsync(
            id,
            cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest(new
            {
                error = result.Error
            });
        }

        return NoContent();
    }

    [HttpPost("{id:guid}/reject")]
    public async Task<IActionResult> Reject(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _rejectHandler.ExecuteAsync(
            id,
            cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest(new
            {
                error = result.Error
            });
        }

        return NoContent();
    }
}


