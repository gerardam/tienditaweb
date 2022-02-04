using System;
using System.Net.Mail;

//| || //  (( |+|====================================================//
//| ||// ((   |+| Tienda V 1.0                                      //
//| ||\\ ((   |+| Kyocode | www.kyocode.com                         \\
//| || \\  (( |+|====================================================\\
namespace VentasCal.Negocio
{
    public static class Mail
    {
        #region ENVIAR MAIL EN NOTIFICACION GENERAL
        /// <summary>
        /// Enviar correos por una notificacion general.
        /// </summary>
        public static void Compra(string nombre, string depa)
        {
            SendMail mail = new SendMail();
            //string ContenidoCorreo = string.Format("Se ha solicitado una orden de compra de {0} del departamento de {1}, acceda al sitio para ver mas detalles.", nombre, depa);
            string ContenidoCorreo = string.Format("Se ha solicitado una orden de compra de {0}, acceda al sitio para ver mas detalles.", nombre);

            bool mailResultado = mail.Enviar("Tienda", "gerardam@outlook.com", "Alerta de compra", ContenidoCorreo, true, MailPriority.Normal, null);
        }
        #endregion
    }
}
