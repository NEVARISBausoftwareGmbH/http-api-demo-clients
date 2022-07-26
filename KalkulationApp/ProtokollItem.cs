using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalkulationApp
{
    public class ProtokollItem
    {
        public ProtokollItem(string? nummer, string? bezeichnung, string skWert)
        {
            Nummer = nummer;
            Bezeichnung = bezeichnung;
            SkWert = skWert;
        }

        public string? Nummer { get; set; }
        public string? Bezeichnung { get; set; }
        public string SkWert { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - Wert sk-Variable: {1} - {2}", Nummer, SkWert, Bezeichnung);
        }
    }

    public static class ProtokollLogger
    {
        internal static void WriterProtokoll(List<ProtokollItem> protokollItems, string projekt)
        {
            try
            {
                var log = new StringBuilder($"Protokoll - KalkulationsApp: {DateTime.Now}").AppendLine();
                log.AppendLine($"Projekt: {projekt}");
                log.AppendLine($"Geänderte sk-Variablen Anzahl: {protokollItems.Count}");
                protokollItems.ForEach((i) => log.AppendLine(i.ToString()));
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
