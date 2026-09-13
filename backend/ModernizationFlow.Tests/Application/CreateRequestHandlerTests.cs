using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ModernizationFlow.Application.Requests.Create;
using ModernizationFlow.Domain.Enums;
using ModernizationFlow.Tests.Application.Fakes;
using ModernizationFlow.Tests.Common;

namespace ModernizationFlow.Tests.Application;

public class CreateRequestHandlerTests
{
    [Fact]
    public async Task ExecuteAsync_WhenCommandIsValid_ShouldCreateRequest()
    {
        var repository = new FakeRequestRepository();
        var handler = new CreateRequestHandler(repository);

        var command = new CreateRequestCommand(
            TestData.NewTitle(),
            TestData.DefaultDescription,
            TestData.DefaultAmount);

        var result = await handler.ExecuteAsync(
            command,
            CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);

        Assert.Single(repository.Requests);

        var createdRequest = repository.Requests.Single();

        Assert.Equal(command.Title, createdRequest.Title);
        Assert.Equal(command.Description, createdRequest.Description);
        Assert.Equal(command.Amount, createdRequest.Amount);
        Assert.Equal(RequestStatus.Draft, createdRequest.Status);
    }
}

