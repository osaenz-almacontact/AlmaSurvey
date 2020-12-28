using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SURVEYTOOLSHP.Model;
using System.Data;

namespace SURVEYTOOLSHP.Content.MenuPrincipal.AdministrarEncuestas
{
    public partial class Default : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {
                cargarGrilla();
            }
        }

        protected void dgridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            String opc = e.CommandName;
            switch (opc) { 
                case "Select":
                    abrirEncuesta(e.CommandArgument.ToString());
                    break;
                case "":
                    break;
            }
        }
        private void abrirEncuesta(String prmNumFila) {
            String url = this.dgrid_Encuesta.Rows[Convert.ToInt32(prmNumFila)].Cells[3].Text;
            Response.Redirect(url);
            
        
        }
        private void cargarGrilla() {
            String strSQL = "[SP_MM_DAR_SUBMENU_SIN_SELECCIONAR] " + HttpContext.Current.Session["ID_PAGINA_MENU"].ToString() + "," + HttpContext.Current.Session["PERMISOS"].ToString();
            DataSet ds = new DataSet();
            Boolean returnError = false;
            ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError) {
                if (ds.Tables.Count > 0) {
                    this.dgrid_Encuesta.DataSource = ds;
                    this.dgrid_Encuesta.DataBind();
                }
            }

        }

        protected void dgrid_Encuesta_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.dgrid_Encuesta.PageIndex = e.NewPageIndex;
            cargarGrilla();
        }

        protected void dgrid_Encuesta_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count >= 4) {
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[3].Visible = false;
            }
        }
    }
}