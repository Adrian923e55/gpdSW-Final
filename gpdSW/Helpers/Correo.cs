using System.Net.Mail;
using System.Net;

namespace gpdSW.Helpers
{
    public class Correo
    {
        private static string remitente = "charcuterialedezma@gmail.com";
        private static string contraseñaApp = "inni pqfw nnqz jjsu";

        public static void EnviarCorreo(string destinatario, string asunto, string mensaje)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(remitente, contraseñaApp),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(remitente),
                Subject = asunto,
                Body = mensaje,
                IsBodyHtml = true
            };

            mailMessage.To.Add(destinatario);

            client.Send(mailMessage);
        }

        //// Manejar correos de contacto de clientes
        //public void EnviarCorreoDeContacto(string correoCliente, string asunto, string mensajeCliente)
        //{
        //    string mensaje = $"Mensaje de {correoCliente}:\n\n{mensajeCliente}";
        //    EnviarCorreo("charcuterialedezma@gmail.com", asunto, mensaje);
        //}
    }
}
