using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Multitennant.Startup))]
namespace Multitennant
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
