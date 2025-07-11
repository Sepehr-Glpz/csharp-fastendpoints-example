using FastEndpoints;
using Microsoft.AspNetCore.Http;
using SGSX.Examples.FastEP.WebApi.Common;
using SGSX.Examples.FastEP.WebApi.Endpoints.Base;
using SGSX.Examples.FastEP.WebApi.Services;
using System.Net.Mime;

namespace SGSX.Examples.FastEP.WebApi.Endpoints.V1.User.GetById;

/// <summary>
/// Get a User By his Id
/// </summary>
public class GetUserByIdEndpoint(UserService userService) : BaseEndpoint<GetUserByIdRequest, GetUserByIdResponse>
{
    protected override void ConfigureEndpoint()
    {
        Get("{id}");
        Group<UserGroup>();
        Description(d =>
        {
            d.Produces<ApiResponse>(StatusCodes.Status404NotFound, MediaTypeNames.Application.Json, MediaTypeNames.Text.Plain);
        });
    }

    private readonly UserService _userService = userService;

    public override async Task<ApiResponse<GetUserByIdResponse>> ExecuteAsync(GetUserByIdRequest req, CancellationToken ct)
    {
        var user = _userService.GetById(req.Id);

        if (user is null)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;

            return new ApiResponse<GetUserByIdResponse>()
            {
                Success = false,
                Errors =
                [
                    new ApiError()
                    {
                        Code = "SearchError",
                        Message = "User Not Found",
                    }
                ],
            };
        }

        return new ApiResponse<GetUserByIdResponse>()
        {
            Success = true,
            Data = new GetUserByIdResponse()
            {
                Username = user.Username,
                Email = user.Email,
            },
        };
    }
}