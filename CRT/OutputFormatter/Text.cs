namespace CRT.OutputFormatter
{
    class Text : OutputInterface
    {
        private string text;
        public Text(string text)
        {
            this.text = text;
        }

        public string InnerText
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
            }
        }

        public string FormatConsole()
        {
            return this.InnerText;
        }
    }
}
