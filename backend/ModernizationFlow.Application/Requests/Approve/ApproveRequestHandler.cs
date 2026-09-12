using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ModernizationFlow.Application.Common;
using ModernizationFlow.Application.Interfaces;

namespace ModernizationFlow.Application.Requests.Approve;

public sealed class ApproveRequestHandler
{
    private readonly IRequestRepository _repository;

    public ApproveRequestHandler(IRequestRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> ExecuteAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var request = await _repository.GetByIdAsync(
            id,
            cancellationToken);

        if (request is null)
        {
            return Result.Failure(
                "Solicitação não encontrada.");
        }

        try
        {
            request.Approve();

            await _repository.SaveChangesAsync(
                cancellationToken);

            return Result.Success();
        }
        catch (InvalidOperationException ex)
        {
            return Result.Failure(ex.Message);
        }
    }
}

