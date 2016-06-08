using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GYMFIND.Startup))]
namespace GYMFIND
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
