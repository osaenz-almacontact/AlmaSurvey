using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.IO;
using System.Web.SessionState;

namespace SURVEYTOOLSHP
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
            #region metodo que se encargara de generar un archivo log si llega a ver algún error
            Application.Lock();
            Exception excepcion = new Exception();
            excepcion = Server.GetLastError().GetBaseException();
            String ambarTitle, ambarDescription;
            File.Delete(Server.MapPath("~/Log/System.log"));
            StreamWriter A_fichero = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + "/log/System.log", true);
            A_fichero.WriteLine("***********************");
            A_fichero.WriteLine();
            ambarTitle = System.DateTime.Now.Year.ToString() + "/" + System.DateTime.Now.Month.ToString() + "/" + System.DateTime.Now.Day.ToString() + "/" + System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + ":" + System.DateTime.Now.Second.ToString();
            ambarDescription = excepcion.Message;
            A_fichero.WriteLine(ambarTitle);
            A_fichero.WriteLine("Descripción: " + ambarDescription);
            A_fichero.WriteLine("Origen: " + excepcion.Source);
            A_fichero.WriteLine();
            A_fichero.WriteLine("***********************");
            A_fichero.Flush();
            A_fichero.Close();
            Application.UnLock();
            Response.Redirect("~/Default.aspx");
           

            #endregion
        }

        void Session_Start(object sender, EventArgs e)
        {
            BasePage.p_UrlError = "~/Message/Default.aspx";
            Session["language"]="es-ES";
            Session["keysession"] = 10001001;
            HttpContext.Current.Session["ID_ACTUAL_ENCUESTA"] = 0;
            HttpContext.Current.Session["HP_USU_ACTUAL"]=String.Empty;
            HttpContext.Current.Session["LOGIN"] = String.Empty;
            HttpContext.Current.Session["ID_USER"] = String.Empty;
            HttpContext.Current.Session["ID_PAGINA_MENU"] = String.Empty;
            HttpContext.Current.Session["USER"] = String.Empty;
            HttpContext.Current.Session["PERMISOS"] = String.Empty;
            HttpContext.Current.Session["EMAIL"] = String.Empty;
                       
            HttpContext.Current.Session["USER"] = String.Empty;
            HttpContext.Current.Session["ID_ACUTAL_EDITAR_USARIO"] = String.Empty;
        }

        void Session_End(object sender, EventArgs e)
        {
            BasePage page = new BasePage();
            
            this.Session.RemoveAll();
            this.Session.Contents.RemoveAll();
        
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
