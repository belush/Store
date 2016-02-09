using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Store.WEB.Startup))]
namespace Store.WEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
