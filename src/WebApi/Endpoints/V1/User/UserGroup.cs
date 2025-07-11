using FastEndpoints;
using SGSX.Examples.FastEP.WebApi.Infra;

namespace SGSX.Examples.FastEP.WebApi.Endpoints.V1.User;

public class UserGroup : SubGroup<RootGroup>
{
    public UserGroup()
    {
        Configure("user", g =>
        {
            g.AllowAnonymous();
            g.ApiVersion(1);
        });
    }
}
