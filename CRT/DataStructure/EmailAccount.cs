namespace CRT.DataStructure
{
    class EmailAccount
    {
        public string accountSource;
        public string displayName;
        public string emailAddress;
    }
    class EmailAccountPOP3 : EmailAccount
    {
        public string incomingServer;
        public int incomingPort;
        public bool incomingSSL;
        public string username;
        public string password;
        public string outgoingServer;
        public int outgoingPort;
        public bool outgoingSSL;
        public bool outgoingAuthentication;
        public bool leaveMailOnServer;
    }
    class EmailAccountIMAP : EmailAccount
    {
        public string incomingServer;
        public int incomingPort;
        public bool incomingSSL;
        public string username;
        public string password;
        public string outgoingServer;
        public int outgoingPort;
        public bool outgoingSSL;
        public bool outgoingAuthentication;
        public string imapRoot;
    }
}
