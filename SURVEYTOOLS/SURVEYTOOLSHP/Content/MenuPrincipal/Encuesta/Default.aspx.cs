using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SURVEYTOOLSHP.Model;
using System.Data;

namespace SURVEYTOOLSHP.Content.MenuPrincipal.Encuesta
{
    public partial class Default : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack){
            cargarGrilla();
            
            }
        }

        protected void dgrid_encuesta_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            String opc = e.CommandName;
            switch (opc) { 
                case "Select":
                    abrirEncuesta(e.CommandArgument.ToString());
                    break;
            }
        }

        private void abrirEncuesta(String prmNumFila)
        {
            String url = this.dgrid_encuesta.Rows[Convert.ToInt32(prmNumFila)].Cells[3].Text + "&&idFase="+cargarFaseActual();
            Response.Redirect(url);

        }
        private String  cargarFaseActual() {
            String strSQL = "SET DATEFORMAT DMY;  SELECT fas_id FROM T_FASE WHERE '" + DateTime.Now.Date + "' BETWEEN fas_fechaComienzo AND fas_fechaFinalizacion";
            Boolean returnError = false;
            String fase = "";
            fase = clsSQL.retornarDatoEscalar(ref returnError, strSQL).ToString();
            return fase;

        }

        private void cargarGrilla() {
            String strSQL = "EXEC [SP_MM_DAR_SUBMENU_SIN_SELECCIONAR] " + HttpContext.Current.Session["ID_PAGINA_MENU"].ToString() + "," + HttpContext.Current.Session["PERMISOS"].ToString();
            DataSet ds = new DataSet();
            Boolean returnError = false;
            ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError)
            {
                if (ds.Tables.Count > 0)
                {
                    this.dgrid_encuesta.DataSource = ds;
                    this.dgrid_encuesta.DataBind();
                }
            }
       
        }

        protected void dgrid_encuesta_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.dgrid_encuesta.PageIndex = e.NewPageIndex;
            cargarGrilla();
        }

        protected void dgrid_encuesta_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count >= 4) {
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[3].Visible = false;
            }
        }
    }
}