using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjetNet.Startup))]
namespace ProjetNet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
