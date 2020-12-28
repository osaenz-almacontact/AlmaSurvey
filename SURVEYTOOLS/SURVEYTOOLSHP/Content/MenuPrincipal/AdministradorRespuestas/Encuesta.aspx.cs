using SURVEYTOOLSHP.Model;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SURVEYTOOLSHP.Content.MenuPrincipal.AdministradorRespuestas
{
    public partial class Satisfaccion : BasePage
    {
  
        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (!Page.IsPostBack) {
                cargarFases();
                cargarNombreEncuesta();
                cargarEncuestas();
                if(this.cmb_tipo_encuesta.Items.Count>0)
                cargarOpcionesRadioListButton();
            }
        }

        private void cargarFases() {
            String strSQL = "SELECT fas_id AS 'ID',fas_nombre AS 'NOMBRE' FROM T_FASE ";
            Boolean returnError = false;
            clsSQL.llenarComboBoxConSelect(ref returnError, strSQL, ref this.cmb_fase, "ID", "NOMBRE");

        }

        private void cargarNombreEncuesta()
        {

          
                this.lbl_nombre_encuesta.Text = "Ver respuestas";

        }

       

      

    
      
      
        private void cargarEncuestas() {
            String strSQL = "SELECT DISTINCT TIPO.tip_id AS 'ID' , TIPO.tip_nombre AS 'NOMBRE'FROM T_PREGUNTA_RESPUESTA RESPUESTA "+
            "INNER JOIN T_ENCUESTA ENCUESTA ON ENCUESTA.enc_id = RESPUESTA.rep_enc_id "+
            "INNER JOIN T_TIPO_ENCUESTA TIPO ON TIPO.tip_id = ENCUESTA.enc_tip_id "+
            "WHERE RESPUESTA.rep_fas_id = " + this.cmb_fase.SelectedItem.Value;
            Boolean returnError = false;
            clsSQL.llenarComboBox(ref returnError, strSQL, ref this.cmb_tipo_encuesta, "ID", "NOMBRE");
            if (!returnError) {
                if (this.cmb_tipo_encuesta.Items.Count > 0)
                {
                    this.pnl_tipo_encuesta.Visible = true;

                    encuestasTotal();
                    cargarOpcionesRadioListButton();
                    this.pnl_filtro.Visible = true;
                    this.cmb_tipo_encuesta.SelectedIndex = 0;
                    if (this.cmb_tipo_encuesta.SelectedItem.Value == "1")
                    {
                        this.pnl_encuesta.Visible = false;
                    }
                    else
                    {
                        this.pnl_encuesta.Visible = true;
                    }
                }
                else {
                    this.pnl_tipo_encuesta.Visible = false;
                    this.pnl_detalle_respuestas.Visible = false;
                    this.pnl_filtro.Visible = false;
                    this.pnl_encuesta.Visible = false;
                }
                
            }
        }
        private void encuestasTotal() {
            Boolean returnError=false;
            String strSQL = "SELECT DISTINCT ENCUESTA.enc_id AS 'ID' ,"+
                            " ENCUESTA.enc_nombre AS 'NOMBRE'"+
                            "FROM T_PREGUNTA_RESPUESTA RESPUESTA "+
                            "INNER JOIN T_ENCUESTA ENCUESTA ON ENCUESTA.enc_id = RESPUESTA.rep_enc_id "+
                            "WHERE ENCUESTA.enc_tip_id = " + this.cmb_tipo_encuesta.SelectedItem.Value + " AND RESPUESTA.rep_fas_id = " + this.cmb_fase.SelectedItem.Value;
            clsSQL.llenarComboBox(ref returnError, strSQL, ref this.cmb_encuestas, "ID", "NOMBRE");
           
        }
        

        
      

        protected void dgridRespuesta_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count >= 4) {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Width = 500;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
            }
        }



        private void verDetalle(int prmNumFila) {
            String nomEvaluado = this.dgrid_ponderado_total.Rows[prmNumFila].Cells[1].Text;
            String Id_tipo_Encuesta = this.dgrid_ponderado_total.Rows[prmNumFila].Cells[5].Text;
            String Id_Encuesta = this.dgrid_ponderado_total.Rows[prmNumFila].Cells[6].Text;
            String ponderado = this.dgrid_ponderado_total.Rows[prmNumFila].Cells[4].Text;
            String idFase = this.cmb_fase.SelectedItem.Value;
      
            int nomGeneral = 0;
            nomEvaluado = cambiarCaracter(nomEvaluado);
            
             
                if (nomEvaluado == "SERVICIO SCC")
                {
                    nomGeneral = 1;
                }
                else if (nomEvaluado.Length >= 12 && nomEvaluado.Substring(0, 12) == "Satisfacción")
                {
                    nomGeneral = 1;
                }
            

            Response.Redirect("~/Content/MenuPrincipal/AdministradorRespuestas/ListarRespuestaDetalles.aspx?nomEvaluado=" + nomEvaluado + "&&IdEncuesta=" + Id_Encuesta + "&&IdTipoEncuesta=" + Id_tipo_Encuesta + "&&nomGeneral=" + nomGeneral + "&&ponderado=" + ponderado + "&&idFase=" + idFase);
        }
        private void cargarGrillaPonderadoCabeza()
        {
            String strSQL = "";
            if (this.rdl_btn_filtro.SelectedItem != null)
            {
                String opc = this.rdl_btn_filtro.SelectedItem.Value;
                switch (opc)
                {
                    case "ServicioOP":
                        strSQL = "EXEC [SP_MM_DAR_LISTA_RESPUESTA_TOTAL_PONDERACION] 1,1,0,"+ this.cmb_fase.SelectedItem.Value;
                        break;
                    case "ServicioBu":
                        strSQL = "EXEC [SP_MM_DAR_LISTA_RESPUESTA_TOTAL_PONDERACION] 1,2," + this.cmb_encuestas.SelectedItem.Value +"," + this.cmb_fase.SelectedItem.Value;
                        break;
                    case "SatisfaccionISR":
                        strSQL = "EXEC [SP_MM_DAR_LISTA_RESPUESTA_TOTAL_PONDERACION] 2,1," + this.cmb_encuestas.SelectedItem.Value +"," + this.cmb_fase.SelectedItem.Value;
                        break;
                    case "SatisfaccionBU":
                        strSQL = "EXEC [SP_MM_DAR_LISTA_RESPUESTA_TOTAL_PONDERACION] 2,2," + this.cmb_encuestas.SelectedItem.Value + "," + this.cmb_fase.SelectedItem.Value;
                        break;
                    case "Liderazgo":
                        strSQL = "EXEC [SP_MM_DAR_LISTA_RESPUESTA_TOTAL_PONDERACION] 3,1," + this.cmb_encuestas.SelectedItem.Value + "," + this.cmb_fase.SelectedItem.Value;
                        break;

                }


                Boolean returnError = false;
                DataSet ds = new DataSet();
                ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
                if (!returnError)
                {
                    dgrid_ponderado_total.DataSource = ds;
                    dgrid_ponderado_total.DataBind();
                    this.pnl_detalle_respuestas.Visible = true;
                }
            }
        }

        protected void dgrid_ponderado_total_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName) {
                case "VerDetalles":
                    verDetalle(Convert.ToInt32(e.CommandArgument));
                    
                    break;
            }
        }

        protected void cmb_encuestas_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            cargarGrillaPonderadoCabeza();
        }

       

        protected void dgrid_ponderado_total_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count >= 6) {
                e.Row.Cells[0].Width = 70;
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                //e.Row.Cells[1].Visible = false;
                e.Row.Cells[4].Width = 70;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;
             
            }
        }

        protected void cmb_tipo_encuesta_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarOpcionesRadioListButton();
     
            encuestasTotal();

            cargarGrillaPonderadoCabeza();
        }

        private void cargarOpcionesRadioListButton() {
            if (this.cmb_tipo_encuesta.SelectedItem.Value == "1")
            {
                this.rdl_btn_filtro.Items.Clear();
                ListItem item = new ListItem();
                item.Value = "ServicioBu";
                item.Text = "Por Unidad";
                ListItem item2 = new ListItem();
                item2.Value = "ServicioOP";
                item2.Text = "Por la operación";
                this.rdl_btn_filtro.Items.Add(item);
                this.rdl_btn_filtro.Items.Add(item2);

            }
            if (this.cmb_tipo_encuesta.SelectedItem.Value == "2")
            {
                this.rdl_btn_filtro.Items.Clear();
                ListItem item = new ListItem();
                item.Value = "SatisfaccionISR";
                item.Text = "Por ISR";
                ListItem item2 = new ListItem();
                item2.Value = "SatisfaccionBU";
                item2.Text = "Por Unidad";
                this.rdl_btn_filtro.Items.Add(item);
                this.rdl_btn_filtro.Items.Add(item2);
            }

            if (this.cmb_tipo_encuesta.SelectedItem.Value == "3")
            {
                this.rdl_btn_filtro.Items.Clear();
                ListItem item = new ListItem();
                item.Value = "Liderazgo";
                item.Text = "Por ISR";
                ListItem item2 = new ListItem();
                item2.Value = "Liderazgo";
                item2.Text = "Por Unidad";
                this.rdl_btn_filtro.Items.Add(item);
                this.rdl_btn_filtro.Items.Add(item2);
            }

            if (this.cmb_tipo_encuesta.SelectedItem.Value == "4")
            {
                this.rdl_btn_filtro.Items.Clear();
                ListItem item = new ListItem();
                item.Value = "Clima";
                item.Text = "Por ISR";
                ListItem item2 = new ListItem();
                item2.Value = "Clima";
                item2.Text = "Por Unidad";
                this.rdl_btn_filtro.Items.Add(item);
                this.rdl_btn_filtro.Items.Add(item2);
            }

            if (this.cmb_tipo_encuesta.SelectedItem.Value == "5")
            {
                this.rdl_btn_filtro.Items.Clear();
                ListItem item = new ListItem();
                item.Value = "Retiro";
                item.Text = "Por ISR";
                ListItem item2 = new ListItem();
                item2.Value = "Retiro";
                item2.Text = "Por Unidad";
                this.rdl_btn_filtro.Items.Add(item);
                this.rdl_btn_filtro.Items.Add(item2);
            }
            this.rdl_btn_filtro.SelectedIndex = 0;
            cargarGrillaPonderadoCabeza();
        }

        protected void rdl_btn_filtro_SelectedIndexChanged(object sender, EventArgs e)
        {
            String opc = this.rdl_btn_filtro.SelectedItem.Value;
            switch (opc) {
                case "ServicioOP":
                    this.pnl_encuesta.Visible = false;
                
                    cargarGrillaPonderadoCabeza();
                    break;
                case "ServicioBu":
                    this.pnl_encuesta.Visible = false;          
                    cargarGrillaPonderadoCabeza();
                    break;
                case "SatisfaccionISR":
                     this.pnl_encuesta.Visible = true;
                 
                    cargarGrillaPonderadoCabeza();
                    break;
                case "SatisfaccionBU":
                     this.pnl_encuesta.Visible = true;
                   
                    cargarGrillaPonderadoCabeza();
                    break;
                case "LiderazgoOP":
                    this.pnl_encuesta.Visible = true;

                    cargarGrillaPonderadoCabeza();
                    break;
                case "LiderazgoBU":
                    this.pnl_encuesta.Visible = true;

                    cargarGrillaPonderadoCabeza();
                    break;
                case "ClimaOP":
                    this.pnl_encuesta.Visible = true;

                    cargarGrillaPonderadoCabeza();
                    break;
                case "ClimaBU":
                    this.pnl_encuesta.Visible = true;

                    cargarGrillaPonderadoCabeza();
                    break;
                case "RetiroOP":
                    this.pnl_encuesta.Visible = true;

                    cargarGrillaPonderadoCabeza();
                    break;
                case "RetiroBU":
                    this.pnl_encuesta.Visible = true;

                    cargarGrillaPonderadoCabeza();
                    break;
            }
        }

        protected void cmb_isr_evaluado_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarGrillaPonderadoCabeza();
        }

        protected void img_btn_exportar_excel_Click(object sender, ImageClickEventArgs e)
        {
            GridView newDataGrid = new GridView();

            cargarrillaExcel(ref newDataGrid);
            exportarGridView_Excel(newDataGrid, "Listar Respuesta");
            
        }
        private void cargarrillaExcel(ref GridView prmDGrid)
        {
            String strSQL = "";
            if (this.rdl_btn_filtro.SelectedItem != null)
            {
                String opc = this.rdl_btn_filtro.SelectedItem.Value;
                switch (opc)
                {
                    case "ServicioOP":
                        strSQL = "EXEC [SP_MM_DAR_LISTA_RESPUESTA_TOTAL_PONDERACION] 1,1,0," + this.cmb_fase.SelectedItem.Value;
                        break;
                    case "ServicioBu":
                        strSQL = "EXEC [SP_MM_DAR_LISTA_RESPUESTA_TOTAL_PONDERACION] 1,2," + this.cmb_encuestas.SelectedItem.Value + "," +this.cmb_fase.SelectedItem.Value;
                        break;
                    case "SatisfaccionISR":
                        strSQL = "EXEC [SP_MM_DAR_LISTA_RESPUESTA_TOTAL_PONDERACION] 2,1," + this.cmb_encuestas.SelectedItem.Value + "," +this.cmb_fase.SelectedItem.Value;
                        break;
                    case "SatisfaccionBU":
                        strSQL = "EXEC [SP_MM_DAR_LISTA_RESPUESTA_TOTAL_PONDERACION] 2,2," + this.cmb_encuestas.SelectedItem.Value + "," + this.cmb_fase.SelectedItem.Value;
                        break;
                    case "Liderazgo":
                        strSQL = "EXEC [SP_MM_DAR_LISTA_RESPUESTA_TOTAL_PONDERACION] 3,1," + this.cmb_encuestas.SelectedItem.Value + "," + this.cmb_fase.SelectedItem.Value;
                        break;
                    case "LiderazgoBU":
                        strSQL = "EXEC [SP_MM_DAR_LISTA_RESPUESTA_TOTAL_PONDERACION] 3,2," + this.cmb_encuestas.SelectedItem.Value + "," + this.cmb_fase.SelectedItem.Value;
                        break;
                    case "ClimaOP":
                        strSQL = "EXEC [SP_MM_DAR_LISTA_RESPUESTA_TOTAL_PONDERACION] 4,1," + this.cmb_encuestas.SelectedItem.Value + "," + this.cmb_fase.SelectedItem.Value;
                        break;
                    case "ClimaBU":
                        strSQL = "EXEC [SP_MM_DAR_LISTA_RESPUESTA_TOTAL_PONDERACION] 4,2," + this.cmb_encuestas.SelectedItem.Value + "," + this.cmb_fase.SelectedItem.Value;
                        break;

                }


                Boolean returnError = false;
                DataSet ds = new DataSet();
                ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
                if (!returnError)
                {
                    ds.Tables[0].Columns.Remove("idTipoEncuesta");
                    ds.Tables[0].Columns.Remove("idEncuesta");
                    prmDGrid.DataSource = ds;
                    prmDGrid.DataBind();
                }
            }
        }

        protected void cmb_fase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmb_fase.SelectedItem.Value != "0")
            {
                cargarEncuestas();
            }
            else {
                this.pnl_detalle_respuestas.Visible = false;
                this.pnl_filtro.Visible = false;
                this.pnl_tipo_encuesta.Visible = false;
                this.pnl_encuesta.Visible = false;
            }
        }

        protected void dgrid_ponderado_total_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.dgrid_ponderado_total.PageIndex = e.NewPageIndex;
            cargarGrillaPonderadoCabeza();
        }
    }
}