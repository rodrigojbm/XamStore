using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(XamStore.Application.Startup))]
namespace XamStore.Application
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
