using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Store.BLL.Interfaces;
using Store.BLL.Services;

[assembly: OwinStartup(typeof(Store.WEB.App_Start.Startup))]

namespace Store.WEB.App_Start
{
    public class Startup
    {
        //TODO: use ninject
        private readonly IServiceCreator serviceCreator = new ServiceCreator();

        public void Configuration(IAppBuilder app)
        {
            //from "public partial class Startup"
            AutoMapperConfig.RegisterMappings();

            app.CreatePerOwinContext(CreateUserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
        }

        private IUserService CreateUserService()
        {
            return serviceCreator.CreateUserService("StoreConnection");
        }
    }
}