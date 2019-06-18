using Microsoft.Owin.Cors;
using Owin;

namespace ChatSignalR.Host
{
    internal class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
}