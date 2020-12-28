using System;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;

namespace SURVEYTOOLSHP.Model
{
    public class clsLogin
    {
        BasePage objMensaje = new BasePage();
        String strSQL = "";
        #region Se asigna a la variable universal el string de conexion
        public void AsignarConectionString()
        {
            HttpContext.Current.Session["CONECTION_STRINGS"] = ConfigurationManager.ConnectionStrings["HP_SURVEYTOOLSHP"].ConnectionString;
            HttpContext.Current.Session["PIE_PAGINA"] = ConfigurationManager.AppSettings["PIEPAGINA"].ToString();
        }
        #endregion
        #region Inicio de sessión fuera de SCC y HP
        public Boolean LOGIN_SURVEYTOOLS(String prmNT, ref Boolean prmreturnError)
        {
            if (prmNT == String.Empty)
            {
                return false;
            }
            else
            {
                prmreturnError = false;
                strSQL = "EXEC SP_MM_DAR_DATOS_USUARIO '" + prmNT.Trim() + "'";
                DataSet ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref prmreturnError);
                if (!prmreturnError)
                {
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows.Count == 1)
                    {
                        foreach (DataRow dRow in ds.Tables[0].Rows)
                        {

                            HttpContext.Current.Session["LOGIN"] = prmNT;
                            HttpContext.Current.Session["ID_USER"] = dRow["ID"].ToString(); ;
                            HttpContext.Current.Session["USER"] = dRow["NOMBRE"].ToString();
                            HttpContext.Current.Session["PERMISOS"] = dRow["PERMISOS"].ToString();
                            HttpContext.Current.Session["EMAIL"] = dRow["EMAIL"].ToString();

                        }


                    }

                }
                return true;
            }
        }

        #endregion
        
  
    }
}