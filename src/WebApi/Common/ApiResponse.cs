
using FastEndpoints;
using Microsoft.Extensions.Primitives;

namespace SGSX.Examples.FastEP.WebApi.Common;
public class ApiResponse
{
    public required bool Success { get; init; }

    public ApiError[] Errors { get; init; } = [];
}

public class ApiResponse<TData> : ApiResponse
{
    public TData? Data { get; init; } = default;
}