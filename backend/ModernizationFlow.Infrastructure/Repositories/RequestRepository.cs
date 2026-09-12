using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using ModernizationFlow.Application.Interfaces;
using ModernizationFlow.Domain.Entities;
using ModernizationFlow.Infrastructure.Persistence;

namespace ModernizationFlow.Infrastructure.Repositories;

public sealed class RequestRepository
    : IRequestRepository
{
    private readonly AppDbContext _context;

    public RequestRepository(
        AppDbContext context)
    {
        _context = context;
    }

    public async Task<Request?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.Requests
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<IReadOnlyCollection<Request>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.Requests
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(
        Request request,
        CancellationToken cancellationToken)
    {
        await _context.Requests.AddAsync(
            request,
            cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(
            cancellationToken);
    }
}

