using System;
using System.Web.Routing;

namespace VentasCal
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup

        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            Exception exc = Server.GetLastError();
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
            RegisterRoutes(RouteTable.Routes);
        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

        void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("", "admin", "~/page/pa01.aspx");
            routes.MapPageRoute("", "producto", "~/page/pa02.aspx");
            routes.MapPageRoute("", "venta", "~/page/pa03.aspx");
            routes.MapPageRoute("", "perfil", "~/page/pa05.aspx");
            routes.MapPageRoute("", "usuario", "~/page/pa06.aspx");
        }

    }
}
