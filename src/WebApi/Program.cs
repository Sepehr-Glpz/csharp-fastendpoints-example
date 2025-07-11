using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using FastEndpoints;
using FastEndpoints.Swagger;
using SGSX.Examples.FastEP.WebApi.Infra;
using SGSX.Examples.FastEP.WebApi.Common;
using Bogus;
using SGSX.Examples.FastEP.WebApi.Services;
using Microsoft.AspNetCore.Authentication;


namespace SGSX.Examples.FastEP.WebApi;
public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddFastEndpoints(f =>
        {
            f.DisableAutoDiscovery = false;
            f.Assemblies = [typeof(Program).Assembly];
        });

        builder.AddSwaggerDocuments();

        builder.Services
            .AddAuthentication(BasicAuthentication.AuthenticationScheme)
            .AddScheme<AuthenticationSchemeOptions, BasicAuthentication>(BasicAuthentication.AuthenticationScheme, opt =>
            {
                opt.ClaimsIssuer = "SGSX";
            });

        builder.Services.AddAuthorization();


        builder.Services.AddScoped<UserService>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseFastEndpoints(fe =>
        {
            fe.Endpoints.RoutePrefix = "api";

            fe.Versioning.DefaultVersion = 1;
            fe.Versioning.PrependToRoute = true;
            fe.Versioning.Prefix = "v";

            fe.Errors.ResponseBuilder = (errors, ctx, _) =>
            {
                ctx.Response.StatusCode = StatusCodes.Status400BadRequest;

                return new ApiResponse()
                {
                    Success = false,
                    Errors =
                    [
                        ..errors.Select(s => new ApiError()
                        {
                            Code = s.ErrorCode,
                            Message = s.ErrorMessage,
                        })
                    ]
                };
            };

            fe.Validation.UsePropertyNamingPolicy = false;
        })
        .UseSwaggerGen();

        await app.RunAsync();
    }
}
