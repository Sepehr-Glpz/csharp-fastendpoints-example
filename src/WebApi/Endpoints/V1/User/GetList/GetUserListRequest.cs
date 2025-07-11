using FastEndpoints;
using System.ComponentModel;

namespace SGSX.Examples.FastEP.WebApi.Endpoints.V1.User.GetList;
public sealed class GetUserListRequest
{
    /// <summary>
    /// the number of the page
    /// </summary>
    [BindFrom("page")]
    [DefaultValue(1)]
    public uint Page { get; init; } = 1;

    /// <summary>
    /// the amount of records in a page
    /// </summary>
    [BindFrom("limit")]
    [DefaultValue(10)]
    public uint Limit { get; init; } = 10;
}
