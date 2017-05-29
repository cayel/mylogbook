using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyLogbook.Startup))]
namespace MyLogbook
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
