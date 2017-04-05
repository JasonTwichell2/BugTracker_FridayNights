using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BugTracker_FridayNights.Startup))]
namespace BugTracker_FridayNights
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
