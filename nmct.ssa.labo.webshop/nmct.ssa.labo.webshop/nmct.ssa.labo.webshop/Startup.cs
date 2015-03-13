using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(nmct.ssa.labo.webshop.Startup))]
namespace nmct.ssa.labo.webshop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
