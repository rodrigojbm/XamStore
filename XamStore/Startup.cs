using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(XamStore.Startup))]
namespace XamStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
