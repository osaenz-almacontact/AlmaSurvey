using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SURVEYTOOLSHP.Model;

namespace SURVEYTOOLSHP.Content.MenuPrincipal.Cuentas
{
    public partial class AdministrarCuentas : BasePage
    {
        private String mensaje = "";
        private String idUsuarioActual = "0";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                HttpContext.Current.Session["CREAR_USUARIO"] = false;
                cargarGrilla();
            }
        }


        #region Carga de datos

        private void cargarGrilla()
        {
            String strSQL = "SELECT U.usu_id AS 'ID'," +
                            "U.usu_nombre  AS 'USUARIO'," +
                            "U.usu_login AS 'LOGIN'," +
                            "U.usu_contrasenia AS 'CONTRASENIA'," +
                            "P.per_nombre AS 'PERMISO', " +
                            "(CASE WHEN usu_estado = 1 THEN 'ACTIVO' ELSE 'DESACTIVO' END) AS 'ESTADO' " +
                            "FROM T_USUARIO U " +
                            "INNER JOIN T_PERMISO P ON U.usu_per_id = P.per_id " +
                            "ORDER BY USUARIO ASC";
            Boolean returnError = false;
            DataSet ds = new DataSet();
            ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError)
            {
                if (ds.Tables.Count > 0)
                {
                    this.dgrid_Cuentas.DataSource = ds;
                    this.dgrid_Cuentas.DataBind();

                }
            }
        }

        private void cargarFases()
        {

            String strSQL = "SELECT fas_id AS 'ID', tfas_nombre +' - '+ fas_nombre AS 'NOMBRE' FROM T_FASE INNER JOIN [dbo].[T_TIPO_FASE] ON fas_tipo_id = tfas_id";
            if (HttpContext.Current.Session["PERMISOS"].ToString() == "4")
            {
                strSQL = "SELECT fas_id AS 'ID', tfas_nombre +' - '+ fas_nombre AS 'NOMBRE' FROM T_FASE INNER JOIN [dbo].[T_TIPO_FASE] ON fas_tipo_id = tfas_id WHERE T_TIPO_FASE.tfas_id = 2";
            }
            Boolean returnError = false;
            clsSQL.llenarComboBox(ref returnError, strSQL, ref this.cmb_fase, "ID", "NOMBRE");

        }

        private void cargarCombosNuevoUsurio()
        {

            cargarFases();
            String strSQL = "SELECT tip_id AS 'ID', tip_nombre AS  'NOMBRE' FROM T_TIPO_ENCUESTA WHERE tip_estado =  1";
            Boolean returnError = false;
            clsSQL.llenarComboBox(ref returnError, strSQL, ref cmb_tipo_encuesta_nuevo_usuario, "ID", "NOMBRE");
            if (!returnError)
            {
                if (this.cmb_tipo_encuesta_nuevo_usuario.Items.Count > 0)
                {
                    cargarNombreEncuesta();
                }
            }


        }

        private void cargarNombreEncuesta()
        {
            String strSQL = "";
            Boolean returnError = false;
            strSQL = "SELECT enc_id AS 'ID', enc_nombre AS 'NOMBRE' FROM T_ENCUESTA WHERE enc_tip_id = " + this.cmb_tipo_encuesta_nuevo_usuario.SelectedItem.Value + " AND enc_estado = 1";
            clsSQL.llenarComboBox(ref returnError, strSQL, ref cmb_encuesta_nuevo_usuario, "ID", "NOMBRE");


        }

        private void cargarTipoEncuestaISR()
        {
            //String strSQL = "SELECT tip_id AS 'ID',tip_nombre AS 'NOMBRE' FROM T_TIPO_ENCUESTA WHERE tip_estado = 1 AND tip_id = 2 ";
            String strSQL = "SELECT tip_id AS 'ID',tip_nombre AS 'NOMBRE' FROM T_TIPO_ENCUESTA WHERE tip_estado = 1 AND tip_id = 2  OR tip_estado = 1 AND tip_id = 3  OR tip_estado = 1 AND tip_id = 4 AND 5";
            Boolean returnError = false;
            clsSQL.llenarComboBox(ref returnError, strSQL, ref this.cmb_tipo_de_encuesta_isr, "ID", "NOMBRE");
        }

        private void cargarEncuestasISR()
        {

            String strSQL = "SELECT " +
                            "ENCUESTANOMBRE.enc_id AS 'ID', " +
                            "ENCUESTANOMBRE.enc_nombre AS 'NOMBRE' " +
                            "FROM T_UNIDAD_ENCUESTA ENCUESTA " +
                            "INNER JOIN T_ENCUESTA_USUARIO USUARIO ON USUARIO.uen_id = ENCUESTA.uni_uen_id " +
                            "INNER JOIN T_ENCUESTA ENCUESTANOMBRE ON ENCUESTANOMBRE.enc_id = ENCUESTA.uni_enc_id " +
                            "WHERE USUARIO.uen_usu_id =" + HttpContext.Current.Session["ID_ACUTAL_EDITAR_USARIO"].ToString() + "  AND USUARIO.uen_estado = 1 AND ENCUESTA.uni_estado = 1 AND USUARIO.uen_tip_id = " + cmb_tipo_de_encuesta_isr.SelectedItem.Value + " AND ENCUESTANOMBRE.enc_estado = 1";

            Boolean returnError = false;

            clsSQL.llenarComboBox(ref returnError, strSQL, ref cmb_encuesta_cambiar_isr, "ID", "NOMBRE");
            if (cmb_encuesta_cambiar_isr.Items.Count > 0)
            {
                agregarEditarISR();
            }
            else
            {
                this.pnl_agregar_isr.Visible = false;
            }

        }

        private void cargarGrillaExcel(ref GridView prmDataView)
        {
            String strSQL = "SELECT U.usu_id AS 'ID'," +
                               "U.usu_nombre  AS 'USUARIO'," +
                               "U.usu_login AS 'LOGIN'," +
                               "U.usu_contrasenia AS 'CONTRASENIA'," +
                               "P.per_nombre AS 'PERMISO', " +
                               "(CASE WHEN usu_estado = 1 THEN 'ACTIVO' ELSE 'DESACTIVO' END) AS 'ESTADO' " +
                               "FROM T_USUARIO U " +
                               "INNER JOIN T_PERMISO P ON U.usu_per_id = P.per_id " +
                               "ORDER BY USUARIO ASC";
            Boolean returnError = false;
            DataSet ds = new DataSet();
            ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError)
            {
                if (ds.Tables.Count > 0)
                {
                    ds.Tables[0].Columns.Remove("ID");
                    prmDataView.DataSource = ds;
                    prmDataView.DataBind();

                }
            }

        }

        #endregion

        #region modificar datos

        private void agregarISR()
        {
            if (HttpContext.Current.Session["ID_ACUTAL_EDITAR_USARIO"] != null)
            {

                String strSQL = "EXEC SP_MM_EDITAR_ISR " + HttpContext.Current.Session["ID_ACUTAL_EDITAR_USARIO"].ToString() + "," + this.cmb_encuesta_cambiar_isr.SelectedItem.Value + "," + HttpContext.Current.Session["ID_USER"].ToString() + ",'" + txt_isr_nuevos_editados.Text + "'";

                Boolean returnError = false;
                clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
                if (!returnError)
                {
                    Message("AGREGAR ISR", "Los isr fueron ingresados", TipoMensaje.Validacion, true);
                }
            }
        }

        private void agregarEditarISR()
        {



            String strSQL = "SELECT ISR.isr_nombre AS 'NOMBRE_ISR' " +
                            "FROM T_ISR ISR " +
                            "INNER JOIN T_UNIDAD_ENCUESTA ENCUESTA ON ENCUESTA.uni_id  = ISR.isr_uni_id " +
                            "INNER JOIN T_ENCUESTA_USUARIO USUARIO ON USUARIO.uen_id  = ENCUESTA.uni_uen_id " +
                            "WHERE USUARIO.uen_tip_id = " + this.cmb_tipo_de_encuesta_isr.SelectedItem.Value + " AND ENCUESTA.uni_enc_id  = " + this.cmb_encuesta_cambiar_isr.SelectedItem.Value + " AND USUARIO.uen_usu_id = " + HttpContext.Current.Session["ID_ACUTAL_EDITAR_USARIO"].ToString() + " AND ENCUESTA.uni_estado = 1 AND USUARIO.uen_estado = 1 AND ISR.isr_estado=1";

            DataSet ds = new DataSet();
            Boolean returnError = false;
            ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError)
            {

                if (ds.Tables[0].Rows.Count > 0)
                {
                    String strMensaje = "";
                    foreach (DataRow dRow in ds.Tables[0].Rows)
                    {
                        strMensaje = strMensaje + dRow["NOMBRE_ISR"].ToString() + ",";

                    }
                    this.txt_isr_nuevos_editados.Text = strMensaje;
                }
                else
                {
                    this.txt_isr_nuevos_editados.Text = "";
                }
            }


        }

        private void filtrarDatos()
        {
            String strSQL = "";
            if (txt_filtro_por_usuario.Text.Length > 0)
            {
                strSQL = "SELECT U.usu_id AS 'ID'," +
                               "U.usu_nombre  AS 'USUARIO'," +
                               "U.usu_login AS 'LOGIN'," +
                               "U.usu_contrasenia AS 'CONTRASENIA'," +
                               "P.per_nombre AS 'PERMISO', " +
                               "(CASE WHEN usu_estado = 1 THEN 'ACTIVO' ELSE 'DESACTIVO' END) AS 'ESTADO' " +
                               "FROM T_USUARIO U " +
                               "INNER JOIN T_PERMISO P ON U.usu_per_id = P.per_id " +
                               "WHERE U.usu_nombre LIKE '%" + this.txt_filtro_por_usuario.Text + "%'" +
                               "ORDER BY USUARIO ASC";
            }
            else
            {
                strSQL = "SELECT U.usu_id AS 'ID'," +
                                  "U.usu_nombre  AS 'USUARIO'," +
                                  "U.usu_login AS 'LOGIN'," +
                                  "U.usu_contrasenia AS 'CONTRASENIA'," +
                                  "P.per_nombre AS 'PERMISO', " +
                                  "(CASE WHEN usu_estado = 1 THEN 'ACTIVO' ELSE 'DESACTIVO' END) AS 'ESTADO' " +
                                  "FROM T_USUARIO U " +
                                  "INNER JOIN T_PERMISO P ON U.usu_per_id = P.per_id " +
                                  "ORDER BY USUARIO ASC";

            }
            DataSet ds = new DataSet();
            Boolean returnError = false;
            ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError)
            {
                if (ds.Tables.Count > 0)
                {
                    this.dgrid_Cuentas.DataSource = ds;
                    this.DataBind();
                }
            }
        }

        private void editarUsuario(String prmNumeroFila)
        {
            HttpContext.Current.Session["ID_CUENTA_ACTUAL"] = this.dgrid_Cuentas.Rows[Convert.ToInt32(prmNumeroFila)].Cells[3].Text;
            Response.Redirect("~/Content/MenuPrincipal/Cuentas/CuentaDetalles.aspx");

        }

        private void deshabilitarCuenta(int prmNumFila)
        {
            String strSQL = "EXEC SP_MM_CAMBIARESTUSUARIO " + this.dgrid_Cuentas.Rows[prmNumFila].Cells[3].Text + "," + HttpContext.Current.Session["ID_USER"].ToString();
            Boolean returnError = false;
            clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError)
            {
                Message("EDITAR CUENTA", "El usuario " + this.dgrid_Cuentas.Rows[prmNumFila].Cells[5].Text + " fue deshabilitado", TipoMensaje.Validacion, true);
                filtrarDatos();

            }
        }

        #endregion

        #region verificar datos






        #endregion

        #region eventos

        protected void dgrid_Cuentas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count >= 6)
            {
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;

            }
        }

        protected void dgrid_Cuentas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            String actualFila = e.CommandArgument.ToString();
            switch (e.CommandName)
            {
                case "Editar":
                    editarUsuario(actualFila);

                    if (this.pnl_agregar_isr.Visible)
                    {
                        this.pnl_agregar_isr.Visible = false;
                    }

                    break;
                case "AgregarISR":
                    HttpContext.Current.Session["ID_ACUTAL_EDITAR_USARIO"] = this.dgrid_Cuentas.Rows[Convert.ToInt32(actualFila)].Cells[3].Text.ToString();
                    cargarTipoEncuestaISR();
                    cargarEncuestasISR();


                    this.pnl_agregar_isr.Visible = true;
                    break;
                case "Desahabilitar":
                    deshabilitarCuenta(Convert.ToInt32(e.CommandArgument));

                    break;
            }
        }

        protected void cmb_permiso_encuesta_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cargarNombreEncuestaEvaluado()
        {
            String strSQL = "";
            Boolean returnError = false;
            strSQL = "SELECT enc_id AS 'ID', enc_nombre AS 'NOMBRE' FROM T_ENCUESTA WHERE enc_tip_id = " + this.cmb_tipo_de_encuesta_isr.SelectedItem.Value + " AND enc_estado = 1";
            clsSQL.llenarComboBox(ref returnError, strSQL, ref cmb_encuesta_cambiar_isr, "ID", "NOMBRE");

        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void Image1_Click(object sender, ImageClickEventArgs e)
        {
            this.pnl_importar_archivo_excel.Visible = true;
            cargarCombosNuevoUsurio();
        }

        protected void lnk_importar_archivo_Click(object sender, EventArgs e)
        {
            Boolean returnError = false;
            String strSQL = "TRUNCATE TABLE T_AUX_ACTUALIZAR_USUARIO";
            clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError)
            {
                exportarExcelSQL(ref returnError, ref this.file_upload_excel, "T_AUX_ACTUALIZAR_USUARIO");
                if (!returnError)
                {
                    strSQL = "EXEC SP_INGRESAR_USUARIO_EXCEL " + HttpContext.Current.Session["ID_USER"] + "," + this.cmb_tipo_encuesta_nuevo_usuario.SelectedItem.Value + "," + this.cmb_encuesta_nuevo_usuario.SelectedItem.Value + "," + this.cmb_fase.SelectedItem.Value;
                    clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
                    if (!returnError)
                    {
                        Message("INGRESO DE USUARIO", "Los usuario fueron ingresados", TipoMensaje.Validacion, true);
                        cargarGrilla();
                    }
                }
                else
                {
                    Panel pnlAux = (Panel)Master.FindControl("pnl_Mensaje");
                    if (!pnlAux.Visible)
                        Message("IMPORTAR ARCHIVO", "Eliga un archivo", TipoMensaje.Information, true);
                }
            }


        }

        protected void cmb_tipo_encuesta_nuevo_usuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarNombreEncuesta();
        }

        protected void btn_Enviar_Cambio_ISR_Click(object sender, EventArgs e)
        {
            agregarISR();

        }

        protected void btn_ocultar_Click(object sender, EventArgs e)
        {
            this.pnl_agregar_isr.Visible = false;
            HttpContext.Current.Session["ID_ACUTAL_EDITAR_USARIO"] = String.Empty;
        }

        protected void cmb_encuesta_cambiar_isr_SelectedIndexChanged(object sender, EventArgs e)
        {
            agregarEditarISR();
        }

        protected void dgrid_Cuentas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.dgrid_Cuentas.PageIndex = e.NewPageIndex;
            cargarGrilla();
        }

        protected void lnk_btn_filtro_Click(object sender, EventArgs e)
        {
            filtrarDatos();
        }

        protected void img_btn_exportar_excel_Click(object sender, ImageClickEventArgs e)
        {
            GridView newdGrid = new GridView();
            cargarGrillaExcel(ref newdGrid);

            exportarGridView_Excel(newdGrid, "Cuentas", 2);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Content/MenuPrincipal/Cuentas/CrearUsuario.aspx");
        }
        protected void cmb_tipo_de_encuesta_isr_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarNombreEncuestaEvaluado();
        }

        #endregion


    }
}