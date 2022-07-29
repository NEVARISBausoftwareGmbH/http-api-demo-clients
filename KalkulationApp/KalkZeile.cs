using Nevaris.Build.ClientApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalkulationApp
{
    public class KalkZeile : NotifyPropertyChangedBase
    {
        private KalkulationsZeile _zeile;
        private List<KalkulationsZeile>? _zeilenUp;

        public KalkZeile(KalkulationsZeile zeile)
        {
            _zeile = zeile;            
        }

        public void Generatezeile()
        {
            EH = _zeile.Einheit;
            Bezeichnung = _zeile.Bezeichnung;
            Kommentar = _zeile.Kommentar;            
            Inaktiv = _zeile.IstInaktiv;
            Nummer = _zeile.Nummer;

            VariableDetails = _zeile.VariablenDetails;
            BetriebsmittelDetails = _zeile.BetriebsmittelDetails;
            KalkzeileDetails = _zeile.Details;
            RückgriffZeileDetails = _zeile.RückgriffDetails;
            SummeDetails = _zeile.SummenDetails;
            UpDetails = _zeile.UnterpositionDetails;

            ErmittleDetails();
        }

        private void ErmittleDetails()
        {
            if (KalkzeileDetails != null)
            {
                Kosten = KalkzeileDetails.Kosten?.FirstValue;
                Preis = KalkzeileDetails.Preis?.FirstValue;
                Ergebnis = KalkzeileDetails.Ergebnis;
                ProduktiveStunden = KalkzeileDetails.StundenProduktiv;
                Gesamtmenge = KalkzeileDetails.MengeGesamt;
                KostenJeEH = KalkzeileDetails.KostenProEinheit?.FirstValue;
            }
            
            if (BetriebsmittelDetails != null)
            {
                Ansatz = BetriebsmittelDetails.Ansatz;
                Variable = BetriebsmittelDetails.Variable;
                BAS = BetriebsmittelDetails.BasNummer;

                var betriebsmittel =
                    Kalkblatt?.GetBetriebsmittel(
                        BetriebsmittelDetails.BetriebsmittelId);

                if (betriebsmittel != null)
                {
                    Nummer = betriebsmittel.NummerKomplett;
                    Bezeichnung = betriebsmittel.Bezeichnung;                    
                }
            }
            
            if (UpDetails != null)
            {
                Ansatz = UpDetails.Ansatz;
                Variable = UpDetails.Variable;
                _zeilenUp = UpDetails.Zeilen;
                BAS = UpDetails.BasNummer;
                IsUpZeile = true;
                foreach (var upZeile in UpDetails.Zeilen)
                {
                    var newKalkZeile = new KalkZeile(upZeile)
                    {
                        Kalkblatt = Kalkblatt,
                        Parent = this,                        
                    };

                    if (Childs == null) Childs = new List<KalkZeile>();
                    Childs.Add(newKalkZeile);
                    Kalkblatt?.Zeilen.Add(newKalkZeile);
                    newKalkZeile.Generatezeile();

                    //newKalkZeile.Nummer = "  " + Nummer;
                }
            }
            
            if (VariableDetails != null)
            {
                Ansatz = VariableDetails.Ansatz;
                Variable = VariableDetails.Variable;                
            }
            
            if (SummeDetails != null)
            {
                Kosten = SummeDetails.Kosten?.FirstValue;
                ProduktiveStunden = SummeDetails.StundenProduktiv;
                Preis = SummeDetails.Preis?.FirstValue;
                Nummer = SummeDetails.Art == SummenKalkulationsZeileArt.Relativ ? "T" : "Z";                
            }
            
            if (RückgriffZeileDetails != null)
            {
                Ansatz = RückgriffZeileDetails.Ansatz;
                Variable = RückgriffZeileDetails.Variable;
            }

            if (_zeile.KommentarDetails != null)
            {
                Bezeichnung = _zeile.Kommentar;
            }

            if (Parent != null && Nummer != null)
            {
                string einzugNummer = "  " + Nummer;
                if (Parent.Nummer != null)
                {
                    einzugNummer = Nummer.PadLeft(Nummer.Length + GetIndent(Parent.Nummer));
                }
                
                Nummer = einzugNummer;
            }
        }

        private int GetIndent(string nummer)
        {
            switch(nummer.Split(".").Length)
            {
                case 1:
                    return 2;
                case 2:
                    return 5;
                case 3:
                    return 7;
                case 4:
                    return 9;
            }

            return 1;   
        }

        public bool Inaktiv { get; set; }
        public string? EH { get; set; }
        public decimal? KostenJeEH { get; set; }
        public string? Bezeichnung { get; set; }
        public string? Kommentar { get; set; }
        public string? Nummer { get; set; }
        public string? Ansatz { get; set; }
        public string? Variable { get; set; }
        public string? BAS { get; set; }
        public decimal? Kosten { get; set; }
        public decimal? Ergebnis { get; set; }
        public decimal? Gesamtmenge { get; set; }
        public decimal? Preis { get; set; }
        public decimal? ProduktiveStunden { get; set; }
        public string? Bieter { get; set; }
        public string? Markierungskennzeichen { get; set; }       

        KalkulationsZeileBetriebsmittelDetails? BetriebsmittelDetails { get; set; }
        KalkulationsZeileVariablenDetails? VariableDetails { get; set; }
        KalkulationsZeileDetails? KalkzeileDetails { get; set; }
        KalkulationsZeileUnterpositionDetails? UpDetails { get; set; }
        SummenKalkulationsZeileDetails? SummeDetails { get; set; }
        RückgriffZeileDetails? RückgriffZeileDetails { get; set; }
        
        public Kalkulationsblatt? Kalkblatt { get; internal set; }
        public List<KalkZeile> Childs { get; set; } = new();
        public KalkZeile? Parent { get; set; }
        public bool IsUpZeile { get; set; }
    }
}
