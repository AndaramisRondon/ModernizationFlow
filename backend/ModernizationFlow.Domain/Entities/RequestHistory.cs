using ModernizationFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernizationFlow.Domain.Entities;

public class RequestHistory
{
    public Guid Id { get; private set; }

    public Guid RequestId { get; private set; }

    public RequestStatus FromStatus { get; private set; }

    public RequestStatus ToStatus { get; private set; }

    public string? Comment { get; private set; }

    public DateTime CreatedAt { get; private set; }

    private RequestHistory()
    {
    }

    public RequestHistory(
        Guid requestId,
        RequestStatus fromStatus,
        RequestStatus toStatus,
        string? comment = null)
    {
        Id = Guid.NewGuid();
        RequestId = requestId;
        FromStatus = fromStatus;
        ToStatus = toStatus;
        Comment = comment;
        CreatedAt = DateTime.UtcNow;
    }
}