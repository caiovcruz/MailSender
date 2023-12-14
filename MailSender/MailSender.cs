using MailSender.Configurations;
using System.Net;
using System.Net.Mail;

namespace MailSender
{
    public class MailSender : IMailSender
    {
        private ISmtpConfigurations _smtpConfigurations { get; }

        public MailSender(ISmtpConfigurations smtpConfigurations)
        {
            _smtpConfigurations = smtpConfigurations;
        }

        public async Task<bool> SendAsync(MailConfigurations mailConfigurations, IList<Attachment> attachments = null)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();

                if (mailConfigurations.FromAddress == null)
                {
                    throw new ArgumentNullException("mailConfigurations.FromAddress");
                }

                mailMessage.From = mailConfigurations.FromAddress;

                mailConfigurations.ToAddressList?.ForEach(mailMessage.To.Add);
                mailConfigurations.CCAddressList?.ForEach(mailMessage.To.Add);
                mailConfigurations.BCCAddressList?.ForEach(mailMessage.To.Add);

                mailMessage.Subject = mailConfigurations.Subject;
                mailMessage.Body = mailConfigurations.Body;
                mailMessage.IsBodyHtml = mailConfigurations.IsHtml;

                using (var smtp = new SmtpClient())
                {
                    if (!string.IsNullOrEmpty(_smtpConfigurations.User))
                    {
                        smtp.Host = _smtpConfigurations.Host;
                        smtp.Port = _smtpConfigurations.Port;
                        smtp.EnableSsl = _smtpConfigurations.Ssl;
                    }
                    else
                    {
                        smtp.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
                    }

                    if (!string.IsNullOrEmpty(_smtpConfigurations.User))
                    {
                        smtp.Credentials = new NetworkCredential(_smtpConfigurations.User, _smtpConfigurations.Password);
                    }

                    if (attachments != null)
                    {
                        foreach (var attachment in attachments)
                        {
                            mailMessage.Attachments.Add(attachment);
                        }
                    }

                    await smtp.SendMailAsync(mailMessage);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
