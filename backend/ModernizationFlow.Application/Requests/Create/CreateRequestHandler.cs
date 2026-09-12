using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using ModernizationFlow.Application.Common;
using ModernizationFlow.Application.Interfaces;
using ModernizationFlow.Domain.Entities;

namespace ModernizationFlow.Application.Requests.Create;

public sealed class CreateRequestHandler
{
    private readonly IRequestRepository _repository;

    public CreateRequestHandler(IRequestRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<CreateRequestResult>> ExecuteAsync(
        CreateRequestCommand command,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(command.Title))
        {
            return Result<CreateRequestResult>.Failure(
                "O título da solicitação é obrigatório.");
        }

        if (command.Amount <= 0)
        {
            return Result<CreateRequestResult>.Failure(
                "O valor da solicitação deve ser maior que zero.");
        }

        var request = new Request(
            command.Title.Trim(),
            command.Description.Trim(),
            command.Amount);

        await _repository.AddAsync(
            request,
            cancellationToken);

        await _repository.SaveChangesAsync(
            cancellationToken);

        var result = new CreateRequestResult(
            request.Id,
            request.Title,
            request.Description,
            request.Amount,
            request.Status.ToString());

        return Result<CreateRequestResult>.Success(result);
    }
}

