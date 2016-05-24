using System.Web.Http;
using System.Web.Http.Cors;

namespace XamStore.Application
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            var cors = new EnableCorsAttribute("https://sandbox.pagseguro.uol.com.br", "*", "*");
            config.EnableCors(cors);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}