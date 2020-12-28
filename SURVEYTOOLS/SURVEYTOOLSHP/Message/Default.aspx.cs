using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SURVEYTOOLSHP.Message
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {
                cargarMensaje();
            
            }
        }
        private void cargarMensaje()
        {
            this.lbl_mensaje_descripcion.Text = HttpContext.Current.Session["SURVEY_DESCRIPCION_ERROR"].ToString();
            this.lbl_mensaje_titulo.Text = HttpContext.Current.Session["SURVEY_TITULO_ERROR"].ToString();
            this.img_mensaje.ImageUrl = HttpContext.Current.Session["SURVEY_IMAGEN_ERROR"].ToString();
            this.lbl_mensaje_descripcion.ForeColor = System.Drawing.Color.FromName(HttpContext.Current.Session["SURVEY_COLOR_DESCRIPCION_ERROR"].ToString());

        }
    } 
}