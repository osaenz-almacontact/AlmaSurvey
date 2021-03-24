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
                cargarGrilla();
            }
        }

        private void cargarFases()
        {
            String strSQL = "SELECT fas_id AS 'ID',fas_nombre AS 'NOMBRE' FROM T_FASE ";
            Boolean returnError = false;
            clsSQL.llenarComboBoxConSelect(ref returnError, strSQL, ref this.cmb_fase, "ID", "NOMBRE");

        }


        protected void cmb_fase_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.pnl_detalle_respuestas.Visible = false;
            String strSQL = "";

            strSQL = "EXEC [SP_MM_REPORTE_ENCUESTAS] 1030," + this.cmb_fase.SelectedItem.Value;

            Boolean returnError = false;
            DataSet ds = new DataSet();
            ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError)
            {
                dgrid_reporte_retiro.DataSource = ds;
                dgrid_reporte_retiro.DataBind();
                this.pnl_detalle_respuestas.Visible = true;
            }

        }


        protected void dgrid_reporte_retiro_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.dgrid_reporte_retiro.PageIndex = e.NewPageIndex;
            cargarGrilla();
        }

        private void cargarGrilla()
        {
            //String strSQL = "";

            //strSQL = "EXEC [SP_MM_REPORTE_ENCUESTAS] 1030," + this.cmb_fase.SelectedItem.Value;

            //Boolean returnError = false;
            //DataSet ds = new DataSet();
            //ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            //if (!returnError)
            //{
            //    dgrid_reporte_satisfaccion.DataSource = ds;
            //    dgrid_reporte_satisfaccion.DataBind();
            //    this.pnl_detalle_respuestas.Visible = true;
            //}

        }

        protected void img_btn_exportar_excel_Click(object sender, ImageClickEventArgs e)
        {
            GridView newDataGrid = new GridView();

            cargarrillaExcel(ref newDataGrid);
            exportarGridView_Excel(newDataGrid, "Listar Respuesta Retiro", 1);

        }

        private void cargarrillaExcel(ref GridView prmDGrid)
        {
            String strSQL = "";

            strSQL = "EXEC [SP_MM_REPORTE_ENCUESTAS] 1030," + this.cmb_fase.SelectedItem.Value;


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
    }
}