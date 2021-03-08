using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SURVEYTOOLSHP.Model;
namespace SURVEYTOOLSHP.Content.MenuPrincipal.AdministradorRespuestas
{
    public partial class ListarRespuestaDetalles : BasePage
    {
        private String nomEvaluado = "";
        private String nomEncuesta = "";
        private String nomTipoEncuesta = "";
        private int nomGeneral = 0;
        private String ponderado = "";
        private String idFase = "";
        protected void Page_Load(object sender, EventArgs e)
        { 
            if(HttpContext.Current.Session["PERMISOS"].ToString()=="1")
            cargarVariables();
            if (!Page.IsPostBack) {
                if (HttpContext.Current.Session["PERMISOS"].ToString() == "1")
                {
                    cargarNombreDatos();
                    cargarGrilla();
                    cargarGrillaEvaluador();
                }
            }
           

        }

        
        private void cargarVariables() {
            nomEvaluado = Request.Params["nomEvaluado"];
            nomEncuesta = Request.Params["IdEncuesta"];
            nomTipoEncuesta = Request.Params["IdTipoEncuesta"];
            nomGeneral = Convert.ToInt32(Request.Params["nomGeneral"]);
            ponderado = Request.Params["ponderado"];
            idFase = Request.Params["idFase"].ToString();
        
        }
        private void cargarNombreDatos() {
            this.txt_nombre_encuesta.Text = nomEvaluado;
            this.lbl_total_ponderacion_individual.Text = ponderado;
        }

    


        private void cargarGrilla()
        {
            Boolean returnError = false;
            DataSet ds = new DataSet();
            String strSQL = "EXEC SP_MM_DAR_LISTA_RESPUESTA_X_USUARIO_2 '"  + nomEvaluado + "'," + nomEncuesta +","+nomTipoEncuesta+","+nomGeneral+",1," + idFase;
            ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError)
            {
                if (ds.Tables.Count > 0)
                {
                    this.dgridRespuesta.DataSource = ds;
                    this.dgridRespuesta.DataBind();

                }
            }
        }
        private void cargarGrillaEvaluador() {
            
            String strSQL = "EXEC SP_MM_DAR_LISTA_RESPUESTA_X_USUARIO_2 '" + nomEvaluado + "'," + nomEncuesta + "," + nomTipoEncuesta + "," + nomGeneral + ",2,"+ idFase;
            Boolean returnError = false;
            DataSet ds = new DataSet();
            ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError) {
                if (ds.Tables.Count > 0) {
                    this.dgrid_evaluador_respuestas.DataSource = ds;
                    this.dgrid_evaluador_respuestas.DataBind();
                
                }
            
            }
        
        }

     

 

    

        

        protected void dgridRespuesta_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.dgridRespuesta.PageIndex = e.NewPageIndex;
            cargarGrilla();
        }

        protected void dgridRespuesta_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count >= 5) {
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[3].Width = 400;
            
            }
        }

        protected void img_btn_exportar_excel_Click(object sender, ImageClickEventArgs e)
        {
            GridView newdaGridView = new GridView();
            cargarGrillaExcel(ref newdaGridView);
            exportarGridView_Excel(newdaGridView, "Respuesta detallada", 2);

        }
        private void cargarGrillaExcel(ref GridView prmGridView) {
            Boolean returnError = false;
            DataSet ds = new DataSet();
            String strSQL = "EXEC SP_MM_DAR_LISTA_RESPUESTA_X_USUARIO '" + nomEvaluado + "'," + nomEncuesta + "," + nomTipoEncuesta + "," + nomGeneral;
            ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError)
            {
                if (ds.Tables.Count > 0)
                {
                    prmGridView.DataSource = ds;
                    prmGridView.DataBind();

                }
            }
        }

        protected void img_btn_ver_respuestas_evaluados_Click(object sender, ImageClickEventArgs e)
        {
            this.dgrid_evaluador_respuestas.Visible = true;
        }

        protected void dgrid_evaluador_respuestas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.dgrid_evaluador_respuestas.PageIndex = e.NewPageIndex;
            cargarGrillaEvaluador();
        }
    }
}