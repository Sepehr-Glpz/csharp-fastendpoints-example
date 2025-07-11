using FastEndpoints;

namespace SGSX.Examples.FastEP.WebApi.Endpoints.V1.User.GetById;

public sealed class GetUserByIdResponse
{
    public required string Username { get; init; }

    public required string Email { get; init; }
}