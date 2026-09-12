using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ModernizationFlow.Application.Common;
using ModernizationFlow.Application.Interfaces;
using ModernizationFlow.Domain.Entities;

namespace ModernizationFlow.Application.Requests.Search;

public sealed class SearchRequestsHandler
{
    private readonly IRequestRepository _repository;

    public SearchRequestsHandler(IRequestRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IReadOnlyCollection<Request>>> ExecuteAsync(
        CancellationToken cancellationToken)
    {
        var requests = await _repository.GetAllAsync(
            cancellationToken);

        return Result<IReadOnlyCollection<Request>>
            .Success(requests);
    }
}


