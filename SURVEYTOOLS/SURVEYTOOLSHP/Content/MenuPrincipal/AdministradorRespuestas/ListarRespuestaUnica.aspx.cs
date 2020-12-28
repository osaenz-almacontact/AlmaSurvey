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
    public partial class ListarRespuestaUnica : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {
                cargarTipoEncuesta();
            }
        }
        private void cargarTipoEncuesta() {
            String strSQL = "SELECT  TIPO.tip_id AS 'ID', TIPO.tip_nombre AS 'NOMBRE' "+
                            "FROM T_ENCUESTA_USUARIO USUARIO "+
                            "INNER JOIN T_TIPO_ENCUESTA TIPO ON TIPO.tip_id = USUARIO.uen_tip_id " +
                            "WHERE USUARIO.uen_usu_id = 51 AND USUARIO.uen_estado = 1 AND TIPO.tip_estado = 1";
            Boolean returnError = false;
            clsSQL.llenarComboBox(ref returnError, strSQL, ref this.cmb_tipo_encuesta, "ID", "NOMBRE");
            if (!returnError && this.cmb_tipo_encuesta.Items.Count > 0) {
                cargarFiltro();
            }


        }
        private void cargarFiltro() {
            String opc = this.cmb_tipo_encuesta.SelectedItem.Value;
            this.rdl_filtrar.Items.Clear();
            switch (opc) { 
                    
                case "1":
                    ListItem item1 = new ListItem();
                    item1.Value = "Por_Unidad_Servicio";
                    item1.Text = "Por Unidad";
                    this.rdl_filtrar.Items.Add(item1);
                    break;
                case "2":
                    ListItem item2 = new ListItem();
                    item2.Value = "Por_Unidad";
                    item2.Text = "Por Unidad";
                      ListItem item3 = new ListItem();
                    item3.Value = "Por_ISR";
                    item3.Text = "Por ISR";
                    this.rdl_filtrar.Items.Add(item2);
                    this.rdl_filtrar.Items.Add(item3);
                    break;
            }
        }

        private void cargarPermisosXBu() {
            String opc = HttpContext.Current.Session["PERMISOS"].ToString();
            switch (opc) {
                case "Por_Unidad_Servicio":
                    break;
            
            }
        }
        private void cargarGrilla() {
            String opc = (this.rdl_filtrar.SelectedItem != null) ? this.rdl_filtrar.SelectedItem.Value : "";
            Boolean returnError = false;
            String strSQL = "", strSQL2 = "" ;
            
            switch (opc) { 
                case "Por_Unidad_Servicio":
                 

                    strSQL = "EXEC SP_MM_DAR_RESPUESTAS_BU 1,"+this.cmb_encuesta.SelectedItem.Value+",0,'"+ this.cmb_encuesta.SelectedItem.Text + "',0";
                    strSQL2 = "EXEC SP_MM_DAR_RESPUESTAS_BU 1," + this.cmb_encuesta.SelectedItem.Value + ",0,'" + this.cmb_encuesta.SelectedItem.Text + "',1";
                    break;
                case "Por_Unidad":
                    
                    strSQL = "EXEC SP_MM_DAR_RESPUESTAS_BU 2," + this.cmb_encuesta.SelectedItem.Value + ",1,'" + this.cmb_encuesta.SelectedItem.Text + "',0";
                    strSQL2 = "EXEC SP_MM_DAR_RESPUESTAS_BU 2," + this.cmb_encuesta.SelectedItem.Value + ",1,'" + this.cmb_encuesta.SelectedItem.Text + "',1";
                    break;
                case "Por_ISR":
                    
                    strSQL = "EXEC SP_MM_DAR_RESPUESTAS_BU 2," + this.cmb_encuesta.SelectedItem.Value + ",2,'" + this.cmb_isr.SelectedItem.Text + "',0";
                    strSQL2 = "EXEC SP_MM_DAR_RESPUESTAS_BU 2," + this.cmb_encuesta.SelectedItem.Value + ",2,'" + this.cmb_isr.SelectedItem.Text + "',1";
                   
                    break;

            }
            DataSet ds = new DataSet();
            ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError) {
                if (ds.Tables.Count > 0) {
                    this.dgrid_respuesta.DataSource = ds;
                    this.dgrid_respuesta.DataBind();
                }
           
            }
            this.pnl_grid.Visible = true;
            this.lbl_ponderado_total.Text = (clsSQL.retornarDatoEscalar(ref returnError, strSQL2) != null) ? clsSQL.retornarDatoEscalar(ref returnError, strSQL2) .ToString(): "0";
        }
        private void cargarCombos()
        {
            String permisos = HttpContext.Current.Session["PERMISOS"].ToString();
            String opc =  (this.rdl_filtrar.SelectedItem!=null) ? this.rdl_filtrar.SelectedItem.Value : "";
            Boolean returnError = false;
            String strSQL = "";
            
         
            switch (opc)
            {
                case "Por_Unidad_Servicio":
                    this.pnl_unidad.Visible = true;
                    this.pnl_isr.Visible = false;
                    strSQL = "SELECT NOM_ENCUESTA.enc_id AS 'ID', NOM_ENCUESTA.enc_nombre AS 'NOMBRE'" +
                             "FROM T_ENCUESTA_USUARIO USUARIO " +
                             "INNER JOIN T_UNIDAD_ENCUESTA ENCUESTA ON ENCUESTA.uni_uen_id = USUARIO.uen_id " +
                             "INNER JOIN T_ENCUESTA NOM_ENCUESTA ON NOM_ENCUESTA.enc_id = ENCUESTA.uni_enc_id " +
                             "WHERE USUARIO.uen_usu_id = 51 "+ " AND NOM_ENCUESTA.enc_estado = 1 AND USUARIO.uen_estado = 1 AND ENCUESTA.uni_estado = 1 AND NOM_ENCUESTA.enc_tip_id = 1";
                    clsSQL.llenarComboBox(ref returnError, strSQL, ref this.cmb_encuesta, "ID", "NOMBRE");
                    break;
                case "Por_Unidad":
                    this.pnl_unidad.Visible = true;
                    this.pnl_isr.Visible = false;
                    strSQL = "SELECT NOM_ENCUESTA.enc_id AS 'ID', NOM_ENCUESTA.enc_nombre AS 'NOMBRE'" +
                             "FROM T_ENCUESTA_USUARIO USUARIO " +
                             "INNER JOIN T_UNIDAD_ENCUESTA ENCUESTA ON ENCUESTA.uni_uen_id = USUARIO.uen_id " +
                             "INNER JOIN T_ENCUESTA NOM_ENCUESTA ON NOM_ENCUESTA.enc_id = ENCUESTA.uni_enc_id " +
                             "WHERE USUARIO.uen_usu_id = 51 " + " AND NOM_ENCUESTA.enc_estado = 1 AND USUARIO.uen_estado = 1 AND ENCUESTA.uni_estado = 1 AND NOM_ENCUESTA.enc_tip_id = 2";
                    clsSQL.llenarComboBox(ref returnError, strSQL, ref this.cmb_encuesta, "ID", "NOMBRE");
                break;
                case "Por_ISR":
                this.pnl_unidad.Visible = true;
                    this.pnl_isr.Visible = true;
                    strSQL = "SELECT NOM_ENCUESTA.enc_id AS 'ID', NOM_ENCUESTA.enc_nombre AS 'NOMBRE'" +
                             "FROM T_ENCUESTA_USUARIO USUARIO " +
                             "INNER JOIN T_UNIDAD_ENCUESTA ENCUESTA ON ENCUESTA.uni_uen_id = USUARIO.uen_id " +
                             "INNER JOIN T_ENCUESTA NOM_ENCUESTA ON NOM_ENCUESTA.enc_id = ENCUESTA.uni_enc_id " +
                             "WHERE USUARIO.uen_usu_id = 51"+ " AND NOM_ENCUESTA.enc_estado = 1 AND USUARIO.uen_estado = 1 AND ENCUESTA.uni_estado = 1 AND NOM_ENCUESTA.enc_tip_id = 2";
                    clsSQL.llenarComboBox(ref returnError, strSQL, ref this.cmb_encuesta, "ID", "NOMBRE");
                    cargarIsr();         
                break;

            }
        }

        private void cargarIsr() {
            String strSQL = "SELECT DISTINCT ISR.isr_nombre AS 'ID', ISR.isr_nombre AS 'NOMBRE' "+
                " FROM T_ISR ISR "+
                " INNER JOIN T_UNIDAD_ENCUESTA ENCUESTA ON ENCUESTA.uni_id = ISR.isr_uni_id "+
                " WHERE ENCUESTA.uni_enc_id = " + this.cmb_encuesta.SelectedItem.Value + " AND ISR.isr_estado = 1 ";
            Boolean returnError = false;
            clsSQL.llenarComboBox(ref returnError, strSQL, ref this.cmb_isr, "ID", "NOMBRE");
        }

        protected void rdl_filtrar_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarCombos();
            cargarGrilla();
           
        }

        protected void cmb_tipo_encuesta_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarFiltro();
            this.pnl_isr.Visible = false;
            this.pnl_unidad.Visible = false;
        }

        protected void dgrid_respuesta_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            this.lbl_ponderado_total.Text = this.dgrid_respuesta.Rows.Count.ToString();
        }

        protected void cmb_encuesta_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarIsr();
            cargarGrilla();
        }

        protected void cmb_isr_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarGrilla();
        }
    }
}