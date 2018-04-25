using System;
using System.Net.Mail;

namespace EmailTester
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Send(
                "remetente@dominio.com",
                "destinatario@dominio.com",
                "copiaseparadaporpontoevirgula@dominio.com",
                "Titulo/Assunto do email",
                "Descrição do email",
                null //attachments em array de bytes
                );
        }

        /// <summary>
        /// Exemplo criado para envio de e-mail através do SMTP do office 365
        /// </summary>
        /// <param name="remetente">Email do remetente</param>
        /// <param name="destinatario">Email do destinatario</param>
        /// <param name="cc">Cópias do email separadas por ';'</param>
        /// <param name="assunto">Descrição do assunto do email</param>
        /// <param name="corpoEmail">Corpo do email</param>
        /// <param name="attachments">Anexos do e-mail</param>
        public static void Send(string remetente, string destinatario, string cc, string assunto, string corpoEmail, Attachment[] attachments)
        {
            try
            {
                //CLIENT CONFIG
                SmtpClient client = new SmtpClient()
                {
                    UseDefaultCredentials = false,
                    Host = "smtp.office365.com",
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new System.Net.NetworkCredential("meuemail@dominio.com", "senha")
                };

                // MESSAGE INFO
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(remetente),
                    Subject = assunto,
                    Body = corpoEmail
                };
                mail.To.Add(destinatario);

                string[] ccs = cc.Split(';');
                foreach (var emailCopy in ccs)
                {
                    if (!string.IsNullOrEmpty(emailCopy))
                    {
                        mail.CC.Add(new MailAddress(emailCopy));
                    }
                }

                // ATTACHMENTS
                if (attachments != null)
                {
                    foreach (Attachment file in attachments)
                    {
                        mail.Attachments.Add(file);
                    }
                }

                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}