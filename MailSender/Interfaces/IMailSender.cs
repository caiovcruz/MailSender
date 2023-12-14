using System.Net.Mail;

namespace MailSender.Configurations
{
    public interface IMailSender
    {
        Task<bool> SendAsync(MailConfigurations emailConfigurations, IList<Attachment> attachments = null);
    }
}
