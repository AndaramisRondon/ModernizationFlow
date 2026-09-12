using ModernizationFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernizationFlow.Domain.Entities;

public class Request
{
    public Guid Id { get; private set; }

    public string Title { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public decimal Amount { get; private set; }

    public RequestStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    private Request()
    {
    }

    public Request(
        string title,
        string description,
        decimal amount)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        Amount = amount;
        Status = RequestStatus.Draft;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(
        string title,
        string description,
        decimal amount)
    {
        if (Status != RequestStatus.Draft)
            throw new InvalidOperationException(
                "Apenas solicitações em rascunho podem ser atualizadas.");

        Title = title;
        Description = description;
        Amount = amount;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Submit()
    {
        if (Status != RequestStatus.Draft)
            throw new InvalidOperationException(
                "Apenas solicitações em rascunho podem ser enviadas.");

        Status = RequestStatus.UnderReview;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Approve()
    {
        if (Status != RequestStatus.UnderReview)
            throw new InvalidOperationException(
                "Apenas solicitações em análise podem ser aprovadas.");

        Status = RequestStatus.Approved;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Reject()
    {
        if (Status != RequestStatus.UnderReview)
            throw new InvalidOperationException(
                "Apenas solicitações em análise podem ser reprovadas.");

        Status = RequestStatus.Rejected;
        UpdatedAt = DateTime.UtcNow;
    }
}