using System;
using Newtonsoft.Json;

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

        public void FormatJson(ref JsonWriter writer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("type");
            writer.WriteValue("text");
            writer.WritePropertyName("text");
            writer.WriteValue(this.InnerText);
            writer.WriteEndObject();
        }
    }
}
