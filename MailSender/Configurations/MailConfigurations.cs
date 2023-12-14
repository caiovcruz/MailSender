using System.Net.Mail;

namespace MailSender.Configurations
{
    public class MailConfigurations
    {
        public string Subject { get; set; }

        public string Body { get; set; }

        public bool IsHtml { get; set; }

        public MailAddress FromAddress { get; set; }

        public List<MailAddress> ToAddressList { get; set; }

        public List<MailAddress> CCAddressList { get; set; }

        public List<MailAddress> BCCAddressList { get; set; }

        public MailConfigurations()
        {
            ToAddressList = new List<MailAddress>();
            CCAddressList = new List<MailAddress>();
            BCCAddressList = new List<MailAddress>();
        }
    }
}
