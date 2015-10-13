using ConsoleTables.Core;
using System.Collections.Generic;
using Newtonsoft.Json;

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

        public void FormatJson(ref JsonWriter writer)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("type");
            writer.WriteValue("table");

            writer.WritePropertyName("headers");
            writer.WriteStartArray();
            foreach(var header in this.headers.ToArray())
            {
                writer.WriteValue(header);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("rows");
            writer.WriteStartArray();
            foreach (var row in this.rows.ToArray())
            {
                writer.WriteStartArray();

                foreach( var field in row)
                {
                    writer.WriteValue(field);
                }

                writer.WriteEndArray();
            }
            writer.WriteEndArray();

            writer.WriteEndObject();
        }
    }
}
