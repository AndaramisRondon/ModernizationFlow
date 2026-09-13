using ModernizationFlow.Application.Common;
using ModernizationFlow.Application.Interfaces;
using ModernizationFlow.Application.Requests.Common;
using ModernizationFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernizationFlow.Application.Requests.Search;

public sealed class SearchRequestsHandler
{
    private readonly IRequestRepository _repository;

    public SearchRequestsHandler(IRequestRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IReadOnlyCollection<RequestDto>>> ExecuteAsync(
        CancellationToken cancellationToken)
    {
        var requests = await _repository.GetAllAsync(
            cancellationToken);

        var result = requests
            .Select(RequestDto.FromEntity)
            .ToList()
            .AsReadOnly();

        return Result<IReadOnlyCollection<RequestDto>>
            .Success(result);
    }
}


