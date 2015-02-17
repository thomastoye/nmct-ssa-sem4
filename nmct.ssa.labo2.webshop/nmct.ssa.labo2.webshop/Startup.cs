using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(nmct.ssa.labo2.webshop.Startup))]
namespace nmct.ssa.labo2.webshop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
