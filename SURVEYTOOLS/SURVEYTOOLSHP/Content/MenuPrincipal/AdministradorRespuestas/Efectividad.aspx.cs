using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SURVEYTOOLSHP.Model;
using System.Data;
namespace SURVEYTOOLSHP.Content.MenuPrincipal.AdministradorRespuestas
{
    public partial class Eficiencia : BasePage
    {
        private clsEmail email;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {
                cargarFase();
                cargarTipoEncuestas();
   
            }
            
          
            this.email = new clsEmail();
        }
        private void cargarFase() {
            String strSQL = "SELECT fas_id AS 'ID',fas_nombre AS 'NOMBRE' FROM T_FASE ";
            Boolean returnError = false;
            clsSQL.llenarComboBoxConSelect(ref returnError, strSQL, ref this.cmb_fase, "ID", "NOMBRE");
        }
        private void cargarTipoEncuestas() {
            if (this.cmb_fase.SelectedItem.Value != "0")
            {
                Boolean returnError = false;
                String strSQL = "SELECT DISTINCT " +
                                "TIPO.tip_id AS 'ID', " +
                                "TIPO.tip_nombre AS 'NOMBRE' " +
                                "FROM T_ENCUESTA_USUARIO USUARIO " +
                                "INNER JOIN T_TIPO_ENCUESTA TIPO ON TIPO.tip_id = USUARIO.uen_tip_id " +
                                "WHERE USUARIO.uen_estado = 1 AND TIPO.tip_estado = 1 AND USUARIO.uen_fas_id = " + this.cmb_fase.SelectedItem.Value;
                clsSQL.llenarComboBox(ref returnError, strSQL, ref this.cmb_tipo_encuesta, "ID", "NOMBRE");

                if (!returnError)
                {
                    if (this.cmb_tipo_encuesta.Items.Count > 0)
                    {
                        cargarEncuesta();
                        cargarOpcionesRadioButton();
                        this.pnl_view_noAsignado.Visible = false;
                        this.pnl_con_data.Visible = true;
                    }
                    else {
                        this.pnl_view_noAsignado.Visible = true;
                        this.pnl_con_data.Visible = false;
                    }
                }
            }
            else {
                this.pnl_con_data.Visible = false;
            }

        }
        private void cargarEncuesta() {
            Boolean returnError = false;
             String strSQL="";
            if (this.cmb_tipo_encuesta.SelectedItem.Value == "1")
            {
                strSQL = "SELECT DISTINCT " +
                                "ENCUESTA.enc_id AS 'ID'," +
                                "ENCUESTA.enc_nombre AS 'NOMBRE' " +
                                "FROM T_UNIDAD_ENCUESTA UNIDAD " +
                                "INNER JOIN T_ENCUESTA_USUARIO USUARIO ON USUARIO.uen_id = UNIDAD.uni_uen_id " +
                                "INNER JOIN T_ENCUESTA ENCUESTA ON ENCUESTA.enc_id = UNIDAD.uni_enc_id " +
                                "WHERE USUARIO.uen_estado = 1 AND ENCUESTA.enc_estado= 1 AND UNIDAD.uni_estado = 1 AND USUARIO.uen_tip_id = 1 AND USUARIO.uen_fas_id = " + this.cmb_fase.SelectedItem.Value ;
            }
            else if (this.cmb_tipo_encuesta.SelectedItem.Value == "2") { 
            strSQL="SELECT DISTINCT "+
                    "ENCUESTA.enc_id AS 'ID', "+
                    "ENCUESTA.enc_nombre AS 'NOMBRE' "+ 
                    "FROM T_UNIDAD_ENCUESTA UNIDAD " +  
                    "INNER JOIN T_ISR ISR ON ISR.isr_uni_id = UNIDAD.uni_id " +
                    "INNER JOIN T_ENCUESTA_USUARIO USUARIO ON USUARIO.uen_id = UNIDAD.uni_uen_id " +
                    "INNER JOIN T_ENCUESTA ENCUESTA ON ENCUESTA.enc_id = UNIDAD.uni_enc_id "+
                    "WHERE USUARIO.uen_estado = 1 AND ENCUESTA.enc_estado= 1 AND UNIDAD.uni_estado = 1 AND USUARIO.uen_tip_id = 2 AND USUARIO.uen_fas_id = " + this.cmb_fase.SelectedItem.Value;
            }
            clsSQL.llenarComboBox(ref returnError, strSQL, ref this.cmb_encuesta, "ID", "NOMBRE");
            if(!returnError){
                if (this.cmb_encuesta.Items.Count > 0)
                {
                    cargarISR();
                }
                else {
                    this.pnl_isr_vista.Visible = false;
                }
         
            }
           
        }
        private void cargarISR() {
            Boolean returnError = false;
            String opc = this.cmb_tipo_encuesta.SelectedItem.Value;
            switch (opc) { 
                case "1":
                    this.pnl_isr_vista.Visible = false;
                 
                    break;
                case "2":
                    String strSQL = "SELECT DISTINCT " + 
                                    "ISR.isr_nombre AS 'NOMBRE' "+
                                    "FROM T_ISR ISR "+
                                    "INNER JOIN T_UNIDAD_ENCUESTA UNIDAD ON UNIDAD.uni_id= ISR.isr_uni_id " +
                                    "INNER JOIN T_ENCUESTA_USUARIO USUARIO ON USUARIO.uen_id = UNIDAD.uni_uen_id " +
                                    "WHERE USUARIO.uen_estado = 1 AND ISR.isr_estado = 1 AND UNIDAD.uni_estado=1 AND UNIDAD.uni_enc_id = " + this.cmb_encuesta.SelectedItem.Value + " AND USUARIO.uen_fas_id = " + this.cmb_fase.SelectedItem.Value;
                    clsSQL.llenarComboBox(ref returnError, strSQL, ref this.cmb_isr, "NOMBRE", "NOMBRE");
                    if (this.cmb_isr.Items.Count > 0)
                    {
                       
                        pnl_view_noAsignado.Visible = false;
                    }
                    else {
                        pnl_view_noAsignado.Visible = true;
                    }
                    break;
            }
        
        }

