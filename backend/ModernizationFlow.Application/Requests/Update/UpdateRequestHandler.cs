using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using ModernizationFlow.Application.Common;
using ModernizationFlow.Application.Interfaces;

namespace ModernizationFlow.Application.Requests.Update;

public sealed class UpdateRequestHandler
{
    private readonly IRequestRepository _repository;

    public UpdateRequestHandler(IRequestRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> ExecuteAsync(
        UpdateRequestCommand command,
        CancellationToken cancellationToken)
    {
        var request = await _repository.GetByIdAsync(
            command.Id,
            cancellationToken);

        if (request is null)
        {
            return Result.Failure(
                "Solicitação não encontrada.");
        }

        if (string.IsNullOrWhiteSpace(command.Title))
        {
            return Result.Failure(
                "O título da solicitação é obrigatório.");
        }

        if (command.Amount <= 0)
        {
            return Result.Failure(
                "O valor da solicitação deve ser maior que zero.");
        }

        try
        {
            request.Update(
                command.Title.Trim(),
                command.Description.Trim(),
                command.Amount);

            await _repository.SaveChangesAsync(
                cancellationToken);

            return Result.Success();
        }
        catch (InvalidOperationException ex)
        {
            return Result.Failure(ex.Message);
        }
    }
}

