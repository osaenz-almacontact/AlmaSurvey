using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SURVEYTOOLSHP.Model;
using System.Data;

namespace SURVEYTOOLSHP.Content.MenuPrincipal.Cuentas
{
    public partial class CuentaDetalles : BasePage
    {
        private String mensaje = "";
        private String idCuenta = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            idCuenta = HttpContext.Current.Session["ID_CUENTA_ACTUAL"].ToString();
            if (!Page.IsPostBack)
            {
                cargarDatos();
                this.pnlEditarUsuario.Visible = true;
                this.lbl_nombre_usuario.Text = cargarNombreUsuario();

            }
        }


        #region cagar información de la cuenta

        private String cargarNombreUsuario()
        {
            String strSQL = "SELECT usu_nombre AS 'NOMBRE' FROM T_USUARIO WHERE usu_id = " + idCuenta;
            DataSet ds = new DataSet();
            Boolean returnError = false;
            String nombreUsu = "";
            ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError)
            {
                if (ds.Tables[0].Rows.Count == 1)
                {
                    foreach (DataRow dRow in ds.Tables[0].Rows)
                    {
                        nombreUsu = dRow["NOMBRE"].ToString();
                    }
                }
            }
            return nombreUsu;
        }

        private void cargarFases()
        {
            String strSQL = "SELECT fas_id AS 'ID',fas_nombre AS 'NOMBRE' FROM T_FASE";
            Boolean returnError = false;
            clsSQL.llenarComboBox(ref returnError, strSQL, ref this.cmb_fase, "ID", "NOMBRE");
        }
        private void cargarDatos()
        {

            String strSQL = "SELECT usu_id AS 'ID',usu_nombre AS 'NOMBRE',usu_login AS 'LOGIN', usu_contrasenia AS 'CONTRASENIA',usu_email AS 'CORREO',usu_estado AS 'ESTADO_USUARIO',usu_per_id AS 'PERMISOS_USUARIO' FROM T_USUARIO WHERE usu_id = " + idCuenta;
            DataSet ds = new DataSet();
            Boolean returnError = false;
            String estadoUsuario = "";
            String permisosUsuario = "";
            ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError)
            {
                if (ds.Tables[0].Rows.Count == 1)
                {
                    foreach (DataRow dRow in ds.Tables[0].Rows)
                    {
                        this.txt_usu_nombre.Text = dRow["NOMBRE"].ToString();
                        this.txt_contrasenia.Text = dRow["CONTRASENIA"].ToString();
                        this.txt_login.Text = dRow["LOGIN"].ToString();
                        this.txt_correo.Text = dRow["CORREO"].ToString();
                        estadoUsuario = dRow["ESTADO_USUARIO"].ToString();
                        permisosUsuario = dRow["PERMISOS_USUARIO"].ToString();
                    }
                    this.cmb_estado.SelectedItem.Value = estadoUsuario;
                    cargarComboPermisos(permisosUsuario);
                    cargarFases();
                    cargarCheklistEncuestas();

                    cargarEncuestasConAcceso();

                }
            }

        }

        private void cargarComboPermisos(String prmValor)
        {
            String strSQL = "SELECT per_id AS 'ID',per_nombre AS 'NOMBRE' FROM T_PERMISO WHERE per_estado = 1";
            Boolean returnError = false;
            clsSQL.llenarComboBox(ref returnError, strSQL, ref this.cmb_Permisos, "ID", "NOMBRE");
            if (!returnError)
            {
                if (this.cmb_Permisos.Items.Count > 0)
                {
                    this.cmb_Permisos.Items.FindByValue(prmValor).Selected = true;
                }
            }
        }



        private void cargarCheklistEncuestas()
        {

            Boolean returnError = false;
            String strSQL = "SELECT enc_id AS 'ID', enc_nombre AS 'NOMBRE'  FROM T_ENCUESTA WHERE enc_estado = 1 AND enc_tip_id = 1";
            DataSet ds = new DataSet();
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
            strSQL = "SELECT enc_id AS 'ID', enc_nombre AS 'NOMBRE'  FROM T_ENCUESTA WHERE enc_estado = 1 AND enc_tip_id = 2";
            returnError = false;
            ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError)
            {
                if (ds.Tables.Count > 0)
                {
                    this.chk_list_encuestas_satisfaccion.Items.Clear();
                    foreach (DataRow dRow in ds.Tables[0].Rows)
                    {
                        ListItem item = new ListItem();
                        item.Value = dRow["ID"].ToString();
                        item.Text = dRow["NOMBRE"].ToString();
                        this.chk_list_encuestas_satisfaccion.Items.Add(item);

                    }
                }

            }

            strSQL = "SELECT enc_id AS 'ID', enc_nombre AS 'NOMBRE'  FROM T_ENCUESTA WHERE enc_estado = 1 AND enc_tip_id = 3";
            returnError = false;
            ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError)
            {
                if (ds.Tables.Count > 0)
                {
                    this.chk_lst_encuesta_liderazgo.Items.Clear();
                    foreach (DataRow dRow in ds.Tables[0].Rows)
                    {
                        ListItem item = new ListItem();
                        item.Value = dRow["ID"].ToString();
                        item.Text = dRow["NOMBRE"].ToString();
                        this.chk_lst_encuesta_liderazgo.Items.Add(item);

                    }
                }

            }

        }




        #endregion

        private void cargarEncuestasConAcceso()
        {
            vaciarChecks();
            String strSQL = "";
            Boolean returnError = false;
            strSQL = "SELECT " +
                    "NOM_ENCUESTA.enc_id AS 'ID'," +
                    "NOM_ENCUESTA.enc_nombre AS 'NOMBRE' " +
                    "FROM T_ENCUESTA NOM_ENCUESTA " +
                    "INNER JOIN T_UNIDAD_ENCUESTA UNIDAD ON UNIDAD.uni_enc_id = NOM_ENCUESTA.enc_id " +
                    "INNER JOIN T_ENCUESTA_USUARIO USUARIO ON USUARIO.uen_id = UNIDAD.uni_uen_id " +
                    "WHERE " +
                    "USUARIO.uen_tip_id = 2 AND " +
                    "USUARIO.uen_usu_id = " + idCuenta + " AND " +
                    "USUARIO.uen_estado = 1 AND " +
                    "UNIDAD.uni_estado = 1 AND " +
                    "NOM_ENCUESTA.enc_estado = 1 AND " +
                    "USUARIO.uen_fas_id = " + this.cmb_fase.SelectedItem.Value;
            DataSet ds = new DataSet();

            ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError)
            {
                if (ds.Tables.Count > 0)
                {

                    foreach (DataRow dRow in ds.Tables[0].Rows)
                    {
                        for (int i = 0; i < this.chk_list_encuestas_satisfaccion.Items.Count; i++)
                        {
                            if (this.chk_list_encuestas_satisfaccion.Items[i].Value == dRow["ID"].ToString())
                            {
                                this.chk_list_encuestas_satisfaccion.Items[i].Selected = true;
                                break;
                            }
                        }
                    }
                }

            }
            strSQL = "SELECT " +
                   "NOM_ENCUESTA.enc_id AS 'ID'," +
                   "NOM_ENCUESTA.enc_nombre AS 'NOMBRE' " +
                   "FROM T_ENCUESTA NOM_ENCUESTA " +
                   "INNER JOIN T_UNIDAD_ENCUESTA UNIDAD ON UNIDAD.uni_enc_id = NOM_ENCUESTA.enc_id " +
                   "INNER JOIN T_ENCUESTA_USUARIO USUARIO ON USUARIO.uen_id = UNIDAD.uni_uen_id " +
                   "WHERE " +
                   "USUARIO.uen_tip_id = 1 AND " +
                   "USUARIO.uen_usu_id = " + idCuenta + " AND " +
                   "USUARIO.uen_estado = 1 AND " +
                   "UNIDAD.uni_estado = 1 AND " +
                   "NOM_ENCUESTA.enc_estado = 1 AND " +
                   "USUARIO.uen_fas_id = " + this.cmb_fase.SelectedItem.Value;
            ds = new DataSet();

            ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError)
            {
                if (ds.Tables.Count > 0)
                {

                    foreach (DataRow dRow in ds.Tables[0].Rows)
                    {
                        for (int i = 0; i < this.chk_lst_encuesta_servicio.Items.Count; i++)
                        {
                            if (this.chk_lst_encuesta_servicio.Items[i].Value == dRow["ID"].ToString())
                            {
                                this.chk_lst_encuesta_servicio.Items[i].Selected = true;
                                break;
                            }
                        }
                    }
                }

            }
        }

        private void vaciarChecks()
        {
            for (int i = 0; i < this.chk_list_encuestas_satisfaccion.Items.Count; i++)
            {
                this.chk_list_encuestas_satisfaccion.Items[i].Selected = false;
            }
            for (int i = 0; i < this.chk_lst_encuesta_servicio.Items.Count; i++)
            {
                this.chk_lst_encuesta_servicio.Items[i].Selected = false;
            }
        }
        private void editarUsuario()
        {

            String strSQL = "";
            Boolean returnError = false;

            if (VerificarDatos())
            {
                strSQL = "UPDATE T_USUARIO SET usu_contrasenia = '" + this.txt_contrasenia.Text + "',usu_nombre  = '" + this.txt_usu_nombre.Text + "',usu_login = '" + this.txt_login.Text + "', usu_email = '" + this.txt_correo.Text + "',usu_estado = " + this.cmb_estado.SelectedItem.Value + ", usu_per_id = " + this.cmb_Permisos.SelectedItem.Value + " WHERE usu_id  = " + idCuenta;
                clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
                if (!returnError)
                {
                    /*ENCUESTA DE TIPO  SATISFACCIÓN*/

                    strSQL = "SELECT DISTINCT uen_id AS 'ID' FROM T_ENCUESTA_USUARIO WHERE uen_usu_id = " + idCuenta + " AND uen_tip_id = 2 AND uen_estado = 1 AND uen_fas_id = " + this.cmb_fase.SelectedItem.Value;
                    int conttadorChekeoSatisfaccion = 0;
                    for (int j = 0; j < this.chk_list_encuestas_satisfaccion.Items.Count; j++)
                    {
                        if (this.chk_list_encuestas_satisfaccion.Items[j].Selected)
                        {
                            conttadorChekeoSatisfaccion++;
                        }
                    }

                    if (clsSQL.retornarDatoEscalar(ref returnError, strSQL) == null)
                    {
                        if (conttadorChekeoSatisfaccion > 0)
                        {
                            strSQL = "INSERT INTO T_ENCUESTA_USUARIO (uen_usu_id,uen_tip_id,uen_creadoXNt,uen_fechaCreacion,uen_estado,uen_fas_id)" +
                                "VALUES(" + idCuenta + ",2," + HttpContext.Current.Session["ID_USER"].ToString() + ",GETDATE(),1," + this.cmb_fase.SelectedItem.Value + ")";
                            clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
                            if (!returnError)
                            {
                                strSQL = "SELECT MAX(uen_id) FROM T_ENCUESTA_USUARIO WHERE uen_estado = 1";
                                String actualIdEncuestaUsuarioNuevo = clsSQL.retornarDatoEscalar(ref returnError, strSQL).ToString();
                                if (!returnError)
                                {
                                    if (!returnError)
                                    {
                                        for (int i = 0; i < this.chk_list_encuestas_satisfaccion.Items.Count; i++)
                                        {
                                            if (chk_list_encuestas_satisfaccion.Items[i].Selected)
                                            {
                                                strSQL = "INSERT INTO T_UNIDAD_ENCUESTA (uni_nombre,uni_estado,uni_uen_id,uni_fechaCreacion,uni_creadoXNt,uni_enc_id,uni_estado_encuesta)" +
                                                    "VALUES ((SELECT enc_nombre FROM T_ENCUESTA WHERE enc_id = " + this.chk_list_encuestas_satisfaccion.Items[i].Value + "),1," + actualIdEncuestaUsuarioNuevo + ",GETDATE()," + HttpContext.Current.Session["ID_USER"].ToString() + "," + this.chk_list_encuestas_satisfaccion.Items[i].Value + ",1)";
                                                clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
                                            }
                                        }

                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        if (conttadorChekeoSatisfaccion > 0)
                        {
                            String actualIDEncuestaUsuario = clsSQL.retornarDatoEscalar(ref returnError, strSQL).ToString();


                            clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
                            int contadorChekeo = 0;
                            for (int k = 0; k < this.chk_list_encuestas_satisfaccion.Items.Count; k++)
                            {
                                if (chk_list_encuestas_satisfaccion.Items[k].Selected)
                                {
                                    contadorChekeo++;
                                }
                            }
                            if (!returnError && contadorChekeo > 0)
                            {
                                for (int i = 0; i < this.chk_list_encuestas_satisfaccion.Items.Count; i++)
                                {
                                    strSQL = "SELECT COUNT(uni_id) FROM T_UNIDAD_ENCUESTA WHERE uni_uen_id = " + actualIDEncuestaUsuario + " AND uni_enc_id = " + this.chk_list_encuestas_satisfaccion.Items[i].Value;
                                    int contt = Convert.ToInt32(clsSQL.retornarDatoEscalar(ref returnError, strSQL).ToString());
                                    if (!returnError && chk_list_encuestas_satisfaccion.Items[i].Selected)
                                    {

                                        if (!returnError && contt == 0)
                                        {
                                            strSQL = "INSERT INTO T_UNIDAD_ENCUESTA (uni_nombre,uni_estado,uni_uen_id,uni_fechaCreacion,uni_creadoXNt,uni_enc_id,uni_estado_encuesta)" +
                                                "VALUES ((SELECT enc_nombre FROM T_ENCUESTA WHERE enc_id = " + this.chk_list_encuestas_satisfaccion.Items[i].Value + "),1," + actualIDEncuestaUsuario + ",GETDATE()," + HttpContext.Current.Session["ID_USER"].ToString() + "," + this.chk_list_encuestas_satisfaccion.Items[i].Value + ",1)";
                                            clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
                                        }

                                    }
                                    else
                                    {
                                        if (contt != 0)
                                        {
                                            strSQL = "UPDATE T_UNIDAD_ENCUESTA SET uni_estado = 0, uni_estado_encuesta = 0 WHERE uni_uen_id =" + actualIDEncuestaUsuario + " AND uni_enc_id = " + this.chk_list_encuestas_satisfaccion.Items[i].Value;
                                            clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
                                        }

                                    }
                                }
                                /* strSQL = "EXEC SP_MM_RESPUESTAS_ENCUESTAS_NOACTIVAS 2," + actualIDEncuestaUsuario;
                                 clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);*/
                            }
                        }

                    }
                }
                /*ENCUESTA DE TIPO SERVICIO*/

                strSQL = "SELECT DISTINCT uen_id AS 'ID' FROM T_ENCUESTA_USUARIO WHERE uen_usu_id = " + idCuenta + " AND uen_tip_id = 1 AND uen_estado = 1 AND uen_fas_id  = " + this.cmb_fase.SelectedItem.Value;
                int contadorChequeo = 0;
                for (int j = 0; j < this.chk_lst_encuesta_servicio.Items.Count; j++)
                {
                    if (this.chk_lst_encuesta_servicio.Items[j].Selected)
                    {
                        contadorChequeo++;
                    }
                }
                if (clsSQL.retornarDatoEscalar(ref returnError, strSQL) == null)
                {
                    if (contadorChequeo > 0)
                    {
                        strSQL = "INSERT INTO T_ENCUESTA_USUARIO (uen_usu_id,uen_tip_id,uen_creadoXNt,uen_fechaCreacion,uen_estado,uen_fas_id)" +
                            "VALUES(" + idCuenta + ",1," + HttpContext.Current.Session["ID_USER"].ToString() + ",GETDATE(),1," + this.cmb_fase.SelectedItem.Value + ")";
                        clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
                        if (!returnError)
                        {
                            strSQL = "SELECT MAX(uen_id) FROM T_ENCUESTA_USUARIO WHERE uen_estado = 1";
                            String actualIdEncuestaUsuarioNuevo = clsSQL.retornarDatoEscalar(ref returnError, strSQL).ToString();
                            if (!returnError)
                            {
                                if (!returnError)
                                {
                                    for (int i = 0; i < this.chk_lst_encuesta_servicio.Items.Count; i++)
                                    {
                                        if (chk_lst_encuesta_servicio.Items[i].Selected)
                                        {
                                            strSQL = "INSERT INTO T_UNIDAD_ENCUESTA (uni_nombre,uni_estado,uni_uen_id,uni_fechaCreacion,uni_creadoXNt,uni_enc_id,uni_estado_encuesta)" +
                                                "VALUES ((SELECT enc_nombre FROM T_ENCUESTA WHERE enc_id = " + this.chk_lst_encuesta_servicio.Items[i].Value + "),1," + actualIdEncuestaUsuarioNuevo + ",GETDATE()," + HttpContext.Current.Session["ID_USER"].ToString() + "," + this.chk_lst_encuesta_servicio.Items[i].Value + ",1)";
                                            clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
                                        }
                                    }

                                }
                            }
                        }
                    }

                }
                else if (contadorChequeo > 0)
                {
                    String actualIDEncuestaUsuario = clsSQL.retornarDatoEscalar(ref returnError, strSQL).ToString();


                    if (!returnError)
                    {
                        for (int i = 0; i < this.chk_lst_encuesta_servicio.Items.Count; i++)
                        {
                            strSQL = "SELECT COUNT(uni_id) FROM T_UNIDAD_ENCUESTA WHERE uni_uen_id = " + actualIDEncuestaUsuario + " AND uni_enc_id = " + this.chk_lst_encuesta_servicio.Items[i].Value + " AND uni_estado = 1";
                            int contt = Convert.ToInt32(clsSQL.retornarDatoEscalar(ref returnError, strSQL));

                            if (chk_lst_encuesta_servicio.Items[i].Selected)
                            {
                                if (!returnError && contt == 0)
                                {
                                    strSQL = "INSERT INTO T_UNIDAD_ENCUESTA (uni_nombre,uni_estado,uni_uen_id,uni_fechaCreacion,uni_creadoXNt,uni_enc_id,uni_estado_encuesta)" +
                                        "VALUES ((SELECT enc_nombre FROM T_ENCUESTA WHERE enc_id = " + this.chk_lst_encuesta_servicio.Items[i].Value + "),1," + actualIDEncuestaUsuario + ",GETDATE()," + HttpContext.Current.Session["ID_USER"].ToString() + "," + this.chk_lst_encuesta_servicio.Items[i].Value + ",1)";
                                    clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
                                }
                            }
                            else
                            {
                                if (contt > 0)
                                {
                                    strSQL = "UPDATE  T_UNIDAD_ENCUESTA SET uni_estado = 0, uni_estado_encuesta=0 WHERE uni_uen_id = " + actualIDEncuestaUsuario + " AND uni_enc_id = " + this.chk_list_encuestas_satisfaccion.Items[i].Value;
                                    clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
                                }
                            }
                        }
                        /* este modulo se desactivo por que se necesitan las respuestas de las otras fases strSQL = "EXEC SP_MM_RESPUESTAS_ENCUESTAS_NOACTIVAS 1," + actualIDEncuestaUsuario;
                         clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);*/
                    }

                }

                /*ENCUESTA DE TIPO LIDERAZGO*/
                strSQL = "SELECT DISTINCT uen_id AS 'ID' FROM T_ENCUESTA_USUARIO WHERE uen_usu_id = " + idCuenta + " AND uen_tip_id = 1 AND uen_estado = 1 AND uen_fas_id  = " + this.cmb_fase.SelectedItem.Value;
                int contadorChequeoLiderazgo = 0;
                for (int j = 0; j < this.chk_lst_encuesta_liderazgo.Items.Count; j++)
                {
                    if (this.chk_lst_encuesta_liderazgo.Items[j].Selected)
                    {
                        contadorChequeoLiderazgo++;
                    }
                }
                if (clsSQL.retornarDatoEscalar(ref returnError, strSQL) == null)
                {
                    if (contadorChequeoLiderazgo > 0)
                    {
                        strSQL = "INSERT INTO T_ENCUESTA_USUARIO (uen_usu_id,uen_tip_id,uen_creadoXNt,uen_fechaCreacion,uen_estado,uen_fas_id)" +
                            "VALUES(" + idCuenta + ",1," + HttpContext.Current.Session["ID_USER"].ToString() + ",GETDATE(),1," + this.cmb_fase.SelectedItem.Value + ")";
                        clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
                        if (!returnError)
                        {
                            strSQL = "SELECT MAX(uen_id) FROM T_ENCUESTA_USUARIO WHERE uen_estado = 1";
                            String actualIdEncuestaUsuarioNuevo = clsSQL.retornarDatoEscalar(ref returnError, strSQL).ToString();
                            if (!returnError)
                            {
                                if (!returnError)
                                {
                                    for (int i = 0; i < this.chk_lst_encuesta_liderazgo.Items.Count; i++)
                                    {
                                        if (chk_lst_encuesta_liderazgo.Items[i].Selected)
                                        {
                                            strSQL = "INSERT INTO T_UNIDAD_ENCUESTA (uni_nombre,uni_estado,uni_uen_id,uni_fechaCreacion,uni_creadoXNt,uni_enc_id,uni_estado_encuesta)" +
                                                "VALUES ((SELECT enc_nombre FROM T_ENCUESTA WHERE enc_id = " + this.chk_lst_encuesta_liderazgo.Items[i].Value + "),1," + actualIdEncuestaUsuarioNuevo + ",GETDATE()," + HttpContext.Current.Session["ID_USER"].ToString() + "," + this.chk_lst_encuesta_liderazgo.Items[i].Value + ",1)";
                                            clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
                                        }
                                    }

                                }
                            }
                        }
                    }

                }
                else if (contadorChequeoLiderazgo > 0)
                {
                    String actualIDEncuestaUsuario = clsSQL.retornarDatoEscalar(ref returnError, strSQL).ToString();


                    if (!returnError)
                    {
                        for (int i = 0; i < this.chk_lst_encuesta_liderazgo.Items.Count; i++)
                        {
                            strSQL = "SELECT COUNT(uni_id) FROM T_UNIDAD_ENCUESTA WHERE uni_uen_id = " + actualIDEncuestaUsuario + " AND uni_enc_id = " + this.chk_lst_encuesta_liderazgo.Items[i].Value + " AND uni_estado = 1";
                            int contt = Convert.ToInt32(clsSQL.retornarDatoEscalar(ref returnError, strSQL));

                            if (chk_lst_encuesta_liderazgo.Items[i].Selected)
                            {
                                if (!returnError && contt == 0)
                                {
                                    strSQL = "INSERT INTO T_UNIDAD_ENCUESTA (uni_nombre,uni_estado,uni_uen_id,uni_fechaCreacion,uni_creadoXNt,uni_enc_id,uni_estado_encuesta)" +
                                        "VALUES ((SELECT enc_nombre FROM T_ENCUESTA WHERE enc_id = " + this.chk_lst_encuesta_liderazgo.Items[i].Value + "),1," + actualIDEncuestaUsuario + ",GETDATE()," + HttpContext.Current.Session["ID_USER"].ToString() + "," + this.chk_lst_encuesta_liderazgo.Items[i].Value + ",1)";
                                    clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
                                }
                            }
                            else
                            {
                                if (contt > 0)
                                {
                                    strSQL = "UPDATE  T_UNIDAD_ENCUESTA SET uni_estado = 0, uni_estado_encuesta=0 WHERE uni_uen_id = " + actualIDEncuestaUsuario + " AND uni_enc_id = " + this.chk_list_encuestas_satisfaccion.Items[i].Value;
                                    clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
                                }
                            }
                        }
                        /* este modulo se desactivo por que se necesitan las respuestas de las otras fases strSQL = "EXEC SP_MM_RESPUESTAS_ENCUESTAS_NOACTIVAS 1," + actualIDEncuestaUsuario;
                         clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);*/
                    }

                }


                if (!returnError)
                    Message("EDITAR CUENTA DE USUARIO", "Los datos del usuario " + cargarNombreUsuario() + " fueron editados", TipoMensaje.Validacion, true);

            }
        }
        #region verifica los datos ingresados por el usuario y los valida 

        private Boolean VerificarDatos()
        {
            if (this.txt_usu_nombre.Text.Length > 0 && this.txt_login.Text.Length > 0 && this.txt_contrasenia.Text.Length > 0)
            {

                return true;
            }
            else
            {
                if (this.txt_usu_nombre.Text.Length == 0)
                {
                    mensaje = mensaje + "Falto Digitar el nombre del usuario";
                }
                if (this.txt_login.Text.Length == 0)
                {
                    mensaje = mensaje + "Falto Digitar el login del usuario";
                }
                if (this.txt_contrasenia.Text.Length == 0)
                {
                    mensaje = mensaje + "Falto Digitar la contraseña del usuario";
                }
                return false;
            }

        }

        #endregion

        protected void btn_Enviar_cambios_Click(object sender, EventArgs e)
        {
            editarUsuario();
        }

        protected void btn_ocultar_panel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Content/MenuPrincipal/Cuentas/AdministrarCuentas.aspx");
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            darNuevaContrasenia();
        }
        private void darNuevaContrasenia()
        {
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

        protected void cmb_fase_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarEncuestasConAcceso();
        }
    }
}