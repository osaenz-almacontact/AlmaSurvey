using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SURVEYTOOLSHP.Model;

namespace SURVEYTOOLSHP
{
    public partial class Defualt : System.Web.UI.Page
    {
        private String strMensajeError = "";
        BasePage objBase = new BasePage();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            clsLogin login = new clsLogin();
            login.AsignarConectionString();
            if (!Page.IsPostBack) {
                this.lbl_Pagina.Text = HttpContext.Current.Session["PIE_PAGINA"].ToString();
    
                                     
            }
            this.pnl_Mensaje.Visible = false;
        }

        
        protected void Button2_Click(object sender, EventArgs e)
        {
      
            this.pnl_Login.Visible = true;


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String strSQL = "SELECT COUNT(*) FROM T_USUARIO WHERE usu_login = '"+ this.txt_Usuario.Text.Trim() +"' AND usu_contrasenia = '" + this.txt_contrasenia.Text.Trim() +"'" ;
            Boolean returnError = false;
            int autenticado = Convert.ToInt32(clsSQL.retornarDatoEscalar(ref returnError, strSQL));
            if (autenticado == 1)
            {
              HttpContext.Current.Session["LOGIN"]= this.txt_Usuario.Text;
                Response.Redirect("~/Content/Home.aspx", true);
            }
            else
            {
                strMensajeError = "El usuario o la contraseña no son correctos";

            }

            this.lbl_Mensaje.Text = strMensajeError;
            pnl_Mensaje.Visible = true;
        }
        private Boolean existeUsuario(String prmLogin) {
            Boolean returnError=false;
            String strSQL = "SELECT COUNT(*) FROM T_USUARIO WHERE usu_login = '" + prmLogin + "'";
            int contador =Convert.ToInt32(clsSQL.retornarDatoEscalar(ref returnError, strSQL));
            if (contador > 0)
                return true;
            else
                return false;
            
        }
    }
}
