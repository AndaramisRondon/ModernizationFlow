using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ModernizationFlow.Domain.Entities;
using ModernizationFlow.Domain.Enums;
using ModernizationFlow.Tests.Common;


namespace ModernizationFlow.Tests.Domain;

public class RequestTests
{
    [Fact]
    public void Submit_WhenRequestIsDraft_ShouldChangeStatusToUnderReview()
    {
        var request = new Request(
            TestData.NewTitle(),
            TestData.DefaultDescription,
            TestData.DefaultAmount);

        request.Submit();

        Assert.Equal(
            RequestStatus.UnderReview,
            request.Status);
    }

    [Fact]
    public void Approve_WhenRequestIsDraft_ShouldThrowException()
    {
        var request = new Request(
            TestData.NewTitle(),
            TestData.DefaultDescription,
            TestData.DefaultAmount);

        var exception = Assert.Throws<InvalidOperationException>(
            () => request.Approve());

        Assert.Equal(
            "Apenas solicitações em análise podem ser aprovadas.",
            exception.Message);
    }

    [Fact]
    public void Approve_WhenRequestIsUnderReview_ShouldChangeStatusToApproved()
    {
        var request = new Request(
            TestData.NewTitle(),
            TestData.DefaultDescription,
            TestData.DefaultAmount);

        request.Submit();
        request.Approve();

        Assert.Equal(
            RequestStatus.Approved,
            request.Status);
    }

    [Fact]
    public void Reject_WhenRequestIsUnderReview_ShouldChangeStatusToRejected()
    {
        var request = new Request(
            TestData.NewTitle(),
            TestData.DefaultDescription,
            TestData.DefaultAmount);

        request.Submit();
        request.Reject();

        Assert.Equal(
            RequestStatus.Rejected,
            request.Status);
    }
}
