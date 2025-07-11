using FastEndpoints;
using SGSX.Examples.FastEP.WebApi.Infra;

namespace SGSX.Examples.FastEP.WebApi.Endpoints.V2.User;

public class UserGroup : SubGroup<RootGroup>
{
    public UserGroup()
    {
        Configure("user", g =>
        {
            g.AuthSchemes(BasicAuthentication.AuthenticationScheme);
            g.ApiVersion(2);
        });
    }
}
