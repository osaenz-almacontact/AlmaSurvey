using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Resources;
using System.Reflection;
using SURVEYTOOLSHP.Model;
namespace SURVEYTOOLSHP.Master
{
    public partial class Default : System.Web.UI.MasterPage
    {
        private clsMenu menu;
        ResourceManager mg = new ResourceManager("Resources.SURVEYTOOLSHP", Assembly.Load("App_GlobalResources"));
        protected void Page_Load(object sender, EventArgs e)
        {
            this.menu = new clsMenu();
            if (!Page.IsPostBack)
            {
                darPiePagina();
                cargarMenu();
                cargarPaneles();
                    cargarFaseActual();

            }
            this.pnl_Mensaje.Visible = false;
        }
        private void cargarPaneles()
        {
            this.pnl_subMenu.Visible = false;
            this.img_btn_separador.Visible = false;



        }
        private void darPiePagina()
        {
            this.lbl_Credenciales_HP.Text = HttpContext.Current.Session["PIE_PAGINA"].ToString();
        }


        #region cargarMenuPrincipal
        private void cargarMenu()
        {
            DataSet ds = new DataSet();
            Boolean returnError = false;
            ds = menu.darMenuPrincipal(ref returnError);
            if (ds.Tables.Count > 0)
            {
                this.menuPrincipal.Items.Clear();
                foreach (DataRow dRow in ds.Tables[0].Rows)
                {
                    MenuItem item = new MenuItem();
                    item.Text = dRow["PAGINA_TITULO"].ToString();
                    item.Target = dRow["PAGINA_URL"].ToString();
                    item.Value = dRow["PAGINA_ID"].ToString();
                    this.menuPrincipal.Items.Add(item);

                }
            }

        }
        #endregion


        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (this.pnl_subMenu.Visible)
            {
                this.img_btn_separador.ImageUrl = "~/Resources/Iconos/Next.png";
                this.pnl_subMenu.Visible = false;

            }
            else
            {
                this.img_btn_separador.ImageUrl = "~/Resources/Iconos/Back.png";
                this.pnl_subMenu.Visible = true;
            }
        }

        private void cargarFaseActual()
        {
            DateTime fechaActual = DateTime.Today;
            String strSQL = "EXEC SP_MM_DAR_FASE '" + fechaActual.ToString("yyyy/MM/dd") + "'";
            Boolean returnError = false;
            String fase = clsSQL.retornarDatoEscalar(ref returnError, strSQL) != null ? clsSQL.retornarDatoEscalar(ref returnError, strSQL).ToString() : "0";
            try
            {
                if (fase == "0")
                {
                    strSQL = "SELECT SUBSTRING(CONVERT(VARCHAR(50),MAX(fas_nombre)),1,LEN(MAX(fas_nombre))) FROM T_FASE";
                    int ultimoNumero = Convert.ToInt32(clsSQL.retornarDatoEscalar(ref returnError, strSQL).ToString()) + 1;
                    strSQL = "SELECT CONVERT(VARCHAR,MAX(fas_fechaFinalizacion),111) FROM T_FASE";
                    DateTime fechaMax = Convert.ToDateTime(clsSQL.retornarDatoEscalar(ref returnError, strSQL).ToString());
                    DateTime fechafinal = fechaMax.AddMonths(6);
                    if (!returnError)
                    {
                        strSQL = "set dateformat ymd; INSERT INTO T_FASE(fas_nombre,fas_fechaComienzo,fas_fechaFinalizacion)" +
                                "VALUES('Fase " + ultimoNumero + "','" + fechaMax.ToString("yyyy/MM/dd") + "','" + fechafinal.ToString("yyyy/MM/dd") + "')";
                        clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
                        if (!returnError)
                        {
                            strSQL = "EXEC SP_MM_DAR_FASE '" + fechaActual.ToString("yyyy/MM/dd") + "'";
                            fase = clsSQL.retornarDatoEscalar(ref returnError, strSQL).ToString();
                        }
                    }

                }
            }
            catch{}

             // TODO: Ajustar el cambio de fase de manera que no haya inconveniente al generar la fase siguiente.

            if (!returnError)
                this.lbl_fase.Text = "Fase Actual:" + fase;
        }

        protected void menuPrincipal_MenuItemClick(object sender, MenuEventArgs e)
        {
            Boolean returnError = false;
            if (this.menuPrincipal.SelectedItem.Target.ToString() == "NoTiene")
            {
                DataSet ds = new DataSet();
                ds = menu.darMenus(ref returnError, this.menuPrincipal.SelectedItem.Value.ToString());
                if (!returnError)
                {
                    if (ds.Tables.Count > 0)
                    {
                        this.trV_encuestas.Nodes.Clear();
                        TreeNode nodoPrincipal = new TreeNode();
                        nodoPrincipal.Text = "ELIGA UNA OPCIÓN";
                        this.trV_encuestas.Nodes.Add(nodoPrincipal);

                        foreach (DataRow dRow in ds.Tables[0].Rows)
                        {
                            TreeNode nodo = new TreeNode();
                            nodo.Text = dRow["PAGINA_TITULO"].ToString();
                            nodo.Target = dRow["PAGINA_URL"].ToString();
                            nodo.Value = dRow["PAGINA_ID"].ToString();
                            nodoPrincipal.ChildNodes.Add(nodo);


                        }
                    }
                }
                this.trV_encuestas.ExpandAll();
                this.pnl_subMenu.Visible = true;

                this.img_btn_separador.Visible = true;
                this.img_btn_separador.ImageUrl = "~/Resources/Iconos/Back.png";
            }
            else
            {
                HttpContext.Current.Session["ID_PAGINA_MENU"] = this.menuPrincipal.SelectedItem.Value;
                Response.Redirect(this.menuPrincipal.SelectedItem.Target);
            }
        }

        protected void trV_encuestas_SelectedNodeChanged(object sender, EventArgs e)
        {
            Response.Redirect(this.trV_encuestas.SelectedNode.Target.ToString());
        }

        protected void img_btn_ocultar_Click(object sender, ImageClickEventArgs e)
        {
            this.pnl_Mensaje.Visible = false;
        }
    }
}
