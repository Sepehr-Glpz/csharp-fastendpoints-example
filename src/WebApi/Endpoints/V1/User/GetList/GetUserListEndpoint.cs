using FastEndpoints;
using Microsoft.AspNetCore.Http;
using SGSX.Examples.FastEP.WebApi.Common;
using SGSX.Examples.FastEP.WebApi.Endpoints.Base;
using SGSX.Examples.FastEP.WebApi.Services;
using System.Net.Mime;

namespace SGSX.Examples.FastEP.WebApi.Endpoints.V1.User.GetList;

public class GetUserListEndpoint(UserService userService) : BaseEndpoint<GetUserListRequest, GetUserListResponse>
{
    protected override void ConfigureEndpoint()
    {
        Get("/");
        Group<UserGroup>();
        Summary(new EndpointSummary()
        {
            Summary = "get a list of users",
        });
    }

    private readonly UserService _userService = userService;

    public override async Task<ApiResponse<GetUserListResponse>> ExecuteAsync(GetUserListRequest req, CancellationToken ct)
    {
        var result = _userService.GetList(req.Page, req.Limit);

        return new ApiResponse<GetUserListResponse>()
        {
            Success = true,
            Data = new GetUserListResponse()
            { 
                Users = result,
            },
        };
    }
}
