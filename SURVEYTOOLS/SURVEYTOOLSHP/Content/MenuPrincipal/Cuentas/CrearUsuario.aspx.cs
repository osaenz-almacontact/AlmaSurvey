using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SURVEYTOOLSHP.Model;

namespace SURVEYTOOLSHP.Content.MenuPrincipal.Cuentas
{
    public partial class CrearUsuario : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {
                cargarDatos();
            }
        }

        private void cargarDatos() {
            cargarfases();
            cargarCombox();
            cargarEncuestas();
        }
        private void cargarCombox() {
            String strSQL = "SELECT per_id AS 'ID',  per_nombre AS 'NOMBRE' FROM T_PERMISO WHERE per_estado = 1";
            Boolean returnError = false;
            clsSQL.llenarComboBox(ref returnError, strSQL, ref cmb_Permisos, "ID", "NOMBRE");

        
        }
        private void cargarfases() {
            String strSQL = "SELECT fas_id AS 'ID',fas_nombre AS 'NOMBRE' FROM T_FASE";
            Boolean returnError = false;
            clsSQL.llenarComboBoxConSelect(ref returnError, strSQL, ref this.cmb_fase, "ID", "NOMBRE");
        }

        private void cargarEncuestas() {
            String strSQL = "SELECT enc_id AS 'ID',enc_nombre AS 'NOMBRE' FROM T_ENCUESTA WHERE enc_tip_id = 2 AND enc_estado = 1";
            Boolean returnError = false;
            DataSet ds = new DataSet();
            ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError) {
                if (ds.Tables.Count > 0)
                {
                    this.chk_list_encuestas_satisfaccion.Items.Clear();
                    foreach (DataRow dRow in ds.Tables[0].Rows) {
                        ListItem item = new ListItem();
                        item.Value = dRow["ID"].ToString();
                        item.Text = dRow["NOMBRE"].ToString();
                        this.chk_list_encuestas_satisfaccion.Items.Add(item);
                    }
                }
                
            }
            strSQL = "SELECT enc_id AS 'ID',enc_nombre AS 'NOMBRE' FROM T_ENCUESTA WHERE enc_tip_id = 1 AND enc_estado = 1";
            returnError = false;
            ds = new DataSet();
            ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError)
            {
                if (ds.Tables.Count > 0)
                {
                    this.chk_lst_encuesta_servicio.Items.Clear();
                    foreach (DataRow dRow in ds.Tables[0].Rows)
                    {
                        ListItem item = new ListItem();
                        item.Value = dRow["ID"].ToString();
                        item.Text = dRow["NOMBRE"].ToString();
                        this.chk_lst_encuesta_servicio.Items.Add(item);
                    }
                }

            }
            
        }

        private void crearNuevoUsuario()
        {
            Boolean returnError = false;
            String strSQL = "";
            if (!verficarExiteciaCuenta(1, this.txt_login.Text))
            {


                strSQL = "INSERT INTO T_USUARIO " +
                   "(usu_nombre,usu_login,usu_email,usu_contrasenia,usu_fechaCreacion,usu_creadoXUsuario,usu_estado,usu_per_id)" +
                   " VALUES (UPPER('" + this.txt_usu_nombre.Text + "'),'" + this.txt_login.Text + "','" + this.txt_login.Text + "','" + this.txt_contrasenia.Text + "',GETDATE()," + HttpContext.Current.Session["ID_USER"].ToString() + ",1," + this.cmb_Permisos.SelectedItem.Value + ")";
                clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);



               
                if (!returnError )
                { 
                    if(this.cmb_fase.SelectedItem.Value!="0"){
                    String idUsuario = "";
                    strSQL = "SELECT (CASE WHEN MAX(usu_id) IS NULL THEN 1 ELSE MAX(usu_id) END) FROM T_USUARIO";
                    idUsuario = clsSQL.retornarDatoEscalar(ref returnError, strSQL).ToString();
           
                    if (!returnError)
                    {
                        /*ENCUESTAS DE TIPO SATISFACCIÓN*/
                        if (!verificarEncuestasUsuario(idUsuario, "2"))
                        {
                            strSQL = "INSERT INTO T_ENCUESTA_USUARIO (uen_usu_id,uen_tip_id,uen_creadoXNt,uen_fechaCreacion,uen_estado,uen_fas_id)" +
                               "VALUES(" + idUsuario + ",2,'" + HttpContext.Current.Session["ID_USER"].ToString() + "',GETDATE(),1," + this.cmb_fase.SelectedItem.Value + ")";
                            clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);

                            if (!returnError)
                            {
                                strSQL = "SELECT MAX(uni_id) FROM T_UNIDAD_ENCUESTA";
                                String maxIDEncuesta = clsSQL.retornarDatoEscalar(ref returnError, strSQL).ToString();
                                if (!returnError)
                                {
                                    for (int i = 0; i < this.chk_list_encuestas_satisfaccion.Items.Count; i++)
                                    {
                                        if (this.chk_list_encuestas_satisfaccion.Items[i].Selected)
                                        {
                                            strSQL = "INSERT INTO T_UNIDAD_ENCUESTA (uni_nombre,uni_uen_id,uni_estado,uni_fechaCreacion,uni_creadoXNt,uni_enc_id,uni_estado_encuesta)" +
                                                "VALUES('" + this.chk_list_encuestas_satisfaccion.Items[i].Text + "'," + maxIDEncuesta + ",1,GETDATE()," + HttpContext.Current.Session["ID_USER"].ToString() + "," + this.chk_list_encuestas_satisfaccion.Items[i].Value + ",1)";
                                            clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
                                        }
                                    }
                                }
                                if (!returnError)
                                {
                                    Message("CREACIÓN DE CUENTA", "El usuario " + this.txt_login.Text + "fue creado", TipoMensaje.Validacion, true);

                                }
                            }
                        }
                        else
                        {
                            Message("CREACIÓN DE CUENTA", "El usuario " + this.txt_login.Text + " ya tiene asignado las encuestas de tipo satisfacción ", TipoMensaje.Information, true);

                        }
                    }

                        /*ENCUESTAS DE TIPO SERVICIO*/
                    if (!verificarEncuestasUsuario(idUsuario, "1"))
                    {
                       
                            strSQL = "INSERT INTO T_ENCUESTA_USUARIO (uen_usu_id,uen_tip_id,uen_creadoXNt,uen_fechaCreacion,uen_estado,uen_fas_id)" +
                               "VALUES(" + idUsuario + ",1,'" + HttpContext.Current.Session["ID_USER"].ToString() + "',GETDATE(),1," + this.cmb_fase.SelectedItem.Value + ")";
                            clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);

                            if (!returnError)
                            {
                                strSQL = "SELECT MAX(uni_id) FROM T_UNIDAD_ENCUESTA";
                                String maxIDEncuesta = clsSQL.retornarDatoEscalar(ref returnError, strSQL).ToString();
                                if (!returnError)
                                {
                                    for (int i = 0; i < this.chk_lst_encuesta_servicio.Items.Count; i++)
                                    {
                                        if (this.chk_lst_encuesta_servicio.Items[i].Selected)
                                        {
                                            strSQL = "INSERT INTO T_UNIDAD_ENCUESTA(uni_nombre,uni_uen_id,uni_estado,uni_fechaCreacion,uni_creadoXNt,uni_enc_id,uni_estado_encuesta)" +
                                                "VALUES('" + this.chk_lst_encuesta_servicio.Items[i].Text + "'," + maxIDEncuesta + ",1,GETDATE()," + HttpContext.Current.Session["ID_USER"].ToString() + "," + this.chk_lst_encuesta_servicio.Items[i].Value + ",1)";
                                            clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
                                        }
                                    }
                                }
                                if (!returnError)
                                {
                                    Message("CREACIÓN DE CUENTA", "El usuario " + this.txt_login.Text + " fue creado", TipoMensaje.Validacion, true);

                                }
                            }
                       
                       
                    }
                    else
                    {
                        Message("CREACIÓN DE CUENTA", "El usuario " + this.txt_login.Text + " ya tiene asignado las encuestas de tipo satisfacción ", TipoMensaje.Information, true);
                    }

                    }
                    else
                    {
                        Message("CREACIÓN DE CUENTA", "Se debe seleccionar la fase la cual va a responder las encuestas ", TipoMensaje.Information, true);
                        this.lbl_val_fase.Visible = true;
                    }
                }

            }
            else {
                Message("CREACIÓN DE CUENTA", "El usuario " + this.txt_login.Text + " ya existe ", TipoMensaje.Information, true);           
            }
        }


        private Boolean verificarEncuestasUsuario(String prmIdUsuario, String tipoEncuesta)
        {
            Boolean returnError = false;
            String strSQL = "SELECT COUNT(uen_id) FROM T_ENCUESTA_USUARIO WHERE uen_usu_id = " + prmIdUsuario + " AND uen_tip_id = " + tipoEncuesta;
            if (Convert.ToInt32(clsSQL.retornarDatoEscalar(ref returnError, strSQL)) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        private Boolean verficarExiteciaCuenta(int opc, String prmUsuario)
        {
            if (opc == 1)
            {
                Boolean returnError = false;
                String strSQL = "SELECT COUNT(usu_id) FROM T_USUARIO WHERE usu_login = '" + prmUsuario + "' AND usu_estado = 1";
                int existe = Convert.ToInt32(clsSQL.retornarDatoEscalar(ref returnError, strSQL));
                if (existe > 0)
                {
                    return true;
                }
                else
                {
                    strSQL = "SELECT COUNT(usu_id) FROM T_USUARIO WHERE usu_contrasenia = '" + this.txt_contrasenia.Text + "'";
                    int existeContrasenia = Convert.ToInt32(clsSQL.retornarDatoEscalar(ref returnError, strSQL));
                    if (existeContrasenia > 0)
                    {
                        return true;
                    }
                    return false;
                }

            } return true;

        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            darNuevaContrasenia();
        }
        private void darNuevaContrasenia() {
            String strSQL = "EXEC SP_MM_DAR_CONTRASENIA";
            Boolean returnError = false;
            String contraseña_nueva = "";
            contraseña_nueva = clsSQL.retornarDatoEscalar(ref returnError, strSQL).ToString();
            strSQL = "SELECT COUNT(usu_id) FROM T_USUARIO WHERE usu_contrasenia  = '" + contraseña_nueva + "'";
            int ext = Convert.ToInt32(clsSQL.retornarDatoEscalar(ref returnError, strSQL));
            while (ext > 0)
            {
                strSQL = "EXEC SP_MM_DAR_CONTRASENIA";
                returnError = false;
                contraseña_nueva = "";
                contraseña_nueva = clsSQL.retornarDatoEscalar(ref returnError, strSQL).ToString();
                strSQL = "SELECT COUNT(usu_id) FROM T_USUARIO WHERE usu_contrasenia  = '" + contraseña_nueva + "'";
                ext = Convert.ToInt32(clsSQL.retornarDatoEscalar(ref returnError, strSQL));
            }
            this.txt_contrasenia.Text = contraseña_nueva;
        }

        protected void btn_ocultar_panel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Content/MenuPrincipal/Cuentas/AdministrarCuentas.aspx");
        }

        protected void btn_Enviar_cambios_Click(object sender, EventArgs e)
        {
            crearNuevoUsuario();
        }

        protected void cmb_fase_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lbl_val_fase.Visible = false;
        }
    }
}