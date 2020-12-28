using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SURVEYTOOLSHP.Model;
using System.Reflection;
using System.Resources;

namespace SURVEYTOOLSHP
{
    public partial class Default : BasePage
    {
        ResourceManager mg = new ResourceManager("Resources.SURVEYTOOLSHP", Assembly.Load("App_GlobalResources"));
        String idActualUsuario = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {
                if (vfEncuestas())
                {
                    cargarNombre();
                    cargarFases();
                    cargarCombos();
                }
                else {
                    this.pnl_encuestas_no_pendientes.Visible = true;
                    this.pnl_encuestas_pendientes.Visible = false;
                }
            }
           

        }
        private void cargarNombre() {
            this.Label1.Text = HttpContext.Current.Session["USER"].ToString();
        }
        private void cargarFases() {
            String strSQL = "SELECT fas_id AS 'ID',fas_nombre AS 'NOMBRE' FROM T_FASE";
            Boolean returnError = false;
            clsSQL.llenarComboBox(ref returnError, strSQL, ref this.cmb_fase, "ID", "NOMBRE");
            strSQL = "SELECT fas_id AS 'ID' FROM T_FASE WHERE '" +  DateTime.Today.ToString("yyyy/MM/dd") +"' BETWEEN fas_fechaComienzo AND fas_fechaFinalizacion";
            String idFase = (clsSQL.retornarDatoEscalar(ref returnError, strSQL).ToString() !=null) ? clsSQL.retornarDatoEscalar(ref returnError, strSQL).ToString() : "1";
            this.cmb_fase.Items.FindByValue(idFase).Selected = true ;
            
        }


        private void cargarCombos() {
 
            String strSQL = "SELECT DISTINCT "+ 
                            "A.tip_id AS 'ID', "+
                            "A.tip_nombre AS 'NOMBRE' "+
                            "FROM T_TIPO_ENCUESTA A "+
                            "INNER JOIN T_ENCUESTA_USUARIO E ON E.uen_tip_id  = A.tip_id "+
                            "INNER JOIN T_UNIDAD_ENCUESTA B ON B.uni_uen_id = E.uen_id "+
                            "LEFT JOIN T_ISR ISR ON  B.uni_id =ISR.isr_uni_id "+
                            "WHERE A.tip_estado = 1 AND  B.uni_estado = 1 AND E.uen_estado = 1 AND E.uen_usu_id = " + HttpContext.Current.Session["ID_USER"].ToString() + " AND B.uni_estado_encuesta=1 AND ((E.uen_tip_id = 3) OR (E.uen_tip_id = 4))  AND E.uen_fas_id = " + this.cmb_fase.SelectedItem.Value;
                            //"WHERE A.tip_estado = 1 AND  B.uni_estado = 1 AND E.uen_estado = 1 AND E.uen_usu_id = " + HttpContext.Current.Session["ID_USER"].ToString() + " AND B.uni_estado_encuesta=1 AND ((ISR.isr_id IS NOT NULL AND ISR.isr_est_encuesta = 1 AND E.uen_tip_id = 2) OR (E.uen_tip_id = 1) OR (E.uen_tip_id = 3) OR (E.uen_tip_id = 4))  AND E.uen_fas_id = " + this.cmb_fase.SelectedItem.Value;
            Boolean returnError = false;

           
            clsSQL.llenarComboBox(ref returnError, strSQL, ref this.cmb_tipo_encuesta, "ID", "NOMBRE");

            if (this.cmb_tipo_encuesta.Items.Count > 0)
            {
                this.cmb_tipo_encuesta.SelectedIndex = 0;                 
                cargarComboUnidades();
                   
              
            }
            else {
                cargarPanelNoTieneEncuestasPendientes();
         
            }
        }
        
        private void cargarPanelNoTieneEncuestasPendientes() {
            this.pnl_encuestas_tipos.Visible = false;
            this.pnl_fase_editada.Visible = true;
        }
        

        private void cargarComboUnidades() {
            Boolean returnError = false;
            String strSQL = "EXEC SP_MM_CARGAR_ENCUESTAS " + HttpContext.Current.Session["ID_USER"].ToString() +"," + this.cmb_tipo_encuesta.SelectedItem.Value  + "," + this.cmb_fase.SelectedItem.Value;
            clsSQL.llenarComboBox(ref returnError, strSQL, ref cmb_unidad_Negocio, "ID", "NOMBRE");
            if (cmb_unidad_Negocio.Items.Count > 0)
            {
                this.pnl_fase_editada.Visible = false;
                this.pnl_encuestas_no_pendientes.Visible = false;
                this.pnl_encuestas_tipos.Visible = true;
                

            }
            else
            {
                this.pnl_fase_editada.Visible = true;
                this.pnl_encuestas_no_pendientes.Visible = false;
                this.pnl_encuestas_tipos.Visible = false;
                
            
            }
         
        }

