using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudentTracker.Startup))]
namespace StudentTracker
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
