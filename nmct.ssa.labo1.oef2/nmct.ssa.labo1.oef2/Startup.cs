using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(nmct.ssa.labo1.oef2.Startup))]
namespace nmct.ssa.labo1.oef2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
