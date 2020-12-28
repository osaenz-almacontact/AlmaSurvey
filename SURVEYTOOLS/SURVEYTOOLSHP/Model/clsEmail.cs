using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Net.Mail;

namespace SURVEYTOOLSHP.Model
{
    public class clsEmail
    {
        BasePage objMensaje = new BasePage();
        Boolean returnError = false;
        
        public Boolean EnviarEmail(MailAddressCollection prmDireccionCorreosDestino, String prmDirreccioCorreoOrigen, String prmNombreCorreo, String prmAsuntoCorreo, String prmContenidoCorreo) {
            MailMessage mensaje = new MailMessage();
            SmtpClient SMTP = new SmtpClient();
            Boolean correoError = false;
            try
            {
                foreach (MailAddress addr in prmDireccionCorreosDestino)
                {
                    mensaje.To.Add(addr);

                }
                mensaje.From = new MailAddress(prmDirreccioCorreoOrigen, prmNombreCorreo);
                mensaje.Subject = prmAsuntoCorreo;
                mensaje.Body = prmContenidoCorreo;
                mensaje.IsBodyHtml = true;
                SMTP.Host = "GVW1341EXA";
                SMTP.Port = 25;
                SMTP.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
                SMTP.EnableSsl = false;
                SMTP.Send(mensaje);

            }
            catch (SmtpException smtp)
            {
                correoError = true;
                objMensaje.Message("No se puedo enviar el correo", smtp.Message, BasePage.TipoMensaje.Error, false);

            }
            catch (Exception e)
            {
                correoError = true;
                objMensaje.Message("¡Error!, ", e.Message, BasePage.TipoMensaje.Error, false);

            }
            finally {
                mensaje = null;
                SMTP = null;
            }
            return correoError;
        }
        public MailAddressCollection buscarCorreosDetinatariosXConsultaIdUser(String[] prmNTs) {
            MailAddressCollection mailCorreo = new MailAddressCollection();
            String NTs = "";
            for (int i = 0; i < prmNTs.Length; i++) {
                if (prmNTs[i] != null) {
                    if (i == prmNTs.Length)
                    {
                        NTs = NTs + "'"+ prmNTs[i]+"'";
                    }
                    else {
                    }
                NTs=NTs+"'"+prmNTs[i]+"',";
                }
            }
            DataSet ds = new DataSet();
            String strSQL = "SELECT DISTINCT [E-mail] FROM WHERE NT IN (" + NTs + ")";
            ds = clsSQL.ejecutarProceConsulSQL(strSQL, ref returnError);
            if (ds.Tables[0].Rows.Count == 1) {
                foreach (DataRow dRow in ds.Tables[0].Rows) {
                    mailCorreo.Add(dRow["[E-mail]"].ToString());
                }
            }
          
            return mailCorreo;
        }
        public MailAddressCollection darDestinatarios(String[] prmDestinatarios) {
            MailAddressCollection mailAdress = new MailAddressCollection();
            try {
                for (int destinatario=0; destinatario < prmDestinatarios.Length; destinatario++) {
                    if (prmDestinatarios[destinatario] != null)
                    {
                        mailAdress.Add(prmDestinatarios[destinatario]);
                    }
                    
                }
            }
            catch (Exception e) {
                objMensaje.Message("¡Error!: ", e.Message, BasePage.TipoMensaje.Error, false);
                return null;
            }
            return mailAdress;
        }
    }
    
}