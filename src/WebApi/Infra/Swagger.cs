using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Builder;
using NSwag;

namespace SGSX.Examples.FastEP.WebApi.Infra;
public static class Swagger
{
    public static void AddSwaggerDocuments(this WebApplicationBuilder builder)
    {
        builder.Services.SwaggerDocument(doc =>
        {
            doc.ReleaseVersion = 1;

            doc.EnableJWTBearerAuth = false;

            doc.DocumentSettings = d =>
            {
                d.Title = "Fast Endpoints Example WebApi";
                d.DocumentName = "v1";
                d.Version = "v1";
            };
        });

        builder.Services.SwaggerDocument(doc =>
        {
            doc.ReleaseVersion = 2;

            doc.EnableJWTBearerAuth = false;

            doc.DocumentSettings = d =>
            {
                d.Title = "Fast Endpoints Example WebApi";
                d.DocumentName = "v2";
                d.Version = "v2";

                d.AddAuth(BasicAuthentication.AuthenticationScheme, new OpenApiSecurityScheme()
                {
                    Type = OpenApiSecuritySchemeType.Basic,
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Name = BasicAuthentication.HeaderName,
                    Scheme = BasicAuthentication.AuthenticationScheme,
                    Description = "Basic Authentication with Useranme and Password",
                });
            };
        });
    }
}
