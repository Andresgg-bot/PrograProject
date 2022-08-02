using Microsoft.Extensions.Options;
using Proyecto_Progra_MVC.Contracts;
using Proyecto_Progra_MVC.Domain.Models.ConfigurationModels;
using Proyecto_Progra_MVC.Domain.Models.PlainModels;
using System.Net;
using System.Net.Mail;

namespace Proyecto_Progra_MVC.Components
{
    public class Cartero : ICartero
    {
        public Cartero(IOptions<ConfiguracionSmtp> configuracion)
        {
            Configuracion = configuracion.Value;
        }

        ConfiguracionSmtp Configuracion;

        public void Enviar(CorreoElectronico correo)
        {
            var cliente =
                new SmtpClient
                {
                    Host = Configuracion.Servidor,
                    Port = Configuracion.Puerto,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials =
                        new NetworkCredential(Configuracion.Usuario, Configuracion.Contrasena)
                };

            var mensaje =
                new MailMessage
                {
                    From = new MailAddress(Configuracion.Remitente),
                    Subject = correo.Asunto,
                    Body = correo.Cuerpo,
                    IsBodyHtml = false
                };

            mensaje.To.Add(new MailAddress(correo.Destinatario));
            cliente.Send(mensaje);
        }
    }
}
