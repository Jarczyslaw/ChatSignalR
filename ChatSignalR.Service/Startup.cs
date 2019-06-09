using Microsoft.Owin.Cors;
using Owin;

namespace ChatSignalR.Service
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