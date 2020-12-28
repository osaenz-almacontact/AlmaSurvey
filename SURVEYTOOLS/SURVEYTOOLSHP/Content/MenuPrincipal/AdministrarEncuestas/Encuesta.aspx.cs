using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SURVEYTOOLSHP.Model;
namespace SURVEYTOOLSHP.Content.MenuPrincipal.AdministrarEncuesta
{
    public partial class Satisfaccion : BasePage
    {
        private String numEncuesta = "";
        private Boolean crearPregunta = false;
    String strMensaje = "";
   
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {
             
                HttpContext.Current.Session["ID_PREGUNTA"] = String.Empty;
            }
            numEncuesta = Request.QueryString["idEncuesta"]; 
            cargarPreguntas();
            cargarNombreEncuesta();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session["ID_PREGUNTA"] = "0";
            this.lbl_titulo_editar_pregunta.Text = "Crear pregunta";
            OpcionesNuevaPregunta();
        }
        private void OpcionesNuevaPregunta() {
            Boolean returnError = false;
            String strSQL = "SELECT tip_id AS 'ID', tip_nombre AS 'NOMBRE' FROM T_BAS_TIPOPREGUNTA WHERE tip_estado=1";
         

                txt_darRespuesta.Text = String.Empty;
                txt_pregunta.Text = String.Empty;
                clsSQL.llenarComboBox(ref returnError, strSQL, ref this.cmb_tipo_pregunta, "ID", "NOMBRE");


                this.pnl_VerCaracterisitacas_Preguntas.Visible = true;
         
        
        }

