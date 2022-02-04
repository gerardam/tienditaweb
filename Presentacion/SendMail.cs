using System;
using System.Configuration;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Configuration;

namespace VentasCal.Negocio
{
    public class SendMail : IDisposable
    {
        /// <sumary>
        /// Variable que indica si ya se liberarón los recursos
        /// </sumary>
        private bool disposed = false;

        /// <sumary>
        /// Método que contiene el correo que será enviado
        /// </sumary>
        private MailMessage correo;

        /// <sumary>
        /// Método que contiene la Configuración del Correo
        /// </sumary>
        private Configuration c;

        /// <sumary>
        /// Método que contiene el Grupo de Configuración del Correo
        /// </sumary>
        private MailSettingsSectionGroup settings;

        /// <sumary>
        /// Método que contiene el SMTP cliente
        /// </sumary>
        private SmtpClient smtp;

        /// <sumary>
        /// Método que contiene la Variable que almacena el error
        /// </sumary>
        private string _errorMessage;

        /// <sumary>
        /// Método que contiene la propiedad  que lee el valor de _errorMessage en caso de haber ocurrido
        /// </sumary>
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        /// <summary>
        /// Método para enviar un correo cuyo remitente es muestra la Empresa cliente
        /// </summary>
        /// <param name="to">Mail para</param>
        /// <param name="subject">Asunto del mail</param>
        /// <param name="message">Cuerpo del mail(mensaje)</param>
        /// <param name="isBodyHtml">el contenido es HTML?</param>
        /// <param name="priority">Enum MailPriority High, Low, Normal</param>
        /// <returns>Verdadero si el correo tuvo salida, Falso en caso contrario. El error puede ser leído en la propiedad ErrorMessage</returns>
        public bool Enviar(string nombreEmpresa, string to, string subject, string message, bool isBodyHtml, MailPriority priority, AlternateView AltView)
        {
            try
            {
                correo = new MailMessage();
                c = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
                settings = (MailSettingsSectionGroup)c.GetSectionGroup("system.net/mailSettings");
                correo.From = new System.Net.Mail.MailAddress(settings.Smtp.From, nombreEmpresa, System.Text.Encoding.UTF8);//Correo de quien?, toma el correo de la webconfig
                correo.To.Add(to);//Correo para quien?
                correo.Subject = subject;
                correo.Body = message;
                if (AltView != null)
                    correo.AlternateViews.Add(AltView);
                correo.IsBodyHtml = isBodyHtml;
                correo.Priority = priority;
                smtp = new SmtpClient();
                smtp.UseDefaultCredentials = settings.Smtp.Network.DefaultCredentials;
                smtp.Host = settings.Smtp.Network.Host;
                smtp.Port = settings.Smtp.Network.Port;
                smtp.Credentials = new NetworkCredential(settings.Smtp.From, settings.Smtp.Network.Password);//toma el correo de la webconfig
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.EnableSsl = settings.Smtp.Network.EnableSsl;

                smtp.Send(correo);
                _errorMessage = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    correo.Dispose();
                    smtp.Dispose();
                }
            }
            this.c = null;
            this.settings = null;
            this._errorMessage = null;
            this.disposed = true;
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~SendMail()
        {
            Dispose(false);
        }
    }
}
