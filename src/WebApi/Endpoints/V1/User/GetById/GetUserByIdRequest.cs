using FastEndpoints;

namespace SGSX.Examples.FastEP.WebApi.Endpoints.V1.User.GetById;

public sealed class GetUserByIdRequest
{
    /// <summary>
    /// The Id of the user
    /// </summary>
    [BindFrom("id")]
    public required long Id { get; init; }
}