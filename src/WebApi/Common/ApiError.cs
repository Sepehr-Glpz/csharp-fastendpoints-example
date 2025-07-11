namespace SGSX.Examples.FastEP.WebApi.Common;
public sealed class ApiError
{
    public required string Message { get; init; }

    public required string Code { get; init; }

    public override string ToString() => $"Code: {Code} - Message: {Message}";
}
