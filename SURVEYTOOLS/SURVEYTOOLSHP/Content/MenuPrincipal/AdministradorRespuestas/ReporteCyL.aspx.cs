using SURVEYTOOLSHP.Model;
using System;
using System.Data;

namespace SURVEYTOOLSHP.Content.MenuPrincipal.AdministradorRespuestas
{
    public partial class ReporteCyL : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ObtnerMetricas();
        }

        public void ObtnerMetricas()
        {

            //CONTADORES CLIMA
            Boolean returnError = false;
            String strSQL = "";
            strSQL = "EXEC [SP_MM_DAR_ESTADISTICA_CLIMA] '1008'";
            DataSet ds = new DataSet();
            ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);

            string ProcentajeEncuestasRespondidas = "";
            string ProcentajeEncuestasFaltantes = "";
            string TotalEncuestasRespondidas = "";
            string TotalEncuestasFaltantes = "";
            int TotalEncuestas = 0;

            if (!returnError)
            {
                if (ds.Tables.Count > 0)
                {
                    ProcentajeEncuestasRespondidas = ds.Tables[0].Rows[0]["PORCENTAJE_RESPONDIDO"].ToString();
                    ProcentajeEncuestasFaltantes = ds.Tables[0].Rows[0]["PORCENTAJE_FALTANTE"].ToString();
                    TotalEncuestasRespondidas = ds.Tables[0].Rows[0]["RESPONDIDA"].ToString();
                    TotalEncuestasFaltantes = ds.Tables[0].Rows[0]["SIN_RESPONDER"].ToString();

                }
            }

            BarEncuestasRespondidas.Style.Add("width", ProcentajeEncuestasRespondidas);
            BarEncuestasFaltantes.Style.Add("width", ProcentajeEncuestasFaltantes);
            LabProgreso.Text = TotalEncuestasRespondidas;
            LabFaltante.Text = TotalEncuestasFaltantes;
            LabPorcentajeClima.Text = ProcentajeEncuestasRespondidas;
            LabRespondidas.Text = TotalEncuestasRespondidas;
            LabPorResponder.Text = TotalEncuestasFaltantes;
            TotalEncuestas = Convert.ToInt32(TotalEncuestasRespondidas) + Convert.ToInt32(TotalEncuestasFaltantes);
            LabTotalClima.Text = TotalEncuestas.ToString();

            ProcentajeEncuestasRespondidas = ProcentajeEncuestasRespondidas.Replace("%", string.Empty);


            if (int.Parse(ProcentajeEncuestasRespondidas) <= 30)
            {
                LabPorcentajeClima.Style.Add("color", "#D53921");
            }
            else if (int.Parse(ProcentajeEncuestasRespondidas) <= 50)
            {
                LabPorcentajeClima.Style.Add("color", "#DB6F1F");
            }
            else
            {
                LabPorcentajeClima.Style.Add("color", "#0D801B");
            }

            //CONTADORES LIDEREZGO
            strSQL = "EXEC [SP_MM_DAR_ESTADISTICA_LIDERAZGO] '1008'";
            DataSet dsLid = new DataSet();
            dsLid = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);

            string ProcentajeEncuestasRespondidasLid = "";
            string ProcentajeEncuestasFaltantesLid = "";
            string TotalEncuestasRespondidasLid = "";
            string TotalEncuestasFaltantesLid = "";
            int TotalEncuestasLid = 0;

            if (!returnError)
            {
                if (dsLid.Tables.Count > 0)
                {
                    ProcentajeEncuestasRespondidasLid = dsLid.Tables[0].Rows[0]["PORCENTAJE_RESPONDIDO"].ToString();
                    ProcentajeEncuestasFaltantesLid = dsLid.Tables[0].Rows[0]["PORCENTAJE_FALTANTE"].ToString();
                    TotalEncuestasRespondidasLid = dsLid.Tables[0].Rows[0]["RESPONDIDA"].ToString();
                    TotalEncuestasFaltantesLid = dsLid.Tables[0].Rows[0]["SIN_RESPONDER"].ToString();

                }
            }

            BarEncuestasRespondidasLid.Style.Add("width", ProcentajeEncuestasRespondidasLid);
            BarEncuestasFaltantesLid.Style.Add("width", ProcentajeEncuestasFaltantesLid);
            LabProgresoLid.Text = TotalEncuestasRespondidasLid;
            LabFaltanteLid.Text = TotalEncuestasFaltantesLid;
            LabPorcentajeLiderazgo.Text = ProcentajeEncuestasRespondidasLid;
            LabRespondidasLid.Text = TotalEncuestasRespondidasLid;
            LabPorResponderLid.Text = TotalEncuestasFaltantesLid;
            TotalEncuestasLid = Convert.ToInt32(TotalEncuestasRespondidasLid) + Convert.ToInt32(TotalEncuestasFaltantesLid);
            LabTotalLiderazgo.Text = TotalEncuestasLid.ToString();

            ProcentajeEncuestasRespondidasLid = ProcentajeEncuestasRespondidasLid.Replace("%", string.Empty);


            if (int.Parse(ProcentajeEncuestasRespondidasLid) <= 30)
            {
                LabPorcentajeLiderazgo.Style.Add("color", "#D53921");
            }
            else if (int.Parse(ProcentajeEncuestasRespondidasLid) <= 50)
            {
                LabPorcentajeLiderazgo.Style.Add("color", "#DB6F1F");
            }
            else
            {
                LabPorcentajeLiderazgo.Style.Add("color", "#0D801B");
            }

            //GRIDS CYL
            strSQL = "EXEC SP_DAR_VER_ESTATUS_CyL " + 1008;
            returnError = false;
            DataSet dsGrid = new DataSet();
            dsGrid = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (!returnError)
            {
                if (dsGrid.Tables.Count > 0)
                {
                    this.dgrid_encuestasClima.DataSource = dsGrid.Tables[0];
                    this.dgrid_encuestasClima.DataBind();

                    this.dgrid_encuestasLiderazgo.DataSource = dsGrid.Tables[1];
                    this.dgrid_encuestasLiderazgo.DataBind();

                }
            }

        }
    }
}