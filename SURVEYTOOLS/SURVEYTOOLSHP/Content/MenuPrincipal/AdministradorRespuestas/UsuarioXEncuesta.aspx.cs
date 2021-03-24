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
    public partial class UsuarioXEncuesta : BasePage
    {
        private clsEmail email;
        protected void Page_Load(object sender, EventArgs e)
        {
            email = new clsEmail();
            if (!Page.IsPostBack)
            {
                cargarGrilla();
                cargarTipoEncuesta();
            }
        }
        private void cargarGrilla()
        {
            String strSQL = "";
            Boolean returnError = false;
            DataSet ds = new DataSet();
            //if (this.pnl_filtro_X_Encuesta.Visible) {
            if (this.cmb_encuesta.Items.Count > 0 && this.cmb_tipo_encuesta.Items.Count > 0)
                strSQL = "EXEC [SP_MM_LISTA_USUARIO_X_ENCUESTA] 'SELECT * FROM #tmpFinalUsuarioXEncuesta WHERE ID_TIPO_ENCUESTA = " + cmb_tipo_encuesta.SelectedItem.Value + " AND ID_ENCUESTA = " + cmb_encuesta.SelectedItem.Value + "'";
            else
                strSQL = "EXEC [SP_MM_LISTA_USUARIO_X_ENCUESTA] 'SELECT * FROM #tmpFinalUsuarioXEncuesta'";
            // }
            // else if (this.pnl_filtro_usuario.Visible)
            //{
            //if (this.txt_filtro_usuario.Text.Length > 0)
            //    strSQL = "EXEC [SP_MM_LISTA_USUARIO_X_ENCUESTA] 'SELECT * FROM #tmpFinalUsuarioXEncuesta WHERE USUARIO LIKE ''%" + this.txt_filtro_usuario.Text + "%'''";
            //else
            //    strSQL = "EXEC [SP_MM_LISTA_USUARIO_X_ENCUESTA] 'SELECT * FROM #tmpFinalUsuarioXEncuesta'";
            //}
            //else if (!this.pnl_filtro_X_Encuesta.Visible && !this.pnl_filtro_usuario.Visible) {
            strSQL = "EXEC [SP_MM_LISTA_USUARIO_X_ENCUESTA] 'SELECT * FROM #tmpFinalUsuarioXEncuesta'";
            //}
            ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);

            if (!returnError)
            {
                if (ds.Tables.Count > 0)
                {
                    this.dgrid_ver_reporte.DataSource = ds;
                    this.dgrid_ver_reporte.DataBind();
                }
            }

        }
        private void cargarTipoEncuesta()
        {
            Boolean returnError = false;
            String strSQL = "";

            if (HttpContext.Current.Session["PERMISOS"].ToString() == "4")
            {
                strSQL = "SELECT DISTINCT tip_id AS 'ID', tip_nombre AS 'NOMBRE' " +
                          " FROM T_TIPO_ENCUESTA " +
                          " WHERE tip_estado = 1 AND tip_id = 5";
            }
            else 
            {
                strSQL = "SELECT DISTINCT " +
                            "TIPO.tip_id AS 'ID', " +
                            "TIPO.tip_nombre AS 'NOMBRE' " +
                            "FROM T_ENCUESTA_USUARIO USUARIO " +
                            "INNER JOIN T_TIPO_ENCUESTA TIPO ON TIPO.tip_id = USUARIO.uen_tip_id " +
                            "WHERE USUARIO.uen_estado = 1 AND TIPO.tip_estado = 1";                
            }
            
            clsSQL.llenarComboBox(ref returnError, strSQL, ref this.cmb_tipo_encuesta, "ID", "NOMBRE");
            if (!returnError)
            {
                if (this.cmb_tipo_encuesta.Items.Count > 0)
                {
                    cargarEncuesta();

                }
            }
        }
        private void cargarEncuesta()
        {
            Boolean returnError = false;
            String strSQL = "";
            if (this.cmb_tipo_encuesta.SelectedItem.Value == "1")
            {
                strSQL = "SELECT DISTINCT " +
                                "ENCUESTA.enc_id AS 'ID'," +
                                "ENCUESTA.enc_nombre AS 'NOMBRE' " +
                                "FROM T_UNIDAD_ENCUESTA UNIDAD " +
                                "INNER JOIN T_ENCUESTA_USUARIO USUARIO ON USUARIO.uen_id = UNIDAD.uni_uen_id " +
                                "INNER JOIN T_ENCUESTA ENCUESTA ON ENCUESTA.enc_id = UNIDAD.uni_enc_id " +
                                "WHERE USUARIO.uen_estado = 1 AND ENCUESTA.enc_estado= 1 AND UNIDAD.uni_estado = 1 AND USUARIO.uen_tip_id = 1";
            }
            else if (this.cmb_tipo_encuesta.SelectedItem.Value == "2")
            {
                strSQL = "SELECT DISTINCT " +
                        "ENCUESTA.enc_id AS 'ID', " +
                        "ENCUESTA.enc_nombre AS 'NOMBRE' " +
                        "FROM T_UNIDAD_ENCUESTA UNIDAD " +
                        "INNER JOIN T_ISR ISR ON ISR.isr_uni_id = UNIDAD.uni_id " +
                        "INNER JOIN T_ENCUESTA_USUARIO USUARIO ON USUARIO.uen_id = UNIDAD.uni_uen_id " +
                        "INNER JOIN T_ENCUESTA ENCUESTA ON ENCUESTA.enc_id = UNIDAD.uni_enc_id " +
                        "WHERE USUARIO.uen_estado = 1 AND ENCUESTA.enc_estado= 1 AND UNIDAD.uni_estado = 1 AND USUARIO.uen_tip_id = 2";
            }
            else if (this.cmb_tipo_encuesta.SelectedItem.Value == "3")
            {
                strSQL = "SELECT DISTINCT " +
                    "ENCUESTA.enc_id AS 'ID', " +
                    "ENCUESTA.enc_nombre AS 'NOMBRE' " +
                    "FROM T_UNIDAD_ENCUESTA UNIDAD " +
                    "INNER JOIN T_ISR ISR ON ISR.isr_uni_id = UNIDAD.uni_id " +
                    "INNER JOIN T_ENCUESTA_USUARIO USUARIO ON USUARIO.uen_id = UNIDAD.uni_uen_id " +
                    "INNER JOIN T_ENCUESTA ENCUESTA ON ENCUESTA.enc_id = UNIDAD.uni_enc_id " +
                    "WHERE USUARIO.uen_estado = 1 AND ENCUESTA.enc_estado= 1 AND UNIDAD.uni_estado = 1 AND USUARIO.uen_tip_id = 3";
            }
            else if (this.cmb_tipo_encuesta.SelectedItem.Value == "4")
            {
                strSQL = "SELECT DISTINCT " +
                    "ENCUESTA.enc_id AS 'ID', " +
                    "ENCUESTA.enc_nombre AS 'NOMBRE' " +
                    "FROM T_UNIDAD_ENCUESTA UNIDAD " +
                    "INNER JOIN T_ISR ISR ON ISR.isr_uni_id = UNIDAD.uni_id " +
                    "INNER JOIN T_ENCUESTA_USUARIO USUARIO ON USUARIO.uen_id = UNIDAD.uni_uen_id " +
                    "INNER JOIN T_ENCUESTA ENCUESTA ON ENCUESTA.enc_id = UNIDAD.uni_enc_id " +
                    "WHERE USUARIO.uen_estado = 1 AND ENCUESTA.enc_estado= 1 AND UNIDAD.uni_estado = 1 AND USUARIO.uen_tip_id = 4";
            }
            else if (this.cmb_tipo_encuesta.SelectedItem.Value == "5")
            {
                strSQL = "SELECT DISTINCT " +
                    "ENCUESTA.enc_id AS 'ID', " +
                    "ENCUESTA.enc_nombre AS 'NOMBRE' " +
                    "FROM T_UNIDAD_ENCUESTA UNIDAD " +
                    "INNER JOIN T_ENCUESTA_USUARIO USUARIO ON USUARIO.uen_id = UNIDAD.uni_uen_id " +
                    "INNER JOIN T_ENCUESTA ENCUESTA ON ENCUESTA.enc_id = UNIDAD.uni_enc_id " +
                    "WHERE USUARIO.uen_estado = 1 AND ENCUESTA.enc_estado= 1 AND UNIDAD.uni_estado = 1 AND USUARIO.uen_tip_id = 5";
            }

            clsSQL.llenarComboBox(ref returnError, strSQL, ref this.cmb_encuesta, "ID", "NOMBRE");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            cargarGrilla();
        }



        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            //if (this.pnl_filtro_usuario.Visible) {
            //    this.pnl_filtro_usuario.Visible = false;
            //}
            //if (!this.pnl_filtro_X_Encuesta.Visible) {
            //    this.pnl_filtro_X_Encuesta.Visible = true;
            //}
        }

        protected void lnk_btn_usuario_filtro_Click(object sender, EventArgs e)
        {
            //if (this.pnl_filtro_X_Encuesta.Visible)
            //{
            //    this.pnl_filtro_X_Encuesta.Visible = false;
            //}
            //if (!this.pnl_filtro_usuario.Visible)
            //{
            //    this.pnl_filtro_usuario.Visible = true;
            //}
        }

        protected void dgrid_ver_reporte_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count >= 17)
            {
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[7].Visible = false;
                e.Row.Cells[9].Visible = false;
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[15].Visible = false;
                e.Row.Cells[14].Width = 100;
                e.Row.Cells[16].Width = 100;
                e.Row.Cells[17].Width = 100;
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[14].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[17].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[16].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[4].Visible = false;
                if (e.Row.Cells[17].Text == "Respuesta")
                {
                    e.Row.Cells[0].Visible = false;
                    e.Row.Cells[1].Visible = true;

                }
                else
                {

                    e.Row.Cells[0].Visible = true;
                    e.Row.Cells[1].Visible = false;
                }

            }
        }

        protected void cmb_tipo_encuesta_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarEncuesta();
        }

        protected void dgrid_ver_reporte_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Select":
                    enviarCorreoEspecifico(Convert.ToInt32(e.CommandArgument));
                    break;
                case "VerRespuesta":
                    abrirRespuestaEspecifica(Convert.ToInt32(e.CommandArgument));
                    break;
            }
        }
        private void enviarCorreoEspecifico(int numFila)
        {
            String correoOrigen = "osaenz@almacontact.com.co";
            String[] destinario = new String[] { this.dgrid_ver_reporte.Rows[numFila].Cells[4].Text };
            //String[] destinario = new String[] { "nestor-alfonso.sierra.mosos@hp.com"}; 
            String login = this.dgrid_ver_reporte.Rows[numFila].Cells[12].Text;
            String contrasenia = this.dgrid_ver_reporte.Rows[numFila].Cells[13].Text;
            String nombreUsuario = this.dgrid_ver_reporte.Rows[numFila].Cells[3].Text;
            String encuesta = this.dgrid_ver_reporte.Rows[numFila].Cells[8].Text;

            //String html = "<div style='padding:15px;'>" +
            //              "<p style='margin:10px 0px 10px 10px;font-family:HP Simplified;font-size:14px'><strong>Usuario: </strong> " + nombreUsuario + "</p>" +
            //              "<p style='margin:0px 0px 20px 10px;font-family:HP Simplified;font-size:14px;color:#585964'><strong>Para el SCC </strong> saber su opinión es una prioridad para  conocer qué piensa sobre su relación comercial y/o el servicio prestado y de esta manera retroalimentar la gestión interna. Le agradecemos su objetividad y disposición para contestar las siguientes encuestas (siga este <a href='http://bog01nmcahp/NMCASCC/SURVEYTOOLSHP/'>link</a>)</p>" +
            //              "<p style='margin:0px 0px 10px 50px;font-size:16px;color:#214023'><strong>Recuerde sus datos:</strong></p>" +
            //              "<p style='margin:0px 0px 10px 50px;font-size:16px;color:#214023'><strong>Usuario : </strong><a style='color:#585964'>" + login + "</a></p>" +
            //              "<p style='margin:0px 0px 10px 50px;font-size:16px;color:#214023'><strong>Contraseña : </strong><a style='color:#585964'>" + contrasenia + "</a></p></div>";
            //html = crearRetornarCuerpoCorreo("RECORDATORIO DE ENVIÓ DE RESPUESTA ", html, "SCC HP", "");

            String html = "<div style='padding:15px;'>" +
                         "<p style='margin:10px 0px 10px 10px;font-family:HP Simplified;font-size:14px'><strong>Usuario: </strong> " + nombreUsuario + "</p>" +
                         "<p style='margin:0px 0px 20px 10px;font-family:HP Simplified;font-size:14px;color:#585964'><strong>Para el SCC </strong> saber su opinión es una prioridad con el fin de tener una percepción de su relación comercial y/o el servicio prestado, de esta manera retroalimentar la gestión interna. Le agradecemos su objetividad y disposición para contestar las siguientes encuestas (siga este <a href='http://bog01nmcahp/NMCASCC/SURVEYTOOLSHP/'>link</a>) </p>" +
                         "<p style='margin:0px 0px 20px 10px;font-family:HP Simplified;font-size:14px;color:#585964'>Si quiere conocer más acerca de este procedimiento lo invitamos a visitar el siguiente <a href='https://sway.com/Q1l1UdR_TpZuTtMx'>link</a></p>" +
                         "<p style='margin:0px 0px 20px 10px;font-family:HP Simplified;font-size:14px;color:#585964'>Si presenta algún problema al ingresar a la encuesta pude ingresar en el siguiente <a href='https://sway.com/3UB1GZdF8Uc8Xwz2'>link</a></p>" +
                         "<p style='margin:0px 0px 10px 50px;font-size:16px;color:#214023'><strong>Recuerde sus datos:</strong></p>" +
                          "<p style='margin:0px 0px 10px 50px;font-size:16px;color:#214023'><strong>Usuario : </strong><a style='color:#585964'>" + login + "</a></p>" +
                          "<p style='margin:0px 0px 10px 50px;font-size:16px;color:#214023'><strong>Contraseña : </strong><a style='color:#585964'>" + contrasenia + "</a></p></div>";
            html = crearRetornarCuerpoCorreo("RECORDATORIO DE ENVIÓ DE RESPUESTA ", html, "SCC HP", "");

            email.EnviarEmail(email.darDestinatarios(destinario), correoOrigen, "SURVEYTOOLSHP", "ENCUESTAS TCE SCC **RECORDATORIO DE ENVIÓ DE RESPUESTA**", html);
            String strSQL = "UPDATE T_ENCUESTA_USUARIO SET uen_envioCorreo = 2 , uen_fechaEnvioCorreo = GETDATE() WHERE uen_estado = 1 AND uen_usu_id  = " + this.dgrid_ver_reporte.Rows[numFila].Cells[3].Text;
            Boolean returnError = false;
            clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError)
            {
                Message("ENVIO DE SOLICITUD", "Petición enviada al usuario " + nombreUsuario, TipoMensaje.Validacion, true);
            }
        }

        private void abrirRespuestaEspecifica(int prmNumFila)
        {
            String idUsuario = this.dgrid_ver_reporte.Rows[prmNumFila].Cells[2].Text;
            String idEncuesta = this.dgrid_ver_reporte.Rows[prmNumFila].Cells[7].Text;
            String evaluado = this.dgrid_ver_reporte.Rows[prmNumFila].Cells[10].Text;
            String nomEscuesta = this.dgrid_ver_reporte.Rows[prmNumFila].Cells[8].Text;
            String nomUsuario = this.dgrid_ver_reporte.Rows[prmNumFila].Cells[3].Text;
            HttpContext.Current.Session["ENCUESTA_ID_USUARIO"] = idUsuario;
            HttpContext.Current.Session["ENCUESTA_ID_ENCUESTA"] = idEncuesta;
            HttpContext.Current.Session["ENCUESTA_EVALUADO"] = evaluado;
            HttpContext.Current.Session["ENCUESTA_NOM_ENCUESTA"] = nomEscuesta;
            HttpContext.Current.Session["ENCUESTA_NOM_USUARIO"] = nomUsuario;


            Response.Redirect("~/Content/MenuPrincipal/AdministradorRespuestas/RespuestaEspecifica.aspx");


        }
        protected void dgrid_ver_reporte_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.dgrid_ver_reporte.PageIndex = e.NewPageIndex;
            cargarGrilla();
        }
    }
}