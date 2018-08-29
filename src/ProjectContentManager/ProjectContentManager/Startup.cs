using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectContentManager.Startup))]
namespace ProjectContentManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
