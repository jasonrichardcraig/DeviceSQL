using Owin;
using System.Web.Http;

namespace DeviceSQL.Service
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var httpConfiguration = new HttpConfiguration();

            httpConfiguration.MapHttpAttributeRoutes();
            appBuilder.UseWebApi(httpConfiguration);
        }
    }
}
