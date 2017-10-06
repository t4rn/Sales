using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sales.Web.Startup))]
namespace Sales.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
