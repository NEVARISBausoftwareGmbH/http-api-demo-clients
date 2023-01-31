using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace KalkulationApp
{
    public class ProtokollItem
    {
        public ProtokollItem(string? nummer, string? bezeichnung, string? skWert, bool hasEigenleistung)
        {
            Nummer = nummer;
            Bezeichnung = bezeichnung;
            SkWert = skWert;

            if (!hasEigenleistung)
            {
                Hinweis = "Keine Eigenleistung vorhanden";
            }

            if (skWert == null)
            {
                Hinweis = "sk-Variable wurde nicht aktualisiert";
            }
        }
              
        public string? Nummer { get; set; }
        public string? Bezeichnung { get; set; }
        public string? SkWert { get; set; }
        public string? Hinweis { get; private set; }
    }

    public static class ProtokollLogger
    {
        private static int tableWidth = 150;
        private static int columnsCount = 4;
        private static string[] columns = new string[] { "Nummer", "sk-Variable", "Bezeichnung", "Hinweis" };

        static string AddRow(params string[] columns)
        {
            int width = (tableWidth - columnsCount) / columnsCount;
            string row = "|";
            List<string> cols = columns.ToList();
            foreach (string column in cols)
            {
                int idx = cols.IndexOf(column);
                bool alignLeft = true;
                if (idx == 0) { width = 15; }
                else if (idx == 1) { width = 15; alignLeft = false; }
                else if (idx == 2) { width = 70; }
                else if (idx == 3) { width = 50; } 
                
                row += AlignColumn(column, width, alignLeft) + "|";
            }

            return row;
        }

        static string AlignColumn(string text, int width, bool alignLeft = true)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else if (alignLeft)
            {
                return text.PadRight(width);
            }
            else
            {
                return text.PadLeft(width);
            }
        }

        internal static void WriterProtokoll(List<ProtokollItem> protokollItems, string projekt)
        {
            try
            {
                var log = new StringBuilder($"Protokoll - KalkulationsApp: {DateTime.Now}").AppendLine();
                log.AppendLine($"Projekt: {projekt}");
                log.AppendLine($"Geänderte sk-Variablen Anzahl: {protokollItems.Count}").AppendLine();

                var row = AddRow(columns);
                log.AppendLine(row).AppendLine(AddRow(""," ","  ","   "));

                foreach (var item in protokollItems)
                {
                    row = AddRow(item.Nummer ?? "", item.SkWert ?? "", item.Bezeichnung ?? "", item.Hinweis ?? "");
                    log.AppendLine(row);
                }
                
                string protokollPath = Path.Combine(Path.GetTempPath(), "KalkulationsApp_Protokoll.txt");
                using (StreamWriter writer = new(protokollPath, false, Encoding.UTF8))
                {
                    writer.Write(log);
                }

                Process.Start("explorer.exe", protokollPath);
            }
            catch (Exception)
            {
            }
        }
    }
}