        //Estructura de correo de envio a usuarios que NO han llenado las encustas.
        private String retornarCuerpoCorreo(String prmNombreUsuario, String prmEncuesta, String prmIsr, String prmTipoEncuesta, String prmLogin, String prmContrasenia, String prmEnvioPrimeraVez)
        {
            String cuerpo = "";
            String mensajeFinal = "";
            //En el caso de que el mensaje sera enviado por primera vez entonces entra en el if de lo contrario se le recordara a la persona que debe responder dicha encuesta entrando al else
            if (prmEnvioPrimeraVez == "1")
            {
                //Mensaje editado
                cuerpo = "<div style='padding:15px;'>" +
                         "<p style='margin:10px 0px 10px 10px;font-family:HP Simplified;font-size:14px'><strong>Usuario: </strong> " + prmNombreUsuario + "</p>" +
                         "<p style='margin:0px 0px 20px 10px;font-family:HP Simplified;font-size:14px;color:#585964'><strong>Para Almacontact </strong> saber su opinión es una prioridad con el fin de tener una percepción de su relación comercial y/o el servicio prestado, de esta manera retroalimentar la gestión interna. Le agradecemos su objetividad y disposición para contestar las siguientes encuestas (siga este <a href='http://bog01nmcahp/NMCASCC/SURVEYTOOLSHP/'>link</a>) </p>" +
                         "<p style='margin:0px 0px 20px 10px;font-family:HP Simplified;font-size:14px;color:#585964'>Si quiere conocer más acerca de este procedimiento lo invitamos a visitar el siguiente <a href='https://sway.com/Q1l1UdR_TpZuTtMx'>link</a></p>" +
                         "<p style='margin:0px 0px 20px 10px;font-family:HP Simplified;font-size:14px;color:#585964'>Si presenta algún problema al ingresar a la encuesta pude ingresar en el siguiente <a href='https://sway.com/3UB1GZdF8Uc8Xwz2'>link</a></p>" +
                         "<p style='margin:0px 0px 10px 50px;font-size:16px;color:#214023'><strong>Recuerde sus datos:</strong></p>" +
                         "<p style='margin:0px 0px 10px 50px;font-size:16px;color:#214023'><strong>Usuario : </strong><a style='color:#585964'><strong>" + prmLogin + "</strong></a></p>" +
                         "<p style='margin:0px 0px 10px 50px;font-size:16px;color:#214023'><strong>Contraseña : </strong><a style='color:#585964'><strong> " + prmContrasenia + "</strong></a></p></div>";


                mensajeFinal = crearRetornarCuerpoCorreo("Mensaje cambiado", cuerpo, "Almacontact", "");
            }
            else if (prmEnvioPrimeraVez == "2")
            {
                //Mensaje editado
                cuerpo = "<div style='padding:15px;'>" +
                         "<p style='margin:10px 0px 10px 10px;font-family:HP Simplified;font-size:14px'><strong>Usuario: </strong> " + prmNombreUsuario + "</p>" +
                         "<p style='margin:0px 0px 20px 10px;font-family:HP Simplified;font-size:14px;color:#585964'><strong>Para Almacontact </strong> saber su opinión es una prioridad con el fin de tener una percepción de su relación comercial y/o el servicio prestado, de esta manera retroalimentar la gestión interna. Le agradecemos su objetividad y disposición para contestar las siguientes encuestas (siga este <a href='http://bog01nmcahp/NMCASCC/SURVEYTOOLSHP/'>link</a>) </p>" +
                         "<p style='margin:0px 0px 20px 10px;font-family:HP Simplified;font-size:14px;color:#585964'>Si quiere conocer más acerca de este procedimiento lo invitamos a visitar el siguiente <a href='https://sway.com/Q1l1UdR_TpZuTtMx'>link</a></p>" +
                         "<p style='margin:0px 0px 20px 10px;font-family:HP Simplified;font-size:14px;color:#585964'>Si presenta algún problema al ingresar a la encuesta pude ingresar en el siguiente <a href='https://sway.com/3UB1GZdF8Uc8Xwz2'>link</a></p>" +
                         "<p style='margin:0px 0px 10px 50px;font-size:16px;color:#214023'><strong>Recuerde sus datos:</strong></p>" +
                         "<p style='margin:0px 0px 10px 50px;font-size:16px;color:#214023'><strong>Usuario: </strong><a style='color:#585964'><strong>" + prmLogin + "</strong></a></p>" +
                         "<p style='margin:0px 0px 10px 50px;font-size:16px;color:#214023'><strong>Contraseña: </strong><a style='color:#585964'><strong>" + prmContrasenia + "</strong></a></p></div>";


                mensajeFinal = crearRetornarCuerpoCorreo("RECORDATORIO DE ENVIÓ DE RESPUESTA", cuerpo, "TEST", "");

            }


            return mensajeFinal;
        }

        private void cargarGrilla() {

            Boolean returnError = false;
            String strSQL = "";
            String[] strSQLs = new String[2];
            if (this.rdl_btn_filtro.Items.Count > 0)
            {
                String opcion="";
                String evaluado = "";
                if(rdl_btn_filtro.SelectedItem!=null)
                opcion = this.rdl_btn_filtro.SelectedItem.Value;
                switch (opcion)
                {
                    case "ServicioBu":
                        strSQL = "EXEC [SP_MM_DAR_EFECTIVIDAD] 2,1," + this.cmb_encuesta.SelectedItem.Value + ",'',"+this.cmb_fase.SelectedItem.Value;
                        strSQLs[0] = strSQL;
                        strSQL = "EXEC [SP_MM_DAR_EFECTIVIDAD] 2,2," + this.cmb_encuesta.SelectedItem.Value + ",'',"+this.cmb_fase.SelectedItem.Value;
                        strSQLs[1] = strSQL;
                        evaluado = cmb_encuesta.SelectedItem.Text;
                        break;
                    case "ServicioOP":
                        strSQL = "EXEC [SP_MM_DAR_EFECTIVIDAD] 1,1,0,'',"+this.cmb_fase.SelectedItem.Value;
                        strSQLs[0] = strSQL;
                        strSQL = "EXEC [SP_MM_DAR_EFECTIVIDAD] 1,2,0,'',"+this.cmb_fase.SelectedItem.Value;
                        strSQLs[1] = strSQL;
                        evaluado = "Operational Center";

                        break;
                    case "SatisfaccionISR":
                        
                        strSQL = "EXEC [SP_MM_DAR_EFECTIVIDAD] 3,1," + this.cmb_encuesta.SelectedItem.Value + ",'" + this.cmb_isr.SelectedItem.Text + "',"+this.cmb_fase.SelectedItem.Value;
                        strSQLs[0] = strSQL;
                        strSQL = "EXEC [SP_MM_DAR_EFECTIVIDAD] 3,2," + this.cmb_encuesta.SelectedItem.Value + ",'" + this.cmb_isr.SelectedItem.Text + "',"+this.cmb_fase.SelectedItem.Value;
                        strSQLs[1] = strSQL;
                        evaluado = this.cmb_isr.SelectedItem.Text;
                        break;
                    case "SatisfaccionBU":
                        strSQL = "EXEC [SP_MM_DAR_EFECTIVIDAD] 4,1," + this.cmb_encuesta.SelectedItem.Value + ",'',"+this.cmb_fase.SelectedItem.Value;
                        strSQLs[0] = strSQL;
                        strSQL = "EXEC [SP_MM_DAR_EFECTIVIDAD] 4,2," + this.cmb_encuesta.SelectedItem.Value + ",'',"+ this.cmb_fase.SelectedItem.Value;
                        strSQLs[1] = strSQL;
                        evaluado = this.cmb_encuesta.SelectedItem.Text;
                        break;
                }

                DataSet ds = new DataSet();
                ds = clsSQL.ejecutarProceConsulSQL(strSQLs[0], ref returnError);
                if (!returnError)
                {
                    if (ds.Tables.Count > 0)
                    {
                        HttpContext.Current.Session["USUARIOS_X_RESPONDER"] = ds;
                        this.dgrid_eficiencia.DataSource = ds;
                        this.dgrid_eficiencia.DataBind();
                    }
                }
                String ponderado = "";
                ponderado = clsSQL.retornarDatoEscalar(ref returnError, strSQLs[1]).ToString();
                if (!returnError)
                {

                    if (ponderado.Length > 0)
                    {
                        this.lbl_pondera_respondidos.Text = ponderado;
                        this.lbl_nom_encuesta.Text = cmb_tipo_encuesta.SelectedItem.Text;
                        this.lbl_nom_evaluado.Text = evaluado;
                        this.pnl_ponderacion.Visible = true;
                    }
                    this.pnl_envio_correo.Visible = true;
                }

            }
            

        }

        private void cargarOpcionesRadioButton()
        {
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
            this.rdl_btn_filtro.SelectedIndex = 0;
            this.pnl_encueta.Visible = true;
            cargarGrilla();
        }

        private void enviarCorreo()
        {
            DataSet ds = (DataSet)HttpContext.Current.Session["USUARIOS_X_RESPONDER"];
            Boolean returnError = false;
         
            if (ds.Tables.Count > 0)
            {
                DataSet newDataSet = new DataSet();
                DataTable table = new DataTable();
                
                table.Columns.Add("LOGIN", typeof(String));
                table.Columns.Add("ID_USUARIO", typeof(String));                
                table.Columns.Add("Email", typeof(String));
                table.Columns.Add("Encuesta", typeof(String));
                table.Columns.Add("ID_TIPO_ENCUESTA", typeof(String));
                table.Columns.Add("CONTRASEÑA", typeof(String));
                table.Columns.Add("ID_EstadoEncuesta", typeof(String));
                table.Columns.Add("Evaluado", typeof(String));
                table.Columns.Add("Usuario", typeof(String));
                


                foreach (DataRow dRow in ds.Tables[0].Rows)
                {
                   
                    if (dRow["Estado de encuesta"].ToString() == "Sin responder")
                    {


                        if (verificarCorreoRepitente(dRow["LOGIN"].ToString(), table))
                        {
                            DataRow newFila;
                            newFila = table.NewRow();
                            newFila["LOGIN"] = dRow["LOGIN"].ToString();
                            newFila["Email"] = dRow["Email"].ToString();
                            newFila["Encuesta"] = dRow["Encuesta"].ToString();
                            newFila["ID_TIPO_ENCUESTA"] = dRow["ID_TIPO_ENCUESTA"].ToString();
                            newFila["CONTRASEÑA"] = dRow["CONTRASEÑA"].ToString();
                            newFila["ID_EstadoEncuesta"] = dRow["ID_EstadoEncuesta"].ToString();
                            newFila["Evaluado"] = dRow["Evaluado"].ToString();
                            newFila["Usuario"] = dRow["Usuario"].ToString();
                            newFila["ID_USUARIO"] = dRow["ID_USUARIO"].ToString();
                            table.Rows.Add(newFila);
                            
                       
                        }
                    }
                }
                newDataSet.Tables.Add(table);
                if (newDataSet.Tables.Count > 0) { 
                foreach(DataRow dRow in newDataSet.Tables[0].Rows){

                    
                    String[] destinario = new String[] { dRow["Email"].ToString() };
                    // la variable html retornar el cuerpo del mensaje que esta siendo mostrado por medio de codigo html
                    String html = retornarCuerpoCorreo(dRow["Usuario"].ToString(), dRow["Encuesta"].ToString(), dRow["Evaluado"].ToString(), dRow["ID_TIPO_ENCUESTA"].ToString(), dRow["LOGIN"].ToString(), dRow["CONTRASEÑA"].ToString(), dRow["ID_EstadoEncuesta"].ToString());
                    email.EnviarEmail(email.darDestinatarios(destinario), "tcescchp@hp.com", "SurveyToolsHP Encuestas", "Encuesta TCE SCC", html);
                    String strSQL = "UPDATE T_ENCUESTA_USUARIO SET uen_envioCorreo = 2 , uen_fechaEnviado = GETDATE() WHERE uen_usu_id = " + dRow["ID_USUARIO"];
                    clsSQL.ejecutarProceConsulSQL(strSQL,ref returnError);
                }
                }
                if (!returnError)
                Message("ENVIO CORREO", "Se envio el correo a los destinatarios", TipoMensaje.Validacion, true);
            }

        }

