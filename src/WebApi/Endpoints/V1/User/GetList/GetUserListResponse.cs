using SGSX.Examples.FastEP.WebApi.Models;

namespace SGSX.Examples.FastEP.WebApi.Endpoints.V1.User.GetList;
public sealed class GetUserListResponse
{
    public IEnumerable<UserModel> Users { get; init; } = [];
}
