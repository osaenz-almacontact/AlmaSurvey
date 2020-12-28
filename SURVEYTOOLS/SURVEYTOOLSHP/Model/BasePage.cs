using System;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using SURVEYTOOLSHP.Model;
using System.Threading;
using System.Globalization;
using System.Configuration;
public class BasePage : Page
{
    Boolean returnError = false;
    public enum TipoMensaje
    {
        Error = 0,
        Information = 1,
        Validacion = 2,
        Warning = 3
    }
    public static String p_UrlError { get; set; }

    protected override void OnPreInit(EventArgs e)
    {

        clsLogin login = new clsLogin();
        login.AsignarConectionString();

        if (!login.LOGIN_SURVEYTOOLS(HttpContext.Current.Session["LOGIN"].ToString(), ref returnError))
        {
            Response.Redirect(ConfigurationManager.AppSettings["PAG_INICIO"].ToString());
        }
        if (returnError)
            Response.Redirect(p_UrlError);
        InitializeCulture();
        HttpContext.Current.Session["HP_AMBAR_PANEL_SEPARADOR"] = "1";
        if (Session["MyTheme"] == null)
        {
            Session.Add("MyTheme", "Default");
            Page.Theme = ((string)Session["MyTheme"]);

        }
        else
        {
            Page.Theme = ((string)Session["MyTheme"]);
        }

    }

    #region Establece el idioma
    protected override void InitializeCulture()
    {
        if (Session["language"].ToString() != String.Empty)
        {
            setCulture(Session["language"].ToString());
            base.InitializeCulture();
        }
    }

    public void setCulture(String prmlanguage)
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(prmlanguage);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(prmlanguage);
    }
    #endregion
    #region Muestra el mensaje segun el evento

    public void Message(String prmTitulo, String prmDescripcion, TipoMensaje prmtipo, Boolean prmVentana)
    {
        if (!prmVentana)
        {
            String lblTituloMensaje = prmTitulo;
            String lblDescripcionMensaje = prmDescripcion;
            String clrBackColor = String.Empty;
            String imgMensaje = String.Empty;
            switch (prmtipo)
            {
                case TipoMensaje.Error:
                    CrearLogErr(prmTitulo, prmDescripcion);
                    imgMensaje = "~/Resources/Mensaje/img_Error.png";
                    clrBackColor = "#C10C0C";
                    lblTituloMensaje = "Error";
                    lblDescripcionMensaje = "Error of application, please inform the system administrator";
                    break;
                case TipoMensaje.Information:
                    imgMensaje = "~/Resources/Mensaje/img_Information.png";
                    clrBackColor = "#272EAE";
                    break;
                case TipoMensaje.Validacion:
                    imgMensaje = "~/Resources/Mensaje/img_Validacion.png";
                    clrBackColor = "#4F7946";
                    break;
                case TipoMensaje.Warning:
                    imgMensaje = "~/Resources/Mensaje/img_Warning.png";
                    clrBackColor = "#ACB41C";
                    break;
                default:
                    imgMensaje = "~/Resources/Mensaje/img_Error.png";
                    clrBackColor = "#C10C0C";
                    break;



            }
            HttpContext.Current.Session["SURVEY_IMAGEN_ERROR"] = imgMensaje;
            HttpContext.Current.Session["SURVEY_DESCRIPCION_ERROR"] = lblDescripcionMensaje;
            HttpContext.Current.Session["SURVEY_TITULO_ERROR"] = lblTituloMensaje;
            HttpContext.Current.Session["SURVEY_COLOR_DESCRIPCION_ERROR"] = clrBackColor;

            HttpContext.Current.Response.Redirect(p_UrlError);
        }
        else
        {
            if (Page.Master != null)
            {
                Label lblTituloMensaje;
                Label lblDescripcionMensaje;
                Panel plnMessage;
                Color clrBackColor;
                System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();


                lblTituloMensaje = (Label)Master.FindControl("lbl_titulo_mensaje");
                lblDescripcionMensaje = (Label)Master.FindControl("lbl_descripcion_Mensaje");
                plnMessage = (Panel)Master.FindControl("pnl_Mensaje");
                img = (System.Web.UI.WebControls.Image)Master.FindControl("img_mensaje");
                lblTituloMensaje.Text = prmTitulo;
                lblDescripcionMensaje.Text = prmDescripcion;

                switch (prmtipo)
                {
                    case TipoMensaje.Error:
                        CrearLogErr(prmTitulo, prmDescripcion);
                        clrBackColor = Color.FromName("#FFCECE");
                        img.ImageUrl = "~/Resources/Mensaje/img_Error.png";
                        lblTituloMensaje.Text = "Error";
                        lblDescripcionMensaje.Text = "Error of application, please inform the system administrator";
                        break;
                    case TipoMensaje.Information:
                        clrBackColor = Color.FromName("#272EAE");
                        img.ImageUrl = "~/Resources/Mensaje/img_Information.png";
                        break;
                    case TipoMensaje.Validacion:
                        clrBackColor = Color.FromName("#4F7946");
                        img.ImageUrl = "~/Resources/Mensaje/img_Validacion.png";
                        break;
                    case TipoMensaje.Warning:
                        clrBackColor = Color.FromName("#ACB41C");
                        img.ImageUrl = "~/Resources/Mensaje/img_Warning.png";
                        break;
                    default:
                        clrBackColor = Color.FromName("#0C2B03");
                        img.ImageUrl = "~/Resources/Mensaje/img_Error.png";
                        break;
                }
                plnMessage.ForeColor = clrBackColor;
                HttpContext.Current.Session["HP_AMBAR_ERR_DESCRIPTION"] = lblDescripcionMensaje.Text;
                HttpContext.Current.Session["HP_AMBAR_ERR_TITLE"] = lblTituloMensaje.Text;
                HttpContext.Current.Session["HP_AMBAR_ERR_COLOR"] = clrBackColor.Name.ToString();
                HttpContext.Current.Session["HP_AMBAR_PANEL_MENSAJE"] = "1";

                plnMessage.Visible = Convert.ToBoolean(Convert.ToInt32(HttpContext.Current.Session["HP_AMBAR_PANEL_MENSAJE"].ToString()));
                HttpContext.Current.Session["MENSAJE"] = plnMessage;
            }

        }
    }
    #endregion
    #region Crear el log del error
    public void CrearLogErr(String prmTitlo, String prmDescripcion)
    {
        StreamWriter A_Fichero = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + @"\log\Application.log", true);
        A_Fichero.WriteLine("********************");
        A_Fichero.WriteLine();
        A_Fichero.WriteLine(prmTitlo);
        A_Fichero.WriteLine("Descripción: " + prmTitlo);
        A_Fichero.WriteLine("Usuario: " + HttpContext.Current.Request.ServerVariables["LOGON_USER"].ToString());
        A_Fichero.WriteLine();
        A_Fichero.WriteLine("********************");
        A_Fichero.Flush();
        A_Fichero.Close();
    }


    #endregion

    #region cambiarCaracter

    public String cambiarCaracter(String prmValor)
    {
        prmValor = prmValor.Replace("&#225;", "á");
        prmValor = prmValor.Replace("&#233;", "é");
        prmValor = prmValor.Replace("&#237;", "í");
        prmValor = prmValor.Replace("&#243;", "ó");
        prmValor = prmValor.Replace("&#250;", "ú");
        prmValor = prmValor.Replace("&#193;", "Á");
        prmValor = prmValor.Replace("&#201;", "É");
        prmValor = prmValor.Replace("&#2O5;", "Í");
        prmValor = prmValor.Replace("&#211;", "Ó");
        prmValor = prmValor.Replace("&#218;", "Ú");
        return prmValor;

    }

    #endregion

    #region método que se encarga de abrir una pagina utilizando condigo javascript
    public void abrirVentanaCentrada(String prmVentana, String prmAlto, String prmAncho, Boolean prmextras)
    {


        String extras;
        String ancho = prmAncho, alto = prmAlto;
        if (prmextras)
            extras = ",resizable=n0, menubar=no, toolbar=no, directories=no, location=no, scrollbars=yes, status=yes";
        else
        {
            extras = "";
        }

        abreVentana(prmVentana, ancho, alto, extras);






    }

    public void abreVentana(String prmPagina, String prmancho, String prmAlto, String prmExtra)
    {

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("\r\n<script language='JavaScript'>\r\n");
        sb.Append("\r\n");
        sb.Append("\r\nvar x = screen.width;\r\n");
        sb.Append("\r\nvar y = screen.height;\r\n");
        sb.Append("\r\nx = (x / 2) - (" + prmancho + "/2);\r\n");
        sb.Append("\r\ny = (y / 2) - (" + prmAlto + "/2);\r\n");
        sb.Append("\r\n");
        sb.Append("AbrirCentrado(x,y)");
        sb.Append("\r\n");
        sb.Append("function AbrirCentrado(x,y){");

        sb.Append("window.open('" + prmPagina + "','Ventanas','width =" + prmancho + ",height =" + prmAlto + ",top='+ y +',left='+ x +'" + prmExtra + "')");
        sb.Append("\r\n");

        sb.Append("\r\n}\r\n");

        sb.Append("</script>");

        ClientScript.RegisterClientScriptBlock(GetType(), "OpenCenterPopUp", sb.ToString());

    }
    public void cerrarVentana()
    {

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("\r\n<script language='JavaScript'>\r\n");
        sb.Append("\r\n");

        sb.Append("window.close();");
        sb.Append("\r\n");
        sb.Append("</script>");

        ClientScript.RegisterClientScriptBlock(GetType(), "conceptos", sb.ToString());
    }
    #endregion
    #region Encargada de ejecutar javaScript
    public void ejecutarScript(String prmScript)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("\r\n<script language='JavaScript'>\r\n");
        sb.Append("\r\n");
        sb.Append("\r\n" + prmScript + "\r\n");
        sb.Append("\r\n");
        sb.Append("</script>");
        ClientScript.RegisterClientScriptBlock(GetType(), "script", sb.ToString());

    }
    #endregion
    #region Crear cuerpo de mensaje de un correo
    //Este el body total del mensaje del correo 
    public String crearRetornarCuerpoCorreo(String prmTituloMensaje, String prmDescripcion, String prmFirma, String prmArea)
    {

        String titulo = "<table align='center' style='margin: 10px 50px 10px 50px;width:600px;font-family:HP Simplified;' cellpadding=0 cellspacing=0>";
        titulo += "<tr><td><p style='font-size:30px;color:#214023;text-align:center;margin-left:20px;margin-top:25px;font-family:HP Simplified;'>SURVEYHPTOOLS</p></strong></td></tr>";
        titulo += "<tr><td><hr style='color:#214023' size='5' /></td></tr>";
        titulo += "<tr><td style='font-size:18px;color:White;background-color:#214023;text-align:center'><strong>" + prmTituloMensaje + "</strong></td></tr>";

        String descripcion = "<tr><td>";
        descripcion += prmDescripcion;
        descripcion += "</td></tr>";
        String firma = "<tr><td><hr style='color:#A6A6A7' size='5' /> <br/></td></tr><tr><td><p style='font-size:16px;color:#585964;margin-bottom:6px'><strong>Cordialmente:</strong></p><p style='margin:0px;color:#A6A6A7;font-size:14px;'><strong>" + prmFirma + "</strong></p></td></tr></table>";
        String html = "<html><body><div style='width:100%;margin:0px'>" + titulo + descripcion + firma + "</div></body></html>";

        return html;
    }
    #endregion
    #region exportacion de excel a sql server

    public void exportarExcelSQL(ref Boolean prmreturError, ref FileUpload fileExcel, String prmTablaDestino)
    {

        string ExcelContentType = "application/vnd.ms-excel";
        string Excel2010ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        if (fileExcel.HasFile)
        {
            //Check the Content Type of the file 
            if (fileExcel.PostedFile.ContentType == ExcelContentType || fileExcel.PostedFile.ContentType == Excel2010ContentType)
            {

                //Save file path 
                string path = string.Concat(Server.MapPath("~/ArchivosExcel/"), fileExcel.FileName);
                //Save File as Temp then you can delete it if you want 
                fileExcel.SaveAs(path);
                //string path = @"C:\Users\Johnney\Desktop\ExcelData.xls"; 

                //string excelConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", path);
                string excelConnectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'", path);

                clsSQL.exportandoDatosExcel(ref prmreturError, excelConnectionString, prmTablaDestino);




            }
            else
            {
                Message("IMPORTAR ARCHIVO", "Existen algunos archivos activos, por favor cierrlos", TipoMensaje.Warning, true);
                prmreturError = true;
            }
        }
        else
        {
            prmreturError = true;
        }
    }

    #endregion
    #region exportar de un datagrid a un libor en excel

    public void exportarGridView_Excel(GridView prmDataGrid, String prmNombreLibro)
    {

        Response.ClearContent();
        Response.Buffer = true;
        String FileName = prmNombreLibro + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        prmDataGrid.AllowPaging = false;

        prmDataGrid.GridLines = GridLines.Both;
        prmDataGrid.HeaderStyle.Font.Bold = true;
        prmDataGrid.HeaderStyle.BackColor = System.Drawing.Color.FromName("#214023");
        prmDataGrid.HeaderStyle.ForeColor = System.Drawing.Color.White;
        prmDataGrid.AlternatingRowStyle.BackColor = System.Drawing.Color.White;
        prmDataGrid.RowStyle.BackColor = System.Drawing.Color.FromName("#ECFDEF");
        prmDataGrid.CssClass = "css_fuente_letra";
        prmDataGrid.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();



    }

    #endregion

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

}
