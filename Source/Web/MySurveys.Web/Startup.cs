using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MySurveys.Web.Startup))]
namespace MySurveys.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
