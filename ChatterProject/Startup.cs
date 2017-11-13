using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChatterProject.Startup))]
namespace ChatterProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
