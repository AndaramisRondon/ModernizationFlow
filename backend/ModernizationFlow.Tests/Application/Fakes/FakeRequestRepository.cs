using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ModernizationFlow.Application.Interfaces;
using ModernizationFlow.Domain.Entities;

namespace ModernizationFlow.Tests.Application.Fakes;

public sealed class FakeRequestRepository : IRequestRepository
{
    private readonly List<Request> _requests = new();

    public IReadOnlyCollection<Request> Requests => _requests;

    public Task<Request?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var request = _requests.FirstOrDefault(x => x.Id == id);

        return Task.FromResult(request);
    }

    public Task<IReadOnlyCollection<Request>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return Task.FromResult<IReadOnlyCollection<Request>>(
            _requests.ToList());
    }

    public Task AddAsync(
        Request request,
        CancellationToken cancellationToken)
    {
        _requests.Add(request);

        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

