using FastEndpoints;
using System.ComponentModel;

namespace SGSX.Examples.FastEP.WebApi.Endpoints.V2.User.GetList;
public sealed class GetUserListRequest
{

    /// <summary>
    /// a search query to filter users by username
    /// </summary>
    [BindFrom("query")]
    public required string Query { get; init; }

    /// <summary>
    /// Page number
    /// </summary>
    [DefaultValue(1)]
    [BindFrom("page")]
    public uint Page { get; init; } = 1;

    /// <summary>
    /// Page Size
    /// </summary>
    [DefaultValue(10)]
    [BindFrom("limit")]
    public uint Limit { get; init; } = 10;
}
