using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Minesweeper.Web.Startup))]
namespace Minesweeper.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
