using FastEndpoints;
using Microsoft.AspNetCore.Http;
using SGSX.Examples.FastEP.WebApi.Common;
using System.Net.Mime;

namespace SGSX.Examples.FastEP.WebApi.Endpoints.Base;
public abstract class BaseEndpoint<TRequest, TResponse> : Endpoint<TRequest, ApiResponse<TResponse>> where TRequest : notnull
{
    public override sealed void Configure()
    {
        ConfigureBase();
        ConfigureEndpoint();
    }

    private void ConfigureBase()
    {
        Description(c => c
            .Accepts<TRequest>()
            .Produces<ApiResponse<TResponse>>(StatusCodes.Status200OK, MediaTypeNames.Application.Json, MediaTypeNames.Text.Plain)
            .Produces<ApiResponse>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json, MediaTypeNames.Text.Plain)
        );
    }

    protected void ApiVersion(int version) => Version(version).StartingRelease(version).DeprecateAt(version + 1);

    protected abstract void ConfigureEndpoint();

    protected void SetStatusCode(int statusCode)
    {
        HttpContext.Response.StatusCode = statusCode;
    }
}