        private Boolean vfEncuestas() {
            String strSQL = "SELECT DISTINCT " +
                              "A.tip_id AS 'ID', " +
                              "A.tip_nombre AS 'NOMBRE' " +
                              "FROM T_TIPO_ENCUESTA A " +
                              "INNER JOIN T_ENCUESTA_USUARIO E ON E.uen_tip_id  = A.tip_id " +
                              "INNER JOIN T_UNIDAD_ENCUESTA B ON B.uni_uen_id = E.uen_id " +
                              "LEFT JOIN T_ISR ISR ON  B.uni_id =ISR.isr_uni_id " +
                              "WHERE A.tip_estado = 1 AND  B.uni_estado = 1 AND E.uen_estado = 1 AND E.uen_usu_id = " + HttpContext.Current.Session["ID_USER"].ToString() + " AND B.uni_estado_encuesta=1 AND ((ISR.isr_id IS NOT NULL AND ISR.isr_est_encuesta = 1 AND E.uen_tip_id = 2) OR (E.uen_tip_id = 1) OR (ISR.isr_id IS NOT NULL AND ISR.isr_est_encuesta = 1 AND E.uen_tip_id = 3)  OR (E.uen_tip_id = 4))";
            Boolean returnError = false;
            if (clsSQL.retornarDatoEscalar(ref returnError, strSQL) != null)
            {
                return true;
            }
            else {
                return false;
            }

        }
        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            String url="";
            DataSet ds = new DataSet();
            Boolean returnError = false;
            String strSQL = "SELECT enc_url AS 'URL' FROM T_ENCUESTA WHERE enc_id = " + this.cmb_unidad_Negocio.SelectedItem.Value;
            ds=clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError) {
                if (ds.Tables.Count > 0) {
                    foreach (DataRow dRow in ds.Tables[0].Rows) {
                        url = dRow["URL"].ToString()+"&&idFase="+this.cmb_fase.SelectedItem.Value;
                    }
                }
            }
            Response.Redirect(url);
                
        }

        protected void cmb_tipo_encuesta_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarComboUnidades();
        }
        private void cargarEncuestaRespondidas() {
            String strSQL = "EXEC SP_MM_DAR_ENCUESTAS_SIN_RESPONDER " + HttpContext.Current.Session["ID_USER"].ToString();
            Boolean returnError = false;
            DataSet ds = new DataSet();
            ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError) {
                if (ds.Tables.Count > 0) {
                    this.dgrid_misEncuestas.DataSource = ds;
                    this.dgrid_misEncuestas.DataBind();
                }
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            this.pnl_EncuestasXResponder.Visible = true;
            cargarGrillaEncuesta();
        }
        private void cargarGrillaEncuesta() {
            String strSQL = "EXEC SP_MM_DAR_ENCUESTAS_X_RESPONDER " + HttpContext.Current.Session["ID_USER"].ToString();
            Boolean returnError = false;
            DataSet ds = new DataSet();
            ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError) {
                if (ds.Tables.Count > 0) {
                    this.dgrid_misEncuestas.DataSource = ds;
                    this.dgrid_misEncuestas.DataBind();
                
                }
            }
        }

        protected void dgrid_misEncuestas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count >= 6) {

                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;
                if (e.Row.Cells[4].Text == "Sin responder")
                {
                    e.Row.Cells[4].Text = "";
                    Image img = new Image();
                    img.ToolTip = "Por responder";
                    img.ImageUrl = "~/Resources/Iconos/Edit.png";
                    img.Width = 40;
                    img.Height = 40;
                    e.Row.Cells[4].Controls.Add(img);
                    e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[4].Width = 150;
                    e.Row.Cells[0].Enabled = true;



                }

                else if (e.Row.Cells[4].Text == "Respuesta")
                {
                     e.Row.Cells[0].Controls.Clear();
                        e.Row.Cells[0].Text = "Ya se respondío";
                        e.Row.Cells[4].Text = "";
                        Image img = new Image();
                        img.ToolTip = "Respondida";
                        img.ImageUrl = "~/Resources/Iconos/Forward.png";
                        img.Width = 40;
                        img.Height = 40;
                        e.Row.Cells[4].Controls.Add(img);
                        e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[4].Width = 150;
                        e.Row.Cells[0].Enabled = false;
                    
                }
               
            }
            
        }

        protected void dgrid_misEncuestas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch(e.CommandName) { 
                case "Responder":
                    irEncuestaSinResponder(Convert.ToInt32(e.CommandArgument));
                    break;
            }
        }
        private void irEncuestaSinResponder(int numeroFila) {
            String url = this.dgrid_misEncuestas.Rows[numeroFila].Cells[5].Text + "&&idFase=" + this.dgrid_misEncuestas.Rows[numeroFila].Cells[6].Text;
            Response.Redirect(url);
        
        }

        protected void cmb_fase_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarCombos();
        }
    }
}