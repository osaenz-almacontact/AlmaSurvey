using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace SURVEYTOOLSHP.Model
{
    public class clsMenu
    {
        private BasePage objMensaje = new BasePage();
        
        public DataSet darMenuPrincipal(ref Boolean prmreturnError) {
            prmreturnError = false;
            DataSet ds = new DataSet();
            String strSQL = "EXEC SP_MM_DAR_MENUPRINCIPAL " + HttpContext.Current.Session["PERMISOS"] ;
            //String strSQL = "EXEC SP_MM_DAR_MENUPRINCIPAL 1";
            ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref prmreturnError);
            if (!prmreturnError)
            {
                return ds;
            }
            else
            {
                
                return null;
            }

        }
        public DataSet darMenus(ref Boolean prmreturnError,String prmIDPagina) {
            prmreturnError = false;
            DataSet ds = new DataSet();
           String strSQL = "EXEC SP_MM_DAR_SUBMENU " + prmIDPagina + "," + HttpContext.Current.Session["PERMISOS"];
            //String strSQL = "EXEC SP_MM_DAR_SUBMENU " + prmIDPagina + ",1";
            ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref prmreturnError);
            if (!prmreturnError)
            
                return ds;
            
            else
                return null;
        }
    }
}