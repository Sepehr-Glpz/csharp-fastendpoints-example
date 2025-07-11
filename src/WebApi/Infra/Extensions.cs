using FastEndpoints;

namespace SGSX.Examples.FastEP.WebApi.Infra;
public static class Extensions
{
    public static void ApiVersion(this EndpointDefinition endpoint, int version) =>
        endpoint
            .EndpointVersion(version, version + 1)
            .StartingRelease(version);

}
