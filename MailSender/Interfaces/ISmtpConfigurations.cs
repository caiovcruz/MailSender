namespace MailSender.Configurations
{
    public interface ISmtpConfigurations
    {
        string Host { get; set; }
        int Port { get; set; }
        bool Ssl { get; set; }
        string User { get; set; }
        string Password { get; set; }
    }
}
