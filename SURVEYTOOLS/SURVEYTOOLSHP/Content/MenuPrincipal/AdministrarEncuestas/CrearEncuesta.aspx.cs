using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SURVEYTOOLSHP.Model;

namespace SURVEYTOOLSHP.Content.MenuPrincipal.AdministrarEncuestas
{
    public partial class CrearEncuesta : BasePage
    {
        private String idEncuesta = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cargarGrilla();
                cargarTipoDatos();

                cargarSeleccion();
            }

        }
        private void cargarGrilla()
        {
            String strSQL = "SELECT enc_id AS 'ID',enc_nombre AS 'Nombre',(CASE WHEN enc_estado = 1  THEN 'ACTIVO' ELSE 'DESACTIVO' END) AS 'Estado' FROM T_ENCUESTA";
            Boolean returnError = false;
            DataSet ds = new DataSet();
            ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError)
            {
                if (ds.Tables.Count > 0)
                {
                    this.dgrid_Encuestas.DataSource = ds;
                    this.dgrid_Encuestas.DataBind();
                }
            }
        }

        private void cargarSeleccion()
        {
            this.rdl_list_copiar_Preguntas.Items.FindByValue("0").Selected = true;
        }

        private void cargarEncuesta()
        {
            String strSQL = "SELECT enc_id AS 'ID',enc_nombre AS 'NOMBRE' FROM T_ENCUESTA WHERE enc_tip_id = " + this.cmb_tipo_encuesta.SelectedItem.Value;
            Boolean returnError = false;
            clsSQL.llenarComboBoxConSelect(ref returnError, strSQL, ref this.cmb_encestas_seleccion, "ID", "NOMBRE");
        }

        private void cargarTipoDatos()
        {
            String strSQL = "SELECT tip_id AS 'ID',tip_nombre AS 'NOMBRE' FROM T_TIPO_ENCUESTA WHERE tip_estado = 1";
            Boolean returnError = false;
            clsSQL.llenarComboBox(ref returnError, strSQL, ref this.cmb_tipo_encuesta, "ID", "NOMBRE");
            clsSQL.llenarComboBox(ref returnError, strSQL, ref this.cmb_Tipo_encuesta_editar, "ID", "NOMBRE");

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (this.txt_nombre_encuesta.Text.Length == 0)
            {
                Message("ERROR FALTA", "Falto digitar el texto", TipoMensaje.Information, true);
            }
            else
            {
                Boolean returnError = false;

                String strSQL = "INSERT INTO T_ENCUESTA (enc_nombre,enc_fechaCreado,enc_estado,enc_tip_id) VALUES ('" + this.txt_nombre_encuesta.Text.ToUpper() + "',GETDATE(),1," + this.cmb_tipo_encuesta.SelectedItem.Value + ")";
                clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
                if (!returnError)
                {
                    strSQL = "SELECT MAX(enc_id) FROM T_ENCUESTA WHERE enc_estado = 1";
                    int maxEncuesta = Convert.ToInt32(clsSQL.retornarDatoEscalar(ref returnError, strSQL));
                    strSQL = "UPDATE T_ENCUESTA SET enc_url='~/Content/MenuPrincipal/Encuesta/Encuesta.aspx?idEncuesta=" + maxEncuesta + "' WHERE enc_id = " + maxEncuesta;
                    clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);

                    if (!returnError)
                    {

                        strSQL = "EXEC SP_MM_INSERTAR_NUEVA_ENCUESTA '" + this.txt_nombre_encuesta.Text + "'," + maxEncuesta;
                        clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
                        if (!returnError)
                        {
                            Message("CRERACIÓN ENCUESTA", "La encuesta fue creada", TipoMensaje.Validacion, true);
                            if (this.rdl_list_copiar_Preguntas.SelectedItem.Value == "1")
                            {
                                if (this.cmb_encestas_seleccion.SelectedItem.Value != "0")
                                {
                                    strSQL = "EXEC SP_MM_COPIAR_ENCUESTA " + maxEncuesta + "," + this.cmb_encestas_seleccion.SelectedItem.Value;
                                    clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
                                    Response.Redirect("~/Content/home.aspx");
                                }
                                else
                                    Message("CREACION DE ENCUESTA", "Tiene que seleccionar la encuesta a la cual se le van a copiar sus preguntas", TipoMensaje.Information, true);
                            }
                            else
                            {
                                String url = "~/Content/MenuPrincipal/AdministrarEncuestas/Encuesta.aspx?idEncuesta=" + maxEncuesta;

                                Response.Redirect(url);
                            }
                        }

                    }
                }


            }
        }

        protected void dgrid_Encuestas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count >= 5)
            {
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        protected void dgrid_Encuestas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToString())
            {
                case "Activar":
                    Activar(Convert.ToInt32(e.CommandArgument));
                    break;
                case "Desactivar":
                    Desactivar(Convert.ToInt32(e.CommandArgument));
                    break;
                case "Editar":
                    Editar(Convert.ToInt32(e.CommandArgument));
                    break;
            }
        }
        private void Editar(int prmActualFila)
        {
            idEncuesta = this.dgrid_Encuestas.Rows[prmActualFila].Cells[3].Text.ToString();
            String strSQL = "SELECT enc_nombre AS 'NOMBRE'  FROM T_ENCUESTA WHERE enc_id = " + idEncuesta;
            Boolean returnError = false;
            DataSet ds = new DataSet();
            ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError)
            {
                if (ds.Tables[0].Rows.Count == 1)
                {
                    this.pnl_EditarEncuesta.Visible = true;
                    foreach (DataRow dRow in ds.Tables[0].Rows)
                    {
                        this.txt_nombre_editar_encuesta.Text = dRow["NOMBRE"].ToString();
                    }


                }
            }
        }
        private void Activar(int prmActualFila)
        {
            String idEcuesta = this.dgrid_Encuestas.Rows[prmActualFila].Cells[3].Text.ToString();
            String strSQL = "UPDATE T_ENCUESTA SET enc_estado = 1 WHERE enc_id = " + idEcuesta;
            Boolean returnError = false;
            clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError)
            {
                strSQL = "UPDATE T_PAGINA SET pag_estado = 1 WHERE pag_enc_id  = " + idEcuesta;
                clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
                if (!returnError)
                {
                    cargarGrilla();
                }
            }
        }
        private void Desactivar(int prmActualFila)
        {
            String idEcuesta = this.dgrid_Encuestas.Rows[prmActualFila].Cells[3].Text.ToString();
            String strSQL = "UPDATE T_ENCUESTA SET enc_estado = 0 WHERE enc_id = " + idEcuesta;
            Boolean returnError = false;
            clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError)
            {
                cargarGrilla();
            }
        }



        protected void btn_enviar_cambio_Click(object sender, EventArgs e)
        {
            cambiarEncuesta();
        }
        private void cambiarEncuesta()
        {
            Boolean returnError = false;
            String strSQL = "UPDATE T_ENCUESTA SET enc_estado = " + cmb_estado_encuesta.SelectedItem.Value + ",SET enc_nombre = '" + this.txt_nombre_editar_encuesta.Text + "', enc_tip_id = " + this.cmb_Tipo_encuesta_editar.SelectedItem.Value + " WHERE enc_id = " + idEncuesta;
            clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError)
            {
                cargarGrilla();
            }
        }

        protected void btn_ocultar_Click(object sender, EventArgs e)
        {
            this.pnl_EditarEncuesta.Visible = false;
        }

        protected void img_btn_exportar_excel_Click(object sender, ImageClickEventArgs e)
        {
            exportarGridView_Excel(this.dgrid_Encuestas, "Encuestas", 2);
        }

        protected void rdl_list_copiar_Preguntas_SelectedIndexChanged(object sender, EventArgs e)
        {
            String opc = this.rdl_list_copiar_Preguntas.SelectedItem.Value;
            switch (opc)
            {
                case "1":
                    cargarEncuesta();

                    this.pnl_otra_encuestas.Visible = true;
                    break;
                case "0":
                    this.pnl_otra_encuestas.Visible = false;
                    break;
            }
        }

        protected void cmb_tipo_encuesta_SelectedIndexChanged(object sender, EventArgs e)
        {

            cargarEncuesta();
        }

        protected void dgrid_Encuestas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cmb_tipo_encuesta_SelectedIndexChanged1(object sender, EventArgs e)
        {
            cargarEncuesta();
        }


    }
}