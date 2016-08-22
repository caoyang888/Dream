using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Dream.Admin.Startup))]
namespace Dream.Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

        }
    }
}
