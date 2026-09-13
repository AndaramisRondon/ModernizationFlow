using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using ModernizationFlow.Domain.Entities;

namespace ModernizationFlow.Application.Requests.Common;

public sealed record RequestDto(
    Guid Id,
    string Title,
    string Description,
    decimal Amount,
    string Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt)
{
    public static RequestDto FromEntity(Request request)
    {
        return new RequestDto(
            request.Id,
            request.Title,
            request.Description,
            request.Amount,
            request.Status.ToString(),
            request.CreatedAt,
            request.UpdatedAt);
    }
}


