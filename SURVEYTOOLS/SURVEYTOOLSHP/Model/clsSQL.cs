using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data.OleDb;
using System.Web.UI.WebControls;

namespace SURVEYTOOLSHP.Model
{
    public class clsSQL
    {
        private static BasePage objMensaje = new BasePage();
  
        #region llena un dataSet con una consulta hecha en sql
        
        public static void llenarComboBox(ref Boolean prmReturnError, String prmConsulta, ref DropDownList prmlista, String prmColumnaValor, String prmColumnaVista)
        {
            try
            {
                DataSet ds = new DataSet();
                String conexion_ = HttpContext.Current.Session["CONECTION_STRINGS"].ToString();
                using (SqlConnection objCon = new SqlConnection(HttpContext.Current.Session["CONECTION_STRINGS"].ToString()))
                {

                    SqlDataAdapter da = new SqlDataAdapter(prmConsulta, objCon);
                    da.SelectCommand.CommandType = CommandType.Text;
                    da.Fill(ds);
                    objCon.Close();


                }
                prmlista.DataSource = ds;
                prmlista.DataTextField = prmColumnaVista;
                prmlista.DataValueField = prmColumnaValor;
                prmlista.DataBind();

            }
            catch (Exception e)
            {
                objMensaje.Message("ERRO SURVEYTOOLHP", e.Message, BasePage.TipoMensaje.Error, false);
                
  }

        }
        public static void llenarComboBoxConSelect(ref Boolean prmReturnError, String prmConsulta, ref DropDownList prmlista, String prmColumnaValor, String prmColumnaVista)
        {
            try
            {
                DataSet ds = new DataSet();
                String conexion_ = HttpContext.Current.Session["CONECTION_STRINGS"].ToString();
                using (SqlConnection objCon = new SqlConnection(HttpContext.Current.Session["CONECTION_STRINGS"].ToString()))
                {

                    SqlDataAdapter da = new SqlDataAdapter(prmConsulta, objCon);
                    da.SelectCommand.CommandType = CommandType.Text;
                    da.Fill(ds);
                    objCon.Close();


                }
                ListItem item = new ListItem();
                item.Value = "0";
                item.Text = "Seleccionar...";
                prmlista.Items.Clear();
                prmlista.Items.Add(item);
                prmlista.AppendDataBoundItems = true;
                prmlista.DataSource = ds;
                prmlista.DataTextField = prmColumnaVista;
                prmlista.DataValueField = prmColumnaValor;
                prmlista.DataBind();

            }
            catch (Exception e)
            {
                objMensaje.Message("ERROR SURVEYTOOLHP", e.Message, BasePage.TipoMensaje.Error, false);

            }

        }
        #endregion
        #region llena un radiobuttonlist medinate una consulta
        public static void llenarRadioButtonList(ref Boolean prmReturnError, String prmConsulta, ref RadioButtonList prmlistaBoton, String prmColumnaValor, String prmColumnaVista)
        {
            try
            {
                DataSet ds = new DataSet();
                String conexion_ = HttpContext.Current.Session["CONECTION_STRINGS"].ToString();
                using (SqlConnection objCon = new SqlConnection(HttpContext.Current.Session["CONECTION_STRINGS"].ToString()))
                {

                    SqlDataAdapter da = new SqlDataAdapter(prmConsulta, objCon);
                    da.SelectCommand.CommandType = CommandType.Text;
                    da.Fill(ds);
                    objCon.Close();


                }
                prmlistaBoton.DataSource = ds;
                prmlistaBoton.DataTextField = prmColumnaVista;
                prmlistaBoton.DataValueField = prmColumnaValor;
                prmlistaBoton.DataBind();

            }
            catch (Exception e)
            {
           
            }

        }

        #endregion
        #region ejecuta un procedimiento o una consulta el cual retorna un valor escalar

        public static Object retornarDatoEscalar(ref Boolean prmReturnError, String prmConsulta)
        {
            Object respuesta = null;
            try
            {
                using (SqlConnection objCon = new SqlConnection(HttpContext.Current.Session["CONECTION_STRINGS"].ToString()))
                {
                    objCon.Open();
                    SqlCommand comand = new SqlCommand(prmConsulta, objCon);
                    respuesta = comand.ExecuteScalar();
                    objCon.Close();
                    return respuesta;
                }
            }
            catch (Exception e)
            {

                prmReturnError = true;
                
                return null;
            }
        }
        #endregion

        #region ejecuta un procedimiento almacenado o una consulta

        public static DataSet ejecutarProceConsulSQL(String prmConsulta, ref Boolean prmreturnError)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection objCon = new SqlConnection(HttpContext.Current.Session["CONECTION_STRINGS"].ToString()))
                {
                    SqlDataAdapter da = new SqlDataAdapter(prmConsulta, objCon);
                    da.SelectCommand.CommandType = CommandType.Text;
                    da.Fill(ds);
                    objCon.Close();
                    return ds;
                }
            }
            catch (Exception e)
            {

                prmreturnError = true;
                objMensaje.Message("ERROR SURVEYTOOLHP", e.Message, BasePage.TipoMensaje.Error, false);
                return null;

            }

        }
        #endregion


        #region Excel a SQL
        public static void exportandoDatosExcel(ref Boolean prmReturnError, string prmConeccionExcel, String prmTablaDestino)
        {
            try
            {
                using (OleDbConnection connection =
                                             new OleDbConnection(prmConeccionExcel))
                {
                    OleDbCommand command = new OleDbCommand
                            ("Select * FROM [Encuesta$]", connection);

                    connection.Open();

                    // Create DbDataReader to Data Worksheet 
                    using (DbDataReader dr = command.ExecuteReader())
                    {

                        // SQL Server Connection String 
                        string sqlConnectionString = HttpContext.Current.Session["CONECTION_STRINGS"].ToString(); ;

                        // Bulk Copy to SQL Server 
                        using (SqlBulkCopy bulkCopy =
                                   new SqlBulkCopy(sqlConnectionString))
                        {
                            bulkCopy.DestinationTableName = prmTablaDestino;
                            bulkCopy.WriteToServer(dr);

                        }
                    }
                    connection.Close();
                }
            }
            catch (SqlException eSQL)
            {
                prmReturnError = true;
                objMensaje.Message("ERROR SURVEYTOOLHP", eSQL.Message, BasePage.TipoMensaje.Error, false);
            }

        }

        #endregion

    }
}