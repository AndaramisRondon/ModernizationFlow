using ModernizationFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernizationFlow.Application.Interfaces;

public interface IRequestRepository
{
    Task<Request?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<IReadOnlyCollection<Request>> GetAllAsync(
        CancellationToken cancellationToken);

    Task AddAsync(
        Request request,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(
        CancellationToken cancellationToken);
}