using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace SURVEYTOOLSHP.Control
{
    public partial class Usuario : System.Web.UI.UserControl
    {
        BasePage obj = new BasePage();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {
                cargarNombre();
            }
        }
        private void cargarNombre()
        {
            this.lbl_usuario.Text = HttpContext.Current.Session["USER"].ToString();
            if (HttpContext.Current.Session["PERMISOS"].ToString() == "1")
            {
                this.lbl_permiso.Text = "ADMINISTRADOR";
            }
            else {
                this.lbl_permiso.Text = "USUARIO";
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
           
            HttpContext.Current.Response.Redirect("~/Default.aspx", true);
           
        }
       
    }
  
}