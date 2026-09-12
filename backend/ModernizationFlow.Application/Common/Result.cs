using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernizationFlow.Application.Common;

public class Result
{
    public bool IsSuccess { get; }

    public string? Error { get; }

    private Result(
        bool isSuccess,
        string? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success()
        => new(true, null);

    public static Result Failure(string error)
        => new(false, error);
}

