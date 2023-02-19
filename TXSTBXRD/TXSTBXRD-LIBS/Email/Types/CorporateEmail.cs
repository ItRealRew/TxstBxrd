namespace TXSTBXRD_LIBS.Email.Types
{
    public class CorporateEmail
    {
        private CorporateEmail(string smtpHost, int smtpPort, string userName, string password)
        {
            Host = smtpHost;
            Port = smtpPort;
            NameCorporateMail = userName;
            Password = password;
        }

        public string Host { get; private set; }
        public int Port { get; private set; }
        public string NameCorporateMail { get; private set; }
        public string Password { get; private set; }

        public static CorporateEmail NoReply
        {
            get
            {
                return new CorporateEmail("smtp.yandex.ru", 587, "txstbxrd.alert@yandex.ru", "*8V5fjI3f{#My1e56ztU");
            }
        }
    }
}