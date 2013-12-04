using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AngularJSMVC.Startup))]
namespace AngularJSMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