        private Boolean verificarCorreoRepitente(String nombreUsuario,DataTable prmTable)
        {
            int contt = 0;
            if (prmTable.Rows.Count > 0)
            {
                foreach (DataRow dRow in prmTable.Rows)
                {
                    if (dRow["LOGIN"].ToString() == nombreUsuario)
                    {
                        contt++;
                    }
                }
                if (contt != 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            return true;
        }

        protected void cmb_tipo_encuesta_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarEncuesta();
            cargarOpcionesRadioButton();
            
            this.pnl_envio_correo.Visible = true;
           
            this.pnl_ponderacion.Visible = true;
            if (this.cmb_tipo_encuesta.SelectedItem.Value == "2" && this.rdl_btn_filtro.SelectedItem.Value == "SatisfaccionISR")
            {
                 this.pnl_isr_vista.Visible = true;
            }
            else { 
             this.pnl_isr_vista.Visible = false;    
            
            }
        }
        

        protected void cmb_encuesta_SelectedIndexChanged(object sender, EventArgs e)
        {

            cargarISR();
            cargarGrilla();
           
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            enviarCorreo();
            cargarGrilla();
        }

     
        protected void dgrid_eficiencia_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count >= 18) {
                e.Row.Cells[0].Visible = true;
            e.Row.Cells[1].Visible=false;
                e.Row.Cells[2].Visible=false;
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Visible = false;
                    e.Row.Cells[6].Visible = false;
                    e.Row.Cells[7].Visible = false;
                    e.Row.Cells[13].Visible = false;
                    e.Row.Cells[14].Visible = false;
                    e.Row.Cells[16].Visible = false;
                    e.Row.Cells[18].Visible = false;
                    if (e.Row.Cells[12].Text == "Respuesta")
                    {
                        e.Row.Cells[0].Visible = false;
                        e.Row.Cells[1].Visible = false;
                        e.Row.Cells[2].Visible = true;
                        

                    }
                    else
                    {


                        if (e.Row.Cells[15].Text == "Correo ya enviado")
                        {
                        
                            e.Row.Cells[0].Visible = false;
                            e.Row.Cells[1].Visible = true;
                            e.Row.Cells[2].Visible = false;
                            
                        }
                        else if (e.Row.Cells[15].Text == "Por ser enviado")
                        {
                         
                            e.Row.Cells[0].Visible = true;
                            e.Row.Cells[1].Visible = false;
                            e.Row.Cells[2].Visible = false;

                        }
                    }
               
            }
        }

        protected void cmb_isr_SelectedIndexChanged1(object sender, EventArgs e)
        {
            cargarGrilla();
        }

        protected void dgrid_eficiencia_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName) {
                case "Select":
                    enviarCorreoOneUser(Convert.ToInt16(e.CommandArgument));
                    break;
                case "VerRespuesta":
                    verRespuesta(Convert.ToInt16(e.CommandArgument));
                    break;
                case "ReenviarCorreo":
                    enviarCorreoOneUser(Convert.ToInt16(e.CommandArgument));
                    break;
            }
        }
        private void enviarCorreoOneUser(int prmNumeroFila) {
            String nombreUser =this.dgrid_eficiencia.Rows[prmNumeroFila].Cells[8].Text;
            String nombreEncuesta = this.dgrid_eficiencia.Rows[prmNumeroFila].Cells[10].Text;
            String nombreTipoEncuesta = this.dgrid_eficiencia.Rows[prmNumeroFila].Cells[9].Text;
            String login = this.dgrid_eficiencia.Rows[prmNumeroFila].Cells[4].Text;
            String contrasenia = this.dgrid_eficiencia.Rows[prmNumeroFila].Cells[5].Text;
            String envioCorreoPrimeraVez = this.dgrid_eficiencia.Rows[prmNumeroFila].Cells[16].Text;
       
            String [] destinatarios = new String []{this.dgrid_eficiencia.Rows[prmNumeroFila].Cells[14].Text};
            //String[] destinatarios = new String[] { "oscar-eduardo.orduz-saenz@hp.com"};
            String idUsuario = this.dgrid_eficiencia.Rows[prmNumeroFila].Cells[3].Text;
            String html = retornarCuerpoCorreo(nombreUser, nombreEncuesta, "", nombreTipoEncuesta, login, contrasenia,envioCorreoPrimeraVez);
            String nombreEmail = "";
            if (envioCorreoPrimeraVez == "1")
            {
                nombreEmail = "ENCUESTA TCE SCC SURVEYHPTOOLS";
                
            }
            else if (envioCorreoPrimeraVez == "2") {
                nombreEmail = "ENCUESTAS TCE SCC **RECORDATORIO DE ENVIÓ DE RESPUESTA**";
            }
            email.EnviarEmail(email.darDestinatarios(destinatarios), "tcescchp@hp.com", "SURVEYHPTOOLS", nombreEmail, html);
            String strSQL = "UPDATE T_ENCUESTA_USUARIO SET uen_envioCorreo = 2 , uen_fechaEnviado = GETDATE() WHERE uen_estado = 1 AND uen_usu_id = " + idUsuario;
            Boolean returnError = false;
            clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError)
            {
                Message("Envio Correo", "Se envio solicitud al usuario " + this.dgrid_eficiencia.Rows[prmNumeroFila].Cells[2].Text, TipoMensaje.Validacion, true);
            }
            cargarGrilla();
        }

       

        private void verRespuesta(int prmNumFila) {
            HttpContext.Current.Session["ENCUESTA_NOM_ENCUESTA"]=this.dgrid_eficiencia.Rows[prmNumFila].Cells[10].Text;
            HttpContext.Current.Session["ENCUESTA_NOM_USUARIO"] = this.dgrid_eficiencia.Rows[prmNumFila].Cells[8].Text;
            HttpContext.Current.Session["ENCUESTA_ID_ENCUESTA"] = this.dgrid_eficiencia.Rows[prmNumFila].Cells[6].Text; 
            HttpContext.Current.Session["ENCUESTA_ID_USUARIO"] = this.dgrid_eficiencia.Rows[prmNumFila].Cells[3].Text; 
            HttpContext.Current.Session["ENCUESTA_EVALUADO"] = this.dgrid_eficiencia.Rows[prmNumFila].Cells[11].Text;
            HttpContext.Current.Session["ENCUESTA_FASE"] = this.dgrid_eficiencia.Rows[prmNumFila].Cells[18].Text;
            Response.Redirect("~/Content/MenuPrincipal/AdministradorRespuestas/RespuestaEspecifica.aspx");
        }

        protected void dgrid_eficiencia_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.dgrid_eficiencia.PageIndex= e.NewPageIndex;
            cargarGrilla();
        }

        protected void rdl_btn_filtro_SelectedIndexChanged(object sender, EventArgs e)
        {
            String valor = this.rdl_btn_filtro.SelectedItem.Value;
            this.pnl_ponderacion.Visible = false;
            this.pnl_isr_vista.Visible = false;
            this.pnl_encueta.Visible = false;
            switch (valor) {
                case "ServicioBu":
                    cargarEncuesta();
                    cargarGrilla();
                    this.pnl_encueta.Visible = true;
                    break;
                case "ServicioOP":
                    this.pnl_encueta.Visible = false;
                    this.pnl_isr_vista.Visible = false;
                    cargarGrilla();
                    break;
                case "SatisfaccionISR":
                    this.pnl_encueta.Visible = true;
                    this.pnl_isr_vista.Visible = true;
                    cargarGrilla();
                    break;
                case "SatisfaccionBU":
                    this.pnl_encueta.Visible = true;
                    this.pnl_isr_vista.Visible = false;
                    cargarGrilla();
                    break;
            }
        }

        protected void img_btn_exportar_excel_Click(object sender, ImageClickEventArgs e)
        {
            GridView newDataGrid = new GridView();
            cargarGrillaExcel(ref newDataGrid);
            
            exportarGridView_Excel(newDataGrid, "Efectividad " + this.cmb_tipo_encuesta.SelectedItem.Text  + " " + this.cmb_encuesta.SelectedItem.Text, 2);
        }

        private void cargarGrillaExcel( ref GridView prmdGrid)
        {

            Boolean returnError = false;
            String strSQL = "";
            String[] strSQLs = new String[2];
            if (this.rdl_btn_filtro.Items.Count > 0)
            {
                String opcion = "";
                
                if (rdl_btn_filtro.SelectedItem != null)
                    opcion = this.rdl_btn_filtro.SelectedItem.Value;
                switch (opcion)
                {
                    case "ServicioBu":
                        strSQL = "EXEC [SP_MM_DAR_EFECTIVIDAD] 2,1," + this.cmb_encuesta.SelectedItem.Value + ",'',"+this.cmb_fase.SelectedItem.Value;
                        strSQLs[0] = strSQL;
                        break;
                    case "ServicioOP":
                        strSQL = "EXEC [SP_MM_DAR_EFECTIVIDAD] 1,1,0,''" + this.cmb_fase.SelectedItem.Value;
                        strSQLs[0] = strSQL;
                    

                        break;
                    case "SatisfaccionISR":

                        strSQL = "EXEC [SP_MM_DAR_EFECTIVIDAD] 3,1," + this.cmb_encuesta.SelectedItem.Value + ",'" + this.cmb_isr.SelectedItem.Text + "'," + this.cmb_fase.SelectedItem.Value;
                        strSQLs[0] = strSQL;
                   
                        break;
                    case "SatisfaccionBU":
                        strSQL = "EXEC [SP_MM_DAR_EFECTIVIDAD] 4,1," + this.cmb_encuesta.SelectedItem.Value + ",''," + this.cmb_fase.SelectedItem.Value;
                        strSQLs[0] = strSQL;
                       

                        break;
                }

                DataSet ds = new DataSet();
                ds = clsSQL.ejecutarProceConsulSQL(strSQLs[0], ref returnError);
                if (!returnError)
                {
                    if (ds.Tables.Count > 0)
                    {
                        ds.Tables[0].Columns.Remove("ID_USUARIO");
                        ds.Tables[0].Columns.Remove("LOGIN");
                        ds.Tables[0].Columns.Remove("CONTRASEÑA");
                        ds.Tables[0].Columns.Remove("ID_ENCUESTA");
                        ds.Tables[0].Columns.Remove("ID_TIPO_ENCUESTA");
                        ds.Tables[0].Columns.Remove("ID_EstadoEncuesta");
                        ds.Tables[0].Columns.Remove("Estado Correo");
                 
                        HttpContext.Current.Session["USUARIOS_X_RESPONDER"] = ds;
                        prmdGrid.DataSource = ds;
                        prmdGrid.DataBind();
                     
                    }
                }
               

            }


        }

        protected void cmb_fase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmb_fase.SelectedItem.Value == "0")
            {
                this.pnl_con_data.Visible = false;
            }
            else {
                cargarTipoEncuestas();
            }
        }

    }
}