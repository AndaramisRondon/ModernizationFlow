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
    string StatusDescription,
    DateTime CreatedAt,
    DateTime? UpdatedAt)
{
    public static RequestDto FromEntity(Request request)
    {
        var status = request.Status.ToString();

        return new RequestDto(
            request.Id,
            request.Title,
            request.Description,
            request.Amount,
            status,
            GetStatusDescription(status),
            request.CreatedAt,
            request.UpdatedAt);
    }

    private static string GetStatusDescription(string status)
    {
        return status switch
        {
            "Draft" => "Rascunho",
            "UnderReview" => "Em análise",
            "Approved" => "Aprovado",
            "Rejected" => "Reprovado",
            _ => status
        };
    }
}


