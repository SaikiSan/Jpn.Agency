using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using JPN.EmailService.Extension;
using JPN.EmailService.Model;
using Microsoft.Extensions.Options;

namespace JPN.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _mailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _mailSettings = emailSettings.Value;
        }

        public async Task<bool> SendEmailAsync(EmailModel email)
        {
            var message = new MailMessage();
            var smtp = new SmtpClient();

            message.From = new MailAddress(_mailSettings.Email, _mailSettings.Nome);
            message.To.Add(new MailAddress(email.Destinatario));
            message.Subject = email.Assunto;

            if (email.Anexo != null)
                using (var ms = new MemoryStream())
                {
                    email.Anexo.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    var att = new Attachment(new MemoryStream(fileBytes), email.Anexo.FileName);
                    message.Attachments.Add(att);
                }
            message.IsBodyHtml = false;
            message.Body = email.Conteudo;
            smtp.Port = _mailSettings.Porta;
            smtp.Host = _mailSettings.Host;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(_mailSettings.Email, _mailSettings.Senha);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                await smtp.SendMailAsync(message);
                return true;
            }
            catch (Exception e)
            {
                if (e == null)
                {
                }

                return false;
            }
        }
    }
}
