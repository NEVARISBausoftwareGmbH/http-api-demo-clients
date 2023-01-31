using Nevaris.Build.ClientApi;

namespace Lv_Viewer
{
    public class LvNode : LvItem
    {
        public LvNode(LvKnoten? lvKnoten)
            : base(lvKnoten)
        {
            Betrag = lvKnoten?.Ergebnisse?.Betrag?.FirstValue;
        }

        public decimal? Betrag { get; set; }
    }
}
