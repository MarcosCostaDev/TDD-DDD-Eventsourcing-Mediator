using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace TheProject.Api.Security.Hangfire
{
    public class AllowConnectionsFilter : IDashboardAuthorizationFilter
    {

        public bool Authorize([NotNull] DashboardContext context)
        {
            return true;
        }
    }
}
