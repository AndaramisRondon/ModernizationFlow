using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ModernizationFlow.Application.Common;
using ModernizationFlow.Application.Interfaces;

namespace ModernizationFlow.Application.Requests.Reject;

public sealed class RejectRequestHandler
{
    private readonly IRequestRepository _repository;

    public RejectRequestHandler(IRequestRepository repository)
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
            request.Reject();

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

