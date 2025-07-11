using Microsoft.AspNetCore.Http;
using SGSX.Examples.FastEP.WebApi.Common;
using SGSX.Examples.FastEP.WebApi.Endpoints.Base;
using SGSX.Examples.FastEP.WebApi.Endpoints.V2.User;
using SGSX.Examples.FastEP.WebApi.Models;
using SGSX.Examples.FastEP.WebApi.Services;

namespace SGSX.Examples.FastEP.WebApi.Endpoints.V2.User.GetList;

/// <summary>
/// Search a list of users by username
/// </summary>
public class GetUserListEndpoint(UserService userService) : BaseEndpoint<GetUserListRequest, UserModel[]>
{
    protected override void ConfigureEndpoint()
    {
        Get("/search/{query}");
        Group<UserGroup>();
    }

    protected UserService UserService { get; } = userService;

    public override Task<ApiResponse<UserModel[]>> ExecuteAsync(GetUserListRequest req, CancellationToken ct)
    {
        var users = UserService.SearchByUsername(req.Query)
            .Skip((int)((req.Page - 1) * req.Limit))
            .Take((int)req.Limit)
            .ToArray();

        if(users is [])
        {
            SetStatusCode(StatusCodes.Status404NotFound);

            return Task.FromResult(new ApiResponse<UserModel[]>()
            {
                Success = false,
                Errors =
                [
                    new ApiError()
                    {
                        Code = "NotFound",
                        Message = "No users found matching the search criteria",
                    }
                ],
            });
        }

        var response = new ApiResponse<UserModel[]>()
        {
            Success = true,
            Data = users,
        };

        return Task.FromResult(response);
    }
}
