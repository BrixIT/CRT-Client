using System;
using Newtonsoft.Json;

namespace CRT.CommandOption
{
    class Checkbox : CommandOptionInterface
    {
        private bool isChecked = false;
        private string label = "";

        public bool IsChecked
        {
            get
            {
                return isChecked;
            }

            set
            {
                isChecked = value;
            }
        }

        public string Label
        {
            get
            {
                return label;
            }

            set
            {
                label = value;
            }
        }

        public string FormatConsole()
        {
            throw new NotImplementedException();
        }

        public void FormatJson(ref JsonWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
