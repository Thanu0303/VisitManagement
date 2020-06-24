namespace Main.Models
{
    public class SmtpConfig
    {
        public string Host { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
        public int Port { get; set; }
    }
}

