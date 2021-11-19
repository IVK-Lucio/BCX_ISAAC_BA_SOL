using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BCX_ISAAC_BA_SOL.Startup))]
namespace BCX_ISAAC_BA_SOL
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
