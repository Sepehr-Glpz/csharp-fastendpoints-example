using FastEndpoints;

namespace SGSX.Examples.FastEP.WebApi.Endpoints;
public class RootGroup : Group
{
    public RootGroup()
    {
        Configure("/", e =>
        {
        });
    }
}