        protected void dgrid_Preguntas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            HttpContext.Current.Session["NUMEROFILA"] = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar") {
                Editar();
                this.lbl_titulo_editar_pregunta.Text = "Editar pregunta";
            }
            else if (e.CommandName == "Deshabilitar")
            {
                habilitarPregunta(false);
                cargarPreguntas();
            }
            else {
                habilitarPregunta(true);
                cargarPreguntas();
            }
        }
        private void Editar() {
            Boolean returnError = false;
            String strSQL = "SELECT pre_id AS 'ID', pre_nombre AS 'NUMERO PREGUNTA' , pre_descripcion AS 'NOMBRE',pre_tip_id AS 'TIPO PREGUNTA' FROM T_PREGUNTA WHERE pre_id = " + this.dgrid_Preguntas.Rows[Convert.ToInt32(HttpContext.Current.Session["NUMEROFILA"].ToString())].Cells[3].Text;
            DataSet ds = new DataSet(),dscmbTipoPreguntas=new DataSet(),dsRespuestas=new DataSet();
            ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError)
            {
                if (ds.Tables[0].Rows.Count == 1)
                {
                    foreach (DataRow dRow in ds.Tables[0].Rows)
                    {
                        this.txt_pregunta.Text=dRow["NOMBRE"].ToString();
                        cargarTipoPreguntas(dRow["TIPO PREGUNTA"].ToString());
                        cargarRespuesta(dRow["ID"].ToString());
                        HttpContext.Current.Session["ID_PREGUNTA"] = dRow["ID"];
                    
                    }

                    this.pnl_VerCaracterisitacas_Preguntas.Visible=true;
                }
            }
            
        
        }
        private void cargarTipoPreguntas(String prmIDTipoPregunta) {
            String actualTipo = "";
            Boolean returnError = false;
            String strSQL = "SELECT tip_id AS 'ID', tip_nombre AS 'NOMBRE' FROM T_BAS_TIPOPREGUNTA";
            clsSQL.llenarComboBox(ref returnError, strSQL, ref this.cmb_tipo_pregunta, "ID", "NOMBRE");
           DataSet ds= new DataSet();
           strSQL = "SELECT pre_tip_id AS 'ID' FROM T_PREGUNTA WHERE pre_id = "  + prmIDTipoPregunta;
            ds = clsSQL.ejecutarProceConsulSQL(strSQL,ref returnError);
            if (!returnError) {
                if (ds.Tables[0].Rows.Count ==1) {
                    foreach (DataRow dRow in ds.Tables[0].Rows) {
                        actualTipo = dRow["ID"].ToString();
                    }
                    this.cmb_tipo_pregunta.Items.FindByValue(actualTipo).Selected=true;
                }
            }
           
        }
        private void cargarRespuesta(String prmIDPregunta) {
            Boolean returnError = false;
            String respuestas = "";
            String strSQL = "SELECT res_id AS 'ID', res_nombre AS 'NOMBRE' FROM T_RESPUESTA WHERE res_pre_id =" + this.dgrid_Preguntas.Rows[Convert.ToInt32(HttpContext.Current.Session["NUMEROFILA"].ToString())].Cells[3].Text + " AND res_estado = 1";
            DataSet ds = new DataSet();
            ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError) {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dRow in ds.Tables[0].Rows)
                    {
                        respuestas = respuestas + dRow["NOMBRE"].ToString()+",";
                    }
                    this.txt_darRespuesta.Text = respuestas;
                    this.pnl_red_cmb.Visible = true;

                }
                else {
                    this.pnl_red_cmb.Visible = false;
                }
            }
        }
        private void habilitarPregunta(Boolean prmhabilitar) {
            Boolean returnError=false;
            String strSQL = "";
            if (prmhabilitar)
            
                strSQL = "UPDATE T_PREGUNTA SET pre_estado = 1 WHERE pre_id = " + this.dgrid_Preguntas.Rows[Convert.ToInt32(HttpContext.Current.Session["NUMEROFILA"].ToString())].Cells[3].Text;
            
            else 
                strSQL = "UPDATE T_PREGUNTA SET pre_estado = 0 WHERE pre_id = " + this.dgrid_Preguntas.Rows[Convert.ToInt32(HttpContext.Current.Session["NUMEROFILA"].ToString())].Cells[3].Text;
            clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError) {
                cargarPreguntas();
            }
        }
        private void cargarNombreEncuesta()
        {
            String strSQL = "SELECT enc_nombre FROM T_ENCUESTA WHERE enc_id = " + numEncuesta;
            Boolean returnError = false;
            String nomEncuesta = clsSQL.retornarDatoEscalar(ref returnError, strSQL).ToString();
            if (!returnError)
                this.lbl_nombre_encuesta.Text = nomEncuesta;

        }
        private void cargarPreguntas() {
           
            Boolean return_Error = false;
            String strSQL = "SELECT P.pre_id AS 'ID', P.pre_descripcion AS 'DESCRIPCION',T.tip_nombre AS 'TIPO DE PREGUNTA',(CASE WHEN P.pre_estado = 1 THEN 'ACTIVO' ELSE 'DESACTIVO' END) AS 'ESTADO' FROM T_PREGUNTA  P INNER JOIN T_BAS_TIPOPREGUNTA T ON P.pre_tip_id= T.tip_id WHERE P.pre_enc_id = "+ numEncuesta;
            DataSet ds = new DataSet();
            ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref return_Error);
            if (!return_Error) {
                if (ds.Tables.Count>0) {
                    this.dgrid_Preguntas.DataSource = ds;
                    this.dgrid_Preguntas.DataBind();
                }
            }
        }

        protected void btnEnviarPregunta_Click(object sender, EventArgs e)
        {
            if (verificarDatos())
            {
                String mensajeValidacion="";
                Boolean returError = false;
                String strSQL = "";
                String idPagin = HttpContext.Current.Session["ID_PREGUNTA"].ToString();
                if (idPagin == "0")
                {
                    if (this.cmb_tipo_pregunta.SelectedItem.Value == "2" || this.cmb_tipo_pregunta.SelectedItem.Value == "3")
                    {
                        if (this.txt_darRespuesta.Text.Substring(this.txt_darRespuesta.Text.Length - 1, 1) == ",")
                        {
                            strSQL = "EXEC SP_GENERAR_PREGUNTA 0," + numEncuesta + ",'" + this.txt_pregunta.Text + "'," + this.cmb_tipo_pregunta.SelectedValue.ToString() + ",'" + this.txt_darRespuesta.Text + "',0";
                            mensajeValidacion = "Se creo la pregunta";
                        }
                        else {
                            returError = true;    
                            mensajeValidacion = "Falto digitar una coma (,)\nSugerencia " + this.txt_darRespuesta.Text + ",";
                        }
                    }
                    else
                    {
                        strSQL = "EXEC SP_GENERAR_PREGUNTA 0," + numEncuesta + ",'" + this.txt_pregunta.Text + "'," + this.cmb_tipo_pregunta.SelectedValue.ToString() + ",'" + this.txt_darRespuesta.Text + "',0";
                        mensajeValidacion = "Se creo la pregunta";
                    }
                }
                else
                {
                    if (this.cmb_tipo_pregunta.SelectedItem.Value == "2" || this.cmb_tipo_pregunta.SelectedItem.Value == "3")
                    {
                        if (this.txt_darRespuesta.Text.Substring(this.txt_darRespuesta.Text.Length - 1, 1) == ",")
                        {
                            strSQL = "EXEC SP_GENERAR_PREGUNTA 1," + numEncuesta + ",'" + this.txt_pregunta.Text + "'," + this.cmb_tipo_pregunta.SelectedValue.ToString() + ",'" + this.txt_darRespuesta.Text + "'," + idPagin;
                            mensajeValidacion = "Se edito la pregunta";
                        }
                        else {
                            returError = true;
                            mensajeValidacion = "Falto digitar una coma (,)\nSugerencia " + this.txt_darRespuesta.Text + ",";
                        }
                    }
                    else
                    {

                        strSQL = "EXEC SP_GENERAR_PREGUNTA 1," + numEncuesta + ",'" + this.txt_pregunta.Text + "'," + this.cmb_tipo_pregunta.SelectedValue.ToString() + ",'" + this.txt_darRespuesta.Text + "'," + idPagin;
                        mensajeValidacion = "Se edito la pregunta";
                    }
                }
                if (!returError)
                {
                    clsSQL.ejecutarProceConsulSQL(strSQL, ref returError);
                    cargarPreguntas();

                    Message("EN LA ENCUESTA", mensajeValidacion, TipoMensaje.Validacion, true);
                }
                else {
                    this.lbl_Respuestas.ForeColor = System.Drawing.Color.FromName("#701212");
                    Message("EN LA ENCUESTA", mensajeValidacion, TipoMensaje.Warning, true);
                }
            }
            else {
                this.lbl_Respuestas.ForeColor = System.Drawing.Color.Black;
                Message("ERROR SURVEYTOOLSHP", strMensaje, TipoMensaje.Warning, true);
            }
        }

        protected void dgrid_Preguntas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count >= 7) {
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;

                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
            

            }
        }

        protected void cmb_tipo_pregunta_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarRespuesta();
          
        }

        private void cargarRespuesta() {
            if (this.cmb_tipo_pregunta.SelectedItem.Value.ToString() == "2" || this.cmb_tipo_pregunta.SelectedItem.Value.ToString() == "3")
            {
                this.pnl_red_cmb.Visible = true;

                this.lbl_pregunta.Text = "Pregunta";
                pnl_pregunta.Visible = true;

            }
            else
            {
                if (HttpContext.Current.Session["ID_PREGUNTA"].ToString() == String.Empty)
                {
                    this.txt_darRespuesta.Text = "";

                }
                if (this.cmb_tipo_pregunta.SelectedItem.Value.ToString() == "5")
                {
                    this.lbl_pregunta.Text = "Encabezado";
                }
                else
                {
                    this.lbl_pregunta.Text = "Pregunta";
                }
                this.pnl_red_cmb.Visible = false;


            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.pnl_red_cmb.Visible = false;
            this.pnl_VerCaracterisitacas_Preguntas.Visible = false;
        }
        private Boolean verificarDatos() {
            if (this.txt_darRespuesta.Text.Length == 0 && this.txt_pregunta.Text.Length == 0 && this.cmb_tipo_pregunta.SelectedItem ==null ) {
           
                if (this.cmb_tipo_pregunta.SelectedItem == null) {
                    strMensaje = strMensaje + "Falta elegir tipo de pregunta \n";
                }
                if (this.txt_pregunta.Text.Length == 0) {
                    strMensaje = strMensaje + "Falta ingresar la pregunta \n";
                
                }
                if((this.cmb_tipo_pregunta.SelectedItem.Value.ToString()=="2" || this.cmb_tipo_pregunta.SelectedItem.Value.ToString()=="2")&& this.txt_darRespuesta.Text.Length==0){
                    strMensaje = strMensaje + "Fata digitar el listado de respuestas";
                }
                return false;
            }
            return true;
        
        }
       

        protected void dgrid_Preguntas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
          
            this.dgrid_Preguntas.PageIndex = e.NewPageIndex;
            cargarPreguntas();
        }

        protected void dgrid_Preguntas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void dgrid_Preguntas_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }


    
    }
}