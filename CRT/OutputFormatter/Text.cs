using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
