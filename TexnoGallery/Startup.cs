using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TexnoGallery.Startup))]
namespace TexnoGallery
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
