using ConsoleTables.Core;
using System.Collections.Generic;

namespace CRT.OutputFormatter
{
    class Table : OutputInterface
    {
        private List<object[]> rows = new List<object[]>();
        private List<string> headers = new List<string>();

        public void AddRow(object[] row)
        {
            this.rows.Add(row);
        }

        public void Clear()
        {
            this.rows.Clear();
        }

        public void SetHeaders(string[] headers)
        {
            this.headers = new List<string>(headers);
        }

        public string FormatConsole()
        {
            var table = new ConsoleTable();
            table.AddColumn(this.headers.ToArray());
            foreach (var row in this.rows)
            {
                table.AddRow(row);
            }
            return table.ToString();
        }
    }
}
