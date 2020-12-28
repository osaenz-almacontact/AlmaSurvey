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
    public partial class RespuestaEspecifica : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {
                cargarDatos(); 
            }
        }
        private void cargarDatos() {
            if (verificarDatos()) {
                this.lbl_nom_encuesta.Text = HttpContext.Current.Session["ENCUESTA_NOM_ENCUESTA"].ToString();
                this.lbl_nombre_usuario.Text = HttpContext.Current.Session["ENCUESTA_NOM_USUARIO"].ToString();
                cargarGrillaRespuesta();
                cargarPonderacion();
            }
        
        }
        private void cargarGrillaRespuesta() {
            String idEncuesta = HttpContext.Current.Session["ENCUESTA_ID_ENCUESTA"].ToString();
            String idUsuario = HttpContext.Current.Session["ENCUESTA_ID_USUARIO"].ToString();
            String nomEvaluado = HttpContext.Current.Session["ENCUESTA_EVALUADO"].ToString();
            String fase = HttpContext.Current.Session["ENCUESTA_FASE"].ToString();
            String strSQL = "SELECT rep_id,rep_pregunta AS 'Pregunta',rep_respuesta AS 'Respuesta' FROM T_PREGUNTA_RESPUESTA WHERE rep_estado = 1 AND rep_respondidoXUsu = " + idUsuario + " AND rep_enc_id = " + idEncuesta + " AND rep_evaluado = '" + cambiarCaracter(nomEvaluado) + "' AND rep_fas_id = " + fase;
            DataSet ds = new DataSet();
            Boolean returnError=false;
            ds=clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError) {
                if (ds.Tables.Count > 0) {
                    dgrid_respuesta_especifica.DataSource = ds;
                    dgrid_respuesta_especifica.DataBind();
                }
            
            }
       
        
        
        }
        private void cargarPonderacion() {
            String idEncuesta = HttpContext.Current.Session["ENCUESTA_ID_ENCUESTA"].ToString();
            String idUsuario = HttpContext.Current.Session["ENCUESTA_ID_USUARIO"].ToString();
            String nomEvaluado = HttpContext.Current.Session["ENCUESTA_EVALUADO"].ToString();
            String nomUsuario = HttpContext.Current.Session["ENCUESTA_NOM_USUARIO"].ToString();
            String nomFase = HttpContext.Current.Session["ENCUESTA_FASE"].ToString();
            String strSQL = "EXEC [SP_MM_DAR_PONDERACION_INDIVIDUAL] " + idUsuario + "," + idEncuesta + ",'" + cambiarCaracter(nomEvaluado) + "'," + nomFase;
            Boolean returnError = false;
            String promedio = clsSQL.retornarDatoEscalar(ref returnError,strSQL).ToString();
            if (!returnError) {
                this.lbl_ponderacion.Text = (promedio.Length > 0) ? promedio : "";
                this.lbl_nombre_evaluado.Text = nomEvaluado;
                this.Lbl_usuario.Text = nomUsuario;
            }
            
        }
        private Boolean verificarDatos() {
            if (HttpContext.Current.Session["ENCUESTA_NOM_ENCUESTA"] != null && HttpContext.Current.Session["ENCUESTA_NOM_USUARIO"] != null && HttpContext.Current.Session["ENCUESTA_EVALUADO"] != null && HttpContext.Current.Session["ENCUESTA_ID_ENCUESTA"] != null && HttpContext.Current.Session["ENCUESTA_ID_USUARIO"] != null)
            {
                return true;
            }
            return false;
        }

        protected void dgrid_respuesta_especifica_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count >= 3) {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Width = 650;
                e.Row.Cells[2].Width = 150;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
            
            }
        }

        protected void dgrid_respuesta_especifica_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.dgrid_respuesta_especifica.PageIndex = e.NewPageIndex;
            cargarGrillaRespuesta();
            cargarPonderacion();
        }

        protected void lbl_volver_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session["ENCUESTA_ID_USUARIO"] = String.Empty;
            HttpContext.Current.Session["ENCUESTA_EVALUADO"] = String.Empty;
            HttpContext.Current.Session["ENCUESTA_NOM_USUARIO"] = String.Empty;
            HttpContext.Current.Session["ENCUESTA_NOM_USUARIO"] = String.Empty;
            Response.Redirect("~/Content/MenuPrincipal/AdministradorRespuestas/Efectividad.aspx");
        }
    }
}