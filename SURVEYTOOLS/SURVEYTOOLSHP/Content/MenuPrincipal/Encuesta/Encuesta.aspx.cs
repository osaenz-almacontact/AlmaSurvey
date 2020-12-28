using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SURVEYTOOLSHP.Model;
using System.Web.Security;

namespace SURVEYTOOLSHP.Content.MenuPrincipal.Encuesta
{
    public partial class Encuesta : BasePage
    {
        private clsEmail email;
        private String numEncuesta = "";
        private String numFase = "";
        private String mensajeError = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.email = new clsEmail();
            this.numEncuesta = Request.QueryString["idEncuesta"];
            this.numFase = Request.QueryString["idFase"].ToString();
            cargarFase();
            cargarSite();
            cargarOperacion();
            cargarServicio();
            cargarEvaluado();
            cargarTabla();
            if (!Page.IsPostBack)
            {
                cargarNombreEncuesta();
            }

        }
        private void cargarEvaluado()
        {
            String strSQL = "SELECT enc_tip_id FROM T_ENCUESTA WHERE enc_id = " + numEncuesta;
            Boolean returnError = false;
            int tipoEncuesta = Convert.ToInt32(clsSQL.retornarDatoEscalar(ref returnError, strSQL));
            Session["idTipoEncuesta"] = tipoEncuesta;
            switch (tipoEncuesta)
            {
                case 1:
                    cargarComboEvaluado(1, numEncuesta);
                    DivMensajeStandar.Visible = true;
                    DivMensajeLiderazgo.Visible = false;
                    DivMensajeAutoevaluacion.Visible = false;
                    DivMensajeClima.Visible = false;
                    DivMensajeRetiro.Visible = false;
                    break;
                case 2:
                    cargarComboEvaluado(2, numEncuesta);
                    DivMensajeStandar.Visible = true;
                    DivMensajeLiderazgo.Visible = false;
                    DivMensajeAutoevaluacion.Visible = false;
                    DivMensajeClima.Visible = false;
                    DivMensajeRetiro.Visible = false;
                    break;
                case 3:
                    cargarComboEvaluado(3, numEncuesta);
                    DivMensajeStandar.Visible = false;
                    DivMensajeLiderazgo.Visible = true;
                    DivMensajeAutoevaluacion.Visible = false;
                    DivMensajeClima.Visible = false;
                    DivMensajeRetiro.Visible = false;
                    break;
                case 4:
                    cargarComboEvaluado(4, numEncuesta);
                    DivMensajeStandar.Visible = false;
                    DivMensajeLiderazgo.Visible = false;
                    DivMensajeAutoevaluacion.Visible = false;
                    DivMensajeClima.Visible = true;
                    DivMensajeRetiro.Visible = false;
                    cmb_cagar_combo_evaluado.Visible = false;
                    lbl_evaluado.Visible = false;
                    break;
                case 5:
                    //cargarComboEvaluado(5, numEncuesta);
                    DivMensajeStandar.Visible = false;
                    DivMensajeLiderazgo.Visible = false;
                    DivMensajeAutoevaluacion.Visible = false;
                    DivMensajeClima.Visible = false;
                    DivMensajeRetiro.Visible = true;
                    cmb_cagar_combo_evaluado.Visible = false;
                    break;

            }

        }

        private void cargarComboEvaluado(int prmOpc, String prmIdEncuesta)
        {
            String strSQL = "";
            Boolean returnError = false;
            switch (prmOpc)
            {
                case 1:
                    strSQL = "SELECT NOM_ENCUESTA.enc_id AS 'ID' ," +
                            "NOM_ENCUESTA.enc_nombre as 'NOMBRE' " +
                            "FROM T_ENCUESTA_USUARIO USUARIO " +
                            "INNER JOIN T_UNIDAD_ENCUESTA ENCUESTA ON ENCUESTA.uni_uen_id = USUARIO.uen_id " +
                            "INNER JOIN T_ENCUESTA NOM_ENCUESTA ON NOM_ENCUESTA.enc_id = ENCUESTA.uni_enc_id " +
                            "WHERE USUARIO.uen_usu_id = " + HttpContext.Current.Session["ID_USER"].ToString() + " AND ENCUESTA.uni_estado = 1 AND USUARIO.uen_estado = 1  AND ENCUESTA.uni_enc_id = " + prmIdEncuesta + " AND ENCUESTA.uni_estado_encuesta = 1 AND NOM_ENCUESTA.enc_estado = 1 AND USUARIO.uen_fas_id = " + numFase;
                    clsSQL.llenarComboBox(ref returnError, strSQL, ref this.cmb_cagar_combo_evaluado, "ID", "NOMBRE");
                    this.lbl_evaluado.Text = "Evaluar a: ";
                    if (this.cmb_cagar_combo_evaluado.Items.Count == 0)
                    {
                        Message("Encuesta", "Ya finalizó con el proceso de evaluación", TipoMensaje.Information, true);
                        Response.Redirect("~/Content/Home.aspx");
                    }
                    break;

                case 2:
                    strSQL = "SELECT DISTINCT " +
                    "ISR.isr_id AS 'ID'," +
                    "ISR.isr_nombre AS 'NOMBRE' " +
                    "FROM T_ISR ISR " +
                    "INNER JOIN T_UNIDAD_ENCUESTA ENCUESTA ON ENCUESTA.uni_id  = ISR.isr_uni_id " +
                    "INNER JOIN T_ENCUESTA_USUARIO USUARIO ON USUARIO.uen_id = ENCUESTA.uni_uen_id " +
                    "WHERE ISR.isr_estado  = 1 " +
                    "AND ENCUESTA.uni_estado = 1 " +
                    "AND USUARIO.uen_estado = 1 " +
                    "AND ISR.isr_est_encuesta = 1 " +
                    "AND USUARIO.uen_usu_id = " + HttpContext.Current.Session["ID_USER"].ToString() +
                    "AND ENCUESTA.uni_enc_id = " + prmIdEncuesta + " " +
                    "AND USUARIO.uen_fas_id = " + numFase;
                    clsSQL.llenarComboBox(ref returnError, strSQL, ref this.cmb_cagar_combo_evaluado, "ID", "NOMBRE");
                    this.lbl_evaluado.Text = "Evaluar a: ";
                    if (this.cmb_cagar_combo_evaluado.Items.Count == 0)
                    {
                        Response.Redirect("~/Content/Home.aspx");
                    }
                    break;

                case 3:
                    strSQL = "SELECT DISTINCT " +
                    "ISR.isr_id AS 'ID'," +
                    "ISR.isr_nombre AS 'NOMBRE' " +
                    "FROM T_ISR ISR " +
                    "INNER JOIN T_UNIDAD_ENCUESTA ENCUESTA ON ENCUESTA.uni_id  = ISR.isr_uni_id " +
                    "INNER JOIN T_ENCUESTA_USUARIO USUARIO ON USUARIO.uen_id = ENCUESTA.uni_uen_id " +
                    "WHERE ISR.isr_estado  = 1 " +
                    "AND ENCUESTA.uni_estado = 1 " +
                    "AND USUARIO.uen_estado = 1 " +
                    "AND ISR.isr_est_encuesta = 1 " +
                    "AND USUARIO.uen_usu_id = " + HttpContext.Current.Session["ID_USER"].ToString() +
                    "AND ENCUESTA.uni_enc_id = " + prmIdEncuesta + " " +
                    "AND USUARIO.uen_fas_id = " + numFase;
                    clsSQL.llenarComboBox(ref returnError, strSQL, ref this.cmb_cagar_combo_evaluado, "ID", "NOMBRE");
                    this.lbl_evaluado.Text = "Evaluar a: ";
                    if (this.cmb_cagar_combo_evaluado.Items.Count == 0)
                    {
                        Response.Redirect("~/Content/Home.aspx");
                    }
                    break;

                case 4:
                    strSQL = "SELECT DISTINCT " +
                    "ISR.isr_id AS 'ID'," +
                    "ISR.isr_nombre AS 'NOMBRE' " +
                    "FROM T_ISR ISR " +
                    "INNER JOIN T_UNIDAD_ENCUESTA ENCUESTA ON ENCUESTA.uni_id  = ISR.isr_uni_id " +
                    "INNER JOIN T_ENCUESTA_USUARIO USUARIO ON USUARIO.uen_id = ENCUESTA.uni_uen_id " +
                    "WHERE ISR.isr_estado  = 1 " +
                    "AND ENCUESTA.uni_estado = 1 " +
                    "AND USUARIO.uen_estado = 1 " +
                    "AND ISR.isr_est_encuesta = 1 " +
                    "AND USUARIO.uen_usu_id = " + HttpContext.Current.Session["ID_USER"].ToString() +
                    "AND ENCUESTA.uni_enc_id = " + prmIdEncuesta + " " +
                    "AND USUARIO.uen_fas_id = " + numFase;
                    clsSQL.llenarComboBox(ref returnError, strSQL, ref this.cmb_cagar_combo_evaluado, "ID", "NOMBRE");
                    this.lbl_evaluado.Text = "Lider de area: ";
                    if (this.cmb_cagar_combo_evaluado.Items.Count == 0)
                    {
                        Response.Redirect("~/Content/Home.aspx");
                    }
                    break;
            }
        }
        private void cargarFase()
        {
            if (!IsPostBack)
            {
                String strSQL = "SELECT fas_id AS 'ID', fas_nombre AS 'NOMBRE' FROM T_FASE WHERE fas_id = " + numFase;
                Boolean returnError = false;
                clsSQL.llenarComboBox(ref returnError, strSQL, ref this.cmb_fase, "ID", "NOMBRE");
            }

        }

        private void cargarSite()
        {
            if (!IsPostBack)
            {
                String strSQL = "SELECT site_id AS 'ID', site_nombre AS 'NOMBRE' FROM T_SITE";
                Boolean returnError = false;
                clsSQL.llenarComboBox(ref returnError, strSQL, ref this.cmb_site, "ID", "NOMBRE");
                this.cmb_site.Items.Insert(0, new ListItem("(Seleccionar)", "0"));
            }
        }

        private void cargarOperacion()
        {
            if (!IsPostBack)
            {
                String strSQL = "SELECT opr_id AS 'ID', opr_nombre AS 'NOMBRE' FROM T_OPERACION";
                Boolean returnError = false;
                clsSQL.llenarComboBox(ref returnError, strSQL, ref this.cmb_operacion, "ID", "NOMBRE");
                this.cmb_operacion.Items.Insert(0, new ListItem("(Seleccionar)", "0"));
            }
        }
        private void cargarServicio()
        {
            //String strSQL = "SELECT area_id AS 'ID', area_nombre AS 'NOMBRE' FROM T_AREA";
            //Boolean returnError = false;
            //clsSQL.llenarComboBox(ref returnError, strSQL, ref this.cmb_servicio, "ID", "NOMBRE");
            this.cmb_servicio.Items.Insert(0, new ListItem("(Seleccionar)", "0"));
        }
        private void cargarNombreEncuesta()
        {

            String strSQL = "SELECT enc_nombre FROM T_ENCUESTA WHERE enc_id = " + numEncuesta;
            Boolean returnError = false;
            String nomEncuesta = clsSQL.retornarDatoEscalar(ref returnError, strSQL).ToString();
            if (!returnError)
                this.lbl_nombre_encuesta.Text = nomEncuesta;

        }
        private void cargarTabla()
        {
            Boolean returnError = false;
            int contador = 1;
            String strSQL = "SELECT pre_id AS 'ID',pre_descripcion AS 'NOMBRE',pre_tip_id AS 'TIPO' FROM T_PREGUNTA WHERE pre_enc_id = " + numEncuesta + " AND pre_estado = 1 ";
            DataSet ds = new DataSet();
            int cont = 1;

            ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError)
            {
                if (ds.Tables.Count > 0)
                {
                    this.Table1.Rows.Clear();
                    foreach (DataRow dRow in ds.Tables[0].Rows)
                    {
                        if (dRow["TIPO"].ToString() == "5")
                        {
                            TableRow fila = new TableRow();
                            TableCell celda1 = new TableCell();

                            Label lblNombrePregunta = new Label();
                            lblNombrePregunta.Text = dRow["NOMBRE"].ToString();
                            lblNombrePregunta.ID = "lbl_" + cont;
                            lblNombrePregunta.Font.Size = 18;
                            lblNombrePregunta.Font.Bold = true;
                            lblNombrePregunta.ForeColor = System.Drawing.Color.FromName("#214023");
                            lblNombrePregunta.CssClass = "css_fuente_letra css_bold";
                            celda1.Controls.Add(lblNombrePregunta);
                            celda1.ColumnSpan = 2;
                            celda1.HorizontalAlign = HorizontalAlign.Left;
                            fila.Cells.Add(celda1);

                            TableRow row1 = new TableRow();
                            TableCell cell1 = new TableCell();
                            Label lblEspacio = new Label();
                            lblEspacio.Text = "espacio";
                            lblEspacio.ForeColor = System.Drawing.Color.White;

                            cell1.Controls.Add(lblEspacio);
                            row1.Cells.Add(cell1);
                            row1.Cells.Add(cell1);
                            this.Table1.Rows.Add(fila);
                            this.Table1.Rows.Add(row1);
                            cont++;
                        }
                        else
                        {
                            TableRow fila = new TableRow();
                            TableCell celda1 = new TableCell();
                            TableCell celda2 = new TableCell();
                            Label lblNombrePregunta = new Label();
                            lblNombrePregunta.ID = "lblP_" + cont;
                            lblNombrePregunta.Text = contador + "." + dRow["NOMBRE"].ToString();
                            lblNombrePregunta.CssClass = "css_fuente_letra css_bold";
                            celda1.Controls.Add(lblNombrePregunta);


                            if (dRow["TIPO"].ToString() == "1")
                            {
                                TextBox txtArea = new TextBox();
                                txtArea.TextMode = TextBoxMode.MultiLine;
                                txtArea.CssClass = "form-control";
                                txtArea.ID = "txt_" + cont;
                                celda2.Controls.Add(txtArea);
                                contador++;
                            }
                            else if (dRow["TIPO"].ToString() == "2")
                            {
                                DropDownList lista = new DropDownList();
                                lista.Width = 400;
                                cargarCombo(ref lista, dRow["ID"].ToString());
                                lista.CssClass = "form-control";
                                lista.ID = "cmb_" + cont;
                                celda2.Controls.Add(lista);
                                contador++;
                            }
                            else if (dRow["TIPO"].ToString() == "3")
                            {
                                RadioButtonList lista = new RadioButtonList();
                                lista.Width = 400;
                                cargarRadioButton(ref lista, dRow["ID"].ToString());
                                lista.CssClass = "css_fuente_letra";
                                lista.ID = "rd_btn_" + cont;
                                celda2.Controls.Add(lista);
                                contador++;
                            }
                            else if (dRow["TIPO"].ToString() == "4")
                            {
                                TextBox txtArea = new TextBox();
                                txtArea.TextMode = TextBoxMode.MultiLine;
                                txtArea.Width = 400;
                                txtArea.Height = 100;
                                txtArea.CssClass = "form-control";
                                txtArea.ID = "txt_area_" + cont;
                                celda2.Controls.Add(txtArea);
                                contador++;
                            }
                            celda1.HorizontalAlign = HorizontalAlign.Left;
                            celda1.CssClass = "css_tamanio_widht_50 css_padmin_top css_border_bottom_simple";

                            celda2.HorizontalAlign = HorizontalAlign.Left;
                            celda2.CssClass = "css_tamanio_widht_50 css_padmin_top css_border_bottom_simple";

                            fila.Cells.Add(celda1);
                            fila.Cells.Add(celda2);


                            TableRow row1 = new TableRow();
                            TableCell cell1 = new TableCell();
                            Label lblEspacio = new Label();
                            lblEspacio.Text = "espacio";
                            lblEspacio.ForeColor = System.Drawing.Color.White;

                            cell1.Controls.Add(lblEspacio);
                            row1.Cells.Add(cell1);
                            row1.Cells.Add(cell1);
                            cont++;
                            this.Table1.Rows.Add(fila);
                            this.Table1.Rows.Add(row1);
                        }
                    }
                }
            }

        }

        private void cargarCombo(ref DropDownList prmlista, String prmIDpre)
        {
            Boolean returnError = false;
            String strComboSQL = "SELECT res_id AS 'ID', res_nombre  AS 'NOMBRE' FROM T_RESPUESTA WHERE res_pre_id = " + prmIDpre + " AND  res_estado= 1 ";
            clsSQL.llenarComboBox(ref returnError, strComboSQL, ref prmlista, "ID", "NOMBRE");
            ListItem itemSelected = new ListItem();
            itemSelected.Value = "0";
            itemSelected.Text = "Seleccione una opción";
            prmlista.Items.Add(itemSelected);
            prmlista.SelectedIndex = prmlista.Items.Count - 1;
        }
        private void cargarRadioButton(ref RadioButtonList prmlista, String prmIDpre)
        {
            Boolean returnError = false;
            String strComboSQL = "SELECT res_id AS 'ID', res_nombre  AS 'NOMBRE' FROM T_RESPUESTA WHERE res_pre_id = " + prmIDpre + " AND  res_estado= 1 ";
            clsSQL.llenarRadioButtonList(ref returnError, strComboSQL, ref prmlista, "ID", "NOMBRE");
        }
        private Boolean verificarRespuestas()
        {
            Boolean returnError = false;
            int contador = 0;
            int Auxcontador = 1;
            String strSQL = "SELECT (CASE WHEN MAX(rep_num_encuesta) IS NULL THEN 0 ELSE MAX(rep_num_encuesta)END)AS 'NUMERO ENCUESTA' FROM T_PREGUNTA_RESPUESTA WHERE rep_estado = 1";
            DataSet ds = new DataSet();
            int numeroEncuesta = Convert.ToInt32(clsSQL.retornarDatoEscalar(ref returnError, strSQL)) + 1;
            strSQL = "SELECT pre_id AS 'ID',pre_descripcion AS 'DESCRIPCION',pre_tip_id AS 'TIPO' FROM T_PREGUNTA WHERE pre_enc_id  = " + numEncuesta + "  AND pre_estado =  1";
            ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError)
            {
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow dRow in ds.Tables[0].Rows)
                    {
                        if (dRow["TIPO"].ToString() == "1")
                        {
                            TextBox txt = (TextBox)this.Table1.Rows[contador].Cells[1].FindControl("txt_" + Auxcontador);
                            if (txt.Text.Length == 0)
                            {
                                Label lbl = (Label)this.Table1.Rows[contador].Cells[0].FindControl("lblP_" + Auxcontador);
                                lbl.ForeColor = System.Drawing.Color.FromName("#C10C0C");
                                mensajeError = mensajeError + "Falto contestar la pregunta: " + dRow["DESCRIPCION"].ToString() + "\n";

                            }


                        }


                        else if (dRow["TIPO"].ToString() == "2")
                        {
                            DropDownList cmb = (DropDownList)this.Table1.Rows[contador].Cells[1].FindControl("cmb_" + Auxcontador);

                            if (cmb.SelectedItem.Value.ToString() == "0")
                            {
                                Label lbl = (Label)this.Table1.Rows[contador].Cells[0].FindControl("lblP_" + Auxcontador);
                                lbl.ForeColor = System.Drawing.Color.FromName("#C10C0C");
                                mensajeError = mensajeError + "Falto contestar la pregunta: " + dRow["DESCRIPCION"].ToString() + "\n";
                            }
                        }
                        else if (dRow["TIPO"].ToString() == "3")
                        {
                            RadioButtonList cmb = (RadioButtonList)this.Table1.Rows[contador].Cells[1].FindControl("rd_btn_" + Auxcontador);
                            if (cmb.SelectedItem == null)
                            {
                                Label lbl = (Label)this.Table1.Rows[contador].Cells[0].FindControl("lblP_" + Auxcontador);
                                lbl.ForeColor = System.Drawing.Color.FromName("#C10C0C");
                                mensajeError = mensajeError + "Falto contestar la pregunta: " + dRow["DESCRIPCION"].ToString() + "\n";
                            }
                        }
                        else if (dRow["TIPO"].ToString() == "4")
                        {
                            TextBox txtArea = (TextBox)this.Table1.Rows[contador].Cells[1].FindControl("txt_area_" + Auxcontador);
                            txtArea.TextMode = TextBoxMode.MultiLine;


                            if (txtArea.Text.Length == 0)
                            {
                                Label lbl = (Label)this.Table1.Rows[contador].Cells[0].FindControl("lblP_" + Auxcontador);
                                lbl.ForeColor = System.Drawing.Color.FromName("#C10C0C");
                                mensajeError = mensajeError + "Falto contestar la pregunta: " + dRow["DESCRIPCION"].ToString() + "\n";

                            }
                        }
                        contador = contador + 2;
                        Auxcontador++;

                    }

                }
            }
            if (mensajeError != String.Empty)
            {
                return false;
            }
            return true;
        }
        protected void btnEnviarFormulario_Click(object sender, EventArgs e)
        {
            if (cmb_servicio.SelectedValue != "0")
            {
                if (verificarRespuestas())
                {

                    Boolean returnError = false;
                    int contador = 0;
                    int Auxcontador = 1;
                    String strSQL = "SELECT fas_id FROM T_FASE WHERE fas_fechaComienzo > '" + DateTime.Today.ToString("yyyy/MM/dd") + "' AND fas_fechaFinalizacion >= " + DateTime.Today.ToString("yyyy/MM/dd");

                    DataSet ds = new DataSet();
                    String numeroEncuesta = this.cmb_fase.SelectedItem.Value;
                    strSQL = "SELECT pre_id AS 'ID',pre_descripcion AS 'DESCRIPCION',pre_tip_id AS 'TIPO' FROM T_PREGUNTA WHERE pre_enc_id  = " + numEncuesta + "  AND pre_estado =  1";
                    ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
                    int tipoEncuesta;
                    tipoEncuesta = Convert.ToInt32(Session["idTipoEncuesta"]);
                    if (!returnError)
                    {

                        if (ds.Tables.Count > 0)
                        {

                            if (tipoEncuesta == 3 || tipoEncuesta == 4)
                            {
                                foreach (DataRow dRow in ds.Tables[0].Rows)
                                {
                                    if (dRow["TIPO"].ToString() == "5")
                                        strSQL = "ENCABEZADO";

                                    else
                                    {
                                        if (dRow["TIPO"].ToString() == "1")
                                        {
                                            TextBox txt = (TextBox)this.Table1.Rows[contador].Cells[1].FindControl("txt_" + Auxcontador);

                                            strSQL = "INSERT INTO T_PREGUNTA_RESPUESTA (rep_pregunta,rep_respuesta,rep_respondidoXUsu,rep_fechaCreacion,rep_enc_id,rep_estado,rep_num_encuesta,rep_evaluado,rep_fas_id,rep_site,rep_operacion,rep_servicio)" +
                                                "VALUES('" + dRow["DESCRIPCION"].ToString() + "','" + txt.Text + "','" + HttpContext.Current.Session["ID_USER"] + "',GETDATE()," + numEncuesta + ",1," + numeroEncuesta + ",'" + this.cmb_cagar_combo_evaluado.SelectedItem.Text + "'," + numeroEncuesta + "," + this.cmb_site.SelectedValue + "," + this.cmb_operacion.SelectedValue + "," + cmb_servicio.SelectedValue + ")";
                                        }
                                        else if (dRow["TIPO"].ToString() == "2")
                                        {
                                            DropDownList cmb = (DropDownList)this.Table1.Rows[contador].Cells[1].FindControl("cmb_" + Auxcontador);

                                            strSQL = "INSERT INTO T_PREGUNTA_RESPUESTA (rep_pregunta,rep_respuesta,rep_respondidoXUsu,rep_fechaCreacion,rep_enc_id,rep_estado,rep_num_encuesta,rep_evaluado,rep_fas_id,rep_site,rep_operacion,rep_servicio)" +
                                                "VALUES('" + dRow["DESCRIPCION"].ToString() + "','" + cmb.SelectedItem.Text + "','" + HttpContext.Current.Session["ID_USER"].ToString() + "',GETDATE()," + numEncuesta + ",1," + numeroEncuesta + ",'" + this.cmb_cagar_combo_evaluado.SelectedItem.Text + "'," + numeroEncuesta + "," + this.cmb_site.SelectedValue + "," + this.cmb_operacion.SelectedValue + "," + cmb_servicio.SelectedValue + ")";
                                        }
                                        else if (dRow["TIPO"].ToString() == "3")
                                        {
                                            RadioButtonList cmb = (RadioButtonList)this.Table1.Rows[contador].Cells[1].FindControl("rd_btn_" + Auxcontador);
                                            strSQL = "INSERT INTO T_PREGUNTA_RESPUESTA (rep_pregunta,rep_respuesta,rep_respondidoXUsu,rep_fechaCreacion,rep_enc_id,rep_estado,rep_num_encuesta,rep_evaluado,rep_fas_id,rep_site,rep_operacion,rep_servicio)" +
                                                "VALUES('" + dRow["DESCRIPCION"].ToString() + "','" + cmb.SelectedItem.Text + "','" + HttpContext.Current.Session["ID_USER"].ToString() + "',GETDATE()," + numEncuesta + ",1," + numeroEncuesta + ",'" + this.cmb_cagar_combo_evaluado.SelectedItem.Text + "'," + numeroEncuesta + "," + this.cmb_site.SelectedValue + "," + this.cmb_operacion.SelectedValue + "," + cmb_servicio.SelectedValue + ")";

                                        }
                                        else if (dRow["TIPO"].ToString() == "4")
                                        {
                                            TextBox txtArea = (TextBox)this.Table1.Rows[contador].Cells[1].FindControl("txt_area_" + Auxcontador);
                                            txtArea.TextMode = TextBoxMode.MultiLine;

                                            strSQL = "INSERT INTO T_PREGUNTA_RESPUESTA (rep_pregunta,rep_respuesta,rep_respondidoXUsu,rep_fechaCreacion,rep_enc_id,rep_estado,rep_num_encuesta,rep_evaluado,rep_fas_id,rep_site,rep_operacion,rep_servicio)" +
                                                "VALUES('" + dRow["DESCRIPCION"].ToString() + "','" + txtArea.Text + "','" + HttpContext.Current.Session["ID_USER"].ToString() + "',GETDATE()," + numEncuesta + ",1," + numeroEncuesta + ",'" + this.cmb_cagar_combo_evaluado.SelectedItem.Text + "'," + numeroEncuesta + "," + this.cmb_site.SelectedValue + "," + this.cmb_operacion.SelectedValue + "," + cmb_servicio.SelectedValue + ")";

                                        }

                                        clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);

                                    }
                                    contador = contador + 2;
                                    Auxcontador++;
                                }
                            }
                            else
                            {
                                foreach (DataRow dRow in ds.Tables[0].Rows)
                                {
                                    if (dRow["TIPO"].ToString() == "5")
                                        strSQL = "ENCABEZADO";

                                    else
                                    {
                                        if (dRow["TIPO"].ToString() == "1")
                                        {
                                            TextBox txt = (TextBox)this.Table1.Rows[contador].Cells[1].FindControl("txt_" + Auxcontador);

                                            strSQL = "INSERT INTO T_PREGUNTA_RESPUESTA (rep_pregunta,rep_respuesta,rep_respondidoXUsu,rep_fechaCreacion,rep_enc_id,rep_estado,rep_num_encuesta,rep_evaluado,rep_fas_id)" +
                                                "VALUES('" + dRow["DESCRIPCION"].ToString() + "','" + txt.Text + "','" + HttpContext.Current.Session["ID_USER"] + "',GETDATE()," + numEncuesta + ",1," + numeroEncuesta + ",'" + this.cmb_cagar_combo_evaluado.SelectedItem.Text + "'," + numeroEncuesta + ")";
                                        }
                                        else if (dRow["TIPO"].ToString() == "2")
                                        {
                                            DropDownList cmb = (DropDownList)this.Table1.Rows[contador].Cells[1].FindControl("cmb_" + Auxcontador);

                                            strSQL = "INSERT INTO T_PREGUNTA_RESPUESTA (rep_pregunta,rep_respuesta,rep_respondidoXUsu,rep_fechaCreacion,rep_enc_id,rep_estado,rep_num_encuesta,rep_evaluado,rep_fas_id)" +
                                                "VALUES('" + dRow["DESCRIPCION"].ToString() + "','" + cmb.SelectedItem.Text + "','" + HttpContext.Current.Session["ID_USER"].ToString() + "',GETDATE()," + numEncuesta + ",1," + numeroEncuesta + ",'" + this.cmb_cagar_combo_evaluado.SelectedItem.Text + "'," + numeroEncuesta + ")";
                                        }
                                        else if (dRow["TIPO"].ToString() == "3")
                                        {
                                            RadioButtonList cmb = (RadioButtonList)this.Table1.Rows[contador].Cells[1].FindControl("rd_btn_" + Auxcontador);
                                            strSQL = "INSERT INTO T_PREGUNTA_RESPUESTA (rep_pregunta,rep_respuesta,rep_respondidoXUsu,rep_fechaCreacion,rep_enc_id,rep_estado,rep_num_encuesta,rep_evaluado,rep_fas_id)" +
                                                "VALUES('" + dRow["DESCRIPCION"].ToString() + "','" + cmb.SelectedItem.Text + "','" + HttpContext.Current.Session["ID_USER"].ToString() + "',GETDATE()," + numEncuesta + ",1," + numeroEncuesta + ",'" + this.cmb_cagar_combo_evaluado.SelectedItem.Text + "'," + numeroEncuesta + ")";

                                        }
                                        else if (dRow["TIPO"].ToString() == "4")
                                        {
                                            TextBox txtArea = (TextBox)this.Table1.Rows[contador].Cells[1].FindControl("txt_area_" + Auxcontador);
                                            txtArea.TextMode = TextBoxMode.MultiLine;

                                            strSQL = "INSERT INTO T_PREGUNTA_RESPUESTA (rep_pregunta,rep_respuesta,rep_respondidoXUsu,rep_fechaCreacion,rep_enc_id,rep_estado,rep_num_encuesta,rep_evaluado,rep_fas_id)" +
                                                "VALUES('" + dRow["DESCRIPCION"].ToString() + "','" + txtArea.Text + "','" + HttpContext.Current.Session["ID_USER"].ToString() + "',GETDATE()," + numEncuesta + ",1," + numeroEncuesta + ",'" + this.cmb_cagar_combo_evaluado.SelectedItem.Text + "'," + numeroEncuesta + ")";

                                        }

                                        clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);

                                    }
                                    contador = contador + 2;
                                    Auxcontador++;
                                }
                            }



                        }
                        if (!returnError)
                            actualizarISREncuesta(ref returnError);

                    }
                    if (!returnError)
                    {
                        Message("ENVIO DE ENCUESTA", "Su encuesta fue enviada", TipoMensaje.Validacion, true);
                        enviarCorreo();
                        cargarEvaluado();
                        cargarTabla();
                    }
                }
                else
                {
                    Message("ENVIO DE ENCUESTA", "Faltaron preguntas por responder:\n" + "Las preguntas en rojo son las faltantes", TipoMensaje.Information, true);
                }
            }
            else
            {
                Message("ENVIO DE ENCUESTA", "Debe ingresar la operación y servicio:\n" + "Faltan datos por ingresar", TipoMensaje.Information, true);
            }

        }

        private void enviarCorreo()
        {
            int tipoEncuesta;
            tipoEncuesta = Convert.ToInt32(Session["idTipoEncuesta"]);
            //if (tipoEncuesta != 8)
            //{
            //    String strSQL = "SELECT enc_nombre AS 'NOMBRE' FROM T_ENCUESTA WHERE enc_id = " + numEncuesta + " AND enc_estado = 1";
            //    Boolean returnError = false;
            //    String idEncuesta = clsSQL.retornarDatoEscalar(ref returnError, strSQL).ToString();
            //    String correoOrigen = HttpContext.Current.Session["EMAIL"].ToString();
            //    String[] destinario = new String[] { "tcescchp@hp.com" };
            //    String html = "<div style='padding:15px;'>" +
            //        "<p style='margin:10px 0px 0px 10px;font-family:HP Simplified;font-size:16px'>El usuario <strong>" + HttpContext.Current.Session["USER"].ToString() + "<strong> respondío la encuesta " + idEncuesta + "</p>" +
            //        "</div>";
            //    html = crearRetornarCuerpoCorreo("Encuesta TCE Respondida", html, HttpContext.Current.Session["USER"].ToString(), "");
            //    email.EnviarEmail(email.darDestinatarios(destinario), correoOrigen, "Encuestas TCE", "Envio de respuesta", html);
            //}
        }

        private void actualizarISREncuesta(ref Boolean returnError)
        {
            String strSQL = "SELECT enc_tip_id FROM T_ENCUESTA WHERE enc_id = " + numEncuesta;
            returnError = false;
            String tipo = clsSQL.retornarDatoEscalar(ref returnError, strSQL).ToString();

            switch (tipo)
            {
                case "1":
                    strSQL = "UPDATE ENCUESTA  SET ENCUESTA.uni_estado_encuesta = 2 " +
                     "FROM T_UNIDAD_ENCUESTA ENCUESTA " +
                     "INNER JOIN T_ENCUESTA_USUARIO USUARIO ON USUARIO.uen_id = ENCUESTA.uni_uen_id " +
                     "WHERE USUARIO.uen_usu_id = " + HttpContext.Current.Session["ID_USER"].ToString() + " AND ENCUESTA.uni_enc_id = " + numEncuesta;
                    clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
                    break;
                case "2":
                    strSQL = "EXEC SP_MM_ACTUALIZAR_ENCUESTA_ISR " + HttpContext.Current.Session["ID_USER"].ToString() + "," + numEncuesta + "," + this.cmb_cagar_combo_evaluado.SelectedItem.Value;
                    clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
                    break;
                case "3":
                    strSQL = "EXEC SP_MM_ACTUALIZAR_ENCUESTA_ISR " + HttpContext.Current.Session["ID_USER"].ToString() + "," + numEncuesta + "," + this.cmb_cagar_combo_evaluado.SelectedItem.Value;
                    clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
                    break;
                case "4":
                    strSQL = "EXEC SP_MM_ACTUALIZAR_ENCUESTA_ISR " + HttpContext.Current.Session["ID_USER"].ToString() + "," + numEncuesta + "," + this.cmb_cagar_combo_evaluado.SelectedItem.Value;
                    clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
                    break;
            }
        }

        protected void BtnCierreContador_ServerClick(object sender, EventArgs e)
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));

            HttpContext.Current.Response.Redirect("~/Default.aspx", true);
        }

        protected void cmb_operacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            String strSQL = "SELECT area_id AS 'ID', area_nombre AS 'NOMBRE' FROM T_AREA WHERE area_operacion =" + cmb_operacion.SelectedValue.ToString() + "";
            Boolean returnError = false;
            clsSQL.llenarComboBox(ref returnError, strSQL, ref this.cmb_servicio, "ID", "NOMBRE");
        }

        protected void cmb_servicio_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}