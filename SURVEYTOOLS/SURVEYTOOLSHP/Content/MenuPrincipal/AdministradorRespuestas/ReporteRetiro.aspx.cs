using SURVEYTOOLSHP.Model;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SURVEYTOOLSHP.Content.MenuPrincipal.AdministradorRespuestas
{
    public partial class ReporteRetiro : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cargarFases();
                cargarOperaicion();
                cargarSite();
                cargarLider();
                cargarGrilla();
            }
        }

        private void cargarFases()
        {
            String strSQL = "SELECT fas_id AS 'ID',fas_nombre AS 'NOMBRE' FROM T_FASE ";
            Boolean returnError = false;
            clsSQL.llenarComboBoxConSelect(ref returnError, strSQL, ref this.cmb_fase, "ID", "NOMBRE");

        }

        private void cargarOperaicion()
        {
            String strSQL = "SELECT opr_id AS 'ID',opr_nombre AS 'NOMBRE' FROM T_OPERACION ";
            Boolean returnError = false;
            clsSQL.llenarComboBoxConSelect(ref returnError, strSQL, ref this.cmb_operacion, "ID", "NOMBRE");

        }

        private void cargarSite()
        {
            String strSQL = "SELECT site_id AS 'ID',site_nombre AS 'NOMBRE' FROM T_SITE ";
            Boolean returnError = false;
            clsSQL.llenarComboBoxConSelect(ref returnError, strSQL, ref this.cmb_site, "ID", "NOMBRE");

        }

        private void cargarLider()
        {
            String strSQL = "SELECT DISTINCT(rep_evaluado) AS 'ID',rep_evaluado AS 'NOMBRE' FROM T_PREGUNTA_RESPUESTA ORDER BY rep_evaluado";
            Boolean returnError = false;
            clsSQL.llenarComboBoxConSelect(ref returnError, strSQL, ref this.cmb_lider, "ID", "NOMBRE");

        }

        protected void cmb_fase_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.pnl_detalle_respuestas.Visible = false;
            //String strSQL = "";

            //strSQL = "EXEC [SP_MM_REPORTE_ENCUESTAS] 1030," + this.cmb_fase.SelectedItem.Value;

            //Boolean returnError = false;
            //DataSet ds = new DataSet();
            //ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            //if (!returnError)
            //{
            //    RptPreguntas.DataSource = ds;
            //    RptPreguntas.DataBind();
            //    this.pnl_detalle_respuestas.Visible = true;
            //}


        }



        private void cargarGrilla()
        {
            String strSQL = "";

            strSQL = "SELECT pre_id AS 'ID', pre_descripcion AS 'PREGUNTA', pre_tip_id AS 'TIPO' FROM T_PREGUNTA WHERE pre_enc_id = 33 AND pre_estado = 1";

            Boolean returnError = false;

            DataSet dsrRpt = new DataSet();
            dsrRpt = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError)
            {

                RptPreguntas.DataSource = dsrRpt;
                RptPreguntas.DataBind();
                this.pnl_detalle_respuestas.Visible = true;

            }

        }

        protected void RptRespuestas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            String strSQL = "";
            string fechaInicio = TxtFechaInicio.Value == "" ? "0" : TxtFechaInicio.Value.Trim();
            string fechaCorte = TxtFechaCierre.Value == "" ? "0" : TxtFechaCierre.Value.Trim();

            if (e.Item.ItemType == ListItemType.Item ||
                   e.Item.ItemType == ListItemType.AlternatingItem)
            {
                TextBox TxtPregunta = (TextBox)e.Item.FindControl("TxtIdPregunta");
                strSQL = "EXEC [SP_MM_REPORTE_ENCUESTAS_RETIRO] '" + cmb_fase.SelectedValue.Trim() +
                         "', '" + TxtPregunta.Text.Trim() +
                         "', " + cmb_operacion.SelectedValue.Trim() +
                         ", " + cmb_site.SelectedValue.Trim() +
                         ", '" + cmb_lider.SelectedValue.Trim() + "'" +
                         ", '" + fechaInicio.Trim() + "'" +
                         ", '" + fechaCorte.Trim() + "'";


                //strSQL = "SELECT CAST(rep_respuesta AS VARCHAR(200)) AS 'RESPUESTA', " +
                //     "COUNT(CAST(rep_respuesta AS VARCHAR(50))) AS 'CONTADOR' " +
                //     "FROM T_PREGUNTA_RESPUESTA " +
                //     "WHERE CAST(rep_pregunta AS VARCHAR(200)) = '"+ TxtPregunta.Text.Trim() + "' " +
                //     "GROUP BY CAST(rep_respuesta AS VARCHAR(200))";
                try
                {
                    Boolean returnError = false;
                    DataSet ds = new DataSet();
                    ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);

                    Repeater RptRespuestas = (Repeater)e.Item.FindControl("RptRespuestas");

                    RptRespuestas.DataSource = ds;
                    RptRespuestas.DataBind();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        protected void img_btn_exportar_excel_Click(object sender, ImageClickEventArgs e)
        {
            GridView newDataGrid = new GridView();

            cargarrillaExcel(ref newDataGrid);
            exportarGridView_Excel(newDataGrid, "Listar Respuesta Satisdaccion", 1);

        }

        private void cargarrillaExcel(ref GridView prmDGrid)
        {
            String strSQL = "";

            strSQL = "EXEC [SP_MM_REPORTE_ENCUESTAS] 33," + this.cmb_fase.SelectedItem.Value;


            Boolean returnError = false;
            DataSet ds = new DataSet();
            ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError)
            {
                //ds.Tables[0].Columns.Remove("idTipoEncuesta");
                //ds.Tables[0].Columns.Remove("idEncuesta");
                prmDGrid.DataSource = ds;
                prmDGrid.DataBind();
            }
        }

        protected void BtnFiltrar_Click(object sender, EventArgs e)
        {
            cargarGrilla();
        }
    }
}


