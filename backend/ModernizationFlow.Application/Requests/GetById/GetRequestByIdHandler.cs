using ModernizationFlow.Application.Common;
using ModernizationFlow.Application.Interfaces;
using ModernizationFlow.Application.Requests.Common;
using ModernizationFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernizationFlow.Application.Requests.GetById;

public sealed class GetRequestByIdHandler
{
    private readonly IRequestRepository _repository;

    public GetRequestByIdHandler(IRequestRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<RequestDto>> ExecuteAsync(
            Guid id,
            CancellationToken cancellationToken)
    {
        var request = await _repository.GetByIdAsync(
            id,
            cancellationToken);

        if (request is null)
        {
            return Result<RequestDto>.Failure(
                "Solicitação não encontrada.");
        }

        return Result<RequestDto>.Success(
            RequestDto.FromEntity(request));
    }
}
