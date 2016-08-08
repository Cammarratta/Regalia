using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InventoryMngmt.Web.Startup))]
namespace InventoryMngmt.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
