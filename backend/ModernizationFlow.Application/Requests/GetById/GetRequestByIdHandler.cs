using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ModernizationFlow.Application.Common;
using ModernizationFlow.Application.Interfaces;
using ModernizationFlow.Domain.Entities;

namespace ModernizationFlow.Application.Requests.GetById;

public sealed class GetRequestByIdHandler
{
    private readonly IRequestRepository _repository;

    public GetRequestByIdHandler(IRequestRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Request>> ExecuteAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var request = await _repository.GetByIdAsync(
            id,
            cancellationToken);

        if (request is null)
        {
            return Result<Request>.Failure(
                "Solicitação não encontrada.");
        }

        return Result<Request>.Success(request);
    }
}
