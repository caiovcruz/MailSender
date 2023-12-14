namespace MailSender.Configurations
{
    public class SmtpConfigurations : ISmtpConfigurations
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public bool Ssl { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        public SmtpConfigurations(string host, int port, bool ssl, string user, string password)
        {
            Host = host;
            Port = port;
            Ssl = ssl;
            User = user;
            Password = password;
        }
    }
}
