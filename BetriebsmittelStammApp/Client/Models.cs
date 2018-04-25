using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HttpApiClient.Client
{
    /// <summary>
    /// Basisklasse aller Model-Klassen.
    /// </summary>
    public class BaseObject
    {
#pragma warning disable CS0169
        /// <summary>
        /// Dictionary, in dem beim Deserialisieren Feld-Werte abgespeichert werden, für die es keine entspechende
        /// Property im Model gibt. Ermöglicht ein versionierungstolerantes Serialisieren und Deserialisieren
        /// von Model-Objekten ohne Informationsverlust.
        /// </summary>
        [JsonExtensionData]
        IDictionary<string, JToken> _additionalData;
#pragma warning restore CS0169
    }

    /// <summary>
    /// Ein Speicherort (Ordner oder Datenbank).
    /// </summary>
    public class Speicherort : BaseObject
    {
        public Guid Id { get; set; }

        public string Bezeichnung { get; set; }

        /// <summary>
        /// Falls der Speicherort ein Ordner ist, enthält dieses Objekt die passenden Informationen.
        /// </summary>
        public OrdnerInfo OrdnerInfo { get; set; }

        /// <summary>
        /// Falls der Speicherort eine Datenbank ist, enthält dieses Objekt die passenden Informationen.
        /// </summary>
        public DatenbankInfo DatenbankInfo { get; set; }

        /// <summary>
        /// (Detailinfo) Liste von Projekten an diesem Speicherort.
        /// </summary>
        public List<ProjektInfo> ProjektInfos { get; set; }
    }

    public class OrdnerInfo : BaseObject
    {
        public string Pfad { get; set; }
        public string Arbeitsgruppenserver { get; set; }
    }

    public class DatenbankInfo : BaseObject
    {
        public string Server { get; set; }
        public string Datenbank { get; set; }
        public string Benutzername { get; set; }
        public string Passwort { get; set; }
        public bool IntegratedSecurity { get; set; }
    }

    /// <summary>
    /// Beschreibt ein Projekt. Im Gegensatz zu <see cref="Projekt"/> enthält dieser Typ nur ID,
    /// Nummer und Bezeichnung des Projekts und sonst keine Projektinhalte.
    /// </summary>
    public class ProjektInfo : BaseObject
    {
        public string Id { get; set; }
        public string Nummer { get; set; }
        public string Bezeichnung { get; set; }
    }

    /// <summary>
    /// Ein Projekt.
    /// </summary>
    public class Projekt : BaseObject
    {
        public string Id { get; set; }
        public string Nummer { get; set; }
        public string Bezeichnung { get; set; }
        public DateTime? Baubeginn { get; set; }
        public DateTime? Bauende { get; set; }
        public string Ampel { get; set; }
        public string Art { get; set; }
        public string Auschreibungsart { get; set; }
        public string Status { get; set; }
        public string Sparte { get; set; }
        public string Typ { get; set; }

        /// <summary>
        /// Liste von Leistungsverzeichnissen, die in diesem Projekt enthalten sind.
        /// </summary>
        public List<Leistungsverzeichnis> Leistungsverzeichnisse { get; set; }
    }

    public class NewProjektInfo : BaseObject
    {
        public string Nummer { get; set; }
        public string Bezeichnung { get; set; }
    }

    /// <summary>
    /// Ein Betriebsmittelstamm (auch Betriebsmittelkatalog genannt).
    /// </summary>
    public class BetriebsmittelStamm : BaseObject
    {
        public Guid Id { get; set; }
        public string Nummer { get; set; }
        public string Bezeichnung { get; set; }
        public string Beschreibung { get; set; }
        public BetriebsmittelStammArt? Art { get; set; }

        public int? RechengenauigkeitMengen { get; set; } // = NachkommastellenAnsatz
        public int? RechengenauigkeitBeträge { get; set; } // = NachkommastellenKostenPreise
        public int? DarstellungsgenauigkeitMengen { get; set; } // = NachkommastellenAnsatzUI
        public int? DarstellungsgenauigkeitBeträge { get; set; } // = NachkommastellenKostenPreiseUI

        public Guid LohnRootGruppeId { get; set; }
        public Guid MaterialRootGruppeId { get; set; }
        public Guid GerätRootGruppeId { get; set; }
        public Guid SonstigeKostenRootGruppeId { get; set; }
        public Guid NachunternehmerRootGruppeId { get; set; }
        public Guid BausteinRootGruppeId { get; set; }

        /// <summary>
        /// (Detailinfo) Enthält Kostenanteilbezeichnungen.
        /// </summary>
        public BetriebsmittelStammBezeichnungen Bezeichnungen { get; set; }

        /// <summary>
        /// (Detailinfo) Liste von Kostenkatalogen.
        /// </summary>
        public List<Kostenkatalog> Kostenkataloge { get; set; }

        /// <summary>
        /// (Detailinfo) Liste von Zuschlagskatalogen.
        /// </summary>
        public List<Zuschlagskatalog> Zuschlagskataloge { get; set; }

        /// <summary>
        /// (Detailinfo) Liste von Zuschlagsgruppen. Legt fest, welche Zuschläge zur Verfügung stehen.
        /// </summary>
        public List<Zuschlagsgruppe> Zuschlagsgruppen { get; set; }

        /// <summary>
        /// (Detailinfo) Liste von Zuschlagsarten (entspricht dem Reiter "Zuschläge" in Build).
        /// Über dieses Feld wird bestimmt, wie viele Zuschlagsspalten in den Kosten- und Zuschlagskatalogen angeboten werden und wie diese heißen.
        /// </summary>
        public List<Zuschlagsart> Zuschlagsarten { get; set; }

        /// <summary>
        /// (Detailinfo) Liste von Gerätefaktoren.
        /// </summary>
        public List<Gerätefaktor> Gerätefaktoren { get; set; }

        /// <summary>
        /// (Detailinfo) Liste von globalen Variablen.
        /// </summary>
        public List<GlobaleVariable> GlobaleVariablen { get; set; }

        /// <summary>
        /// (Detailinfo) Liste von Warengruppen.
        /// </summary>
        public List<Warengruppe> Warengruppen { get; set; }

        /// <summary>
        /// (Detailinfo) Liste von DbBetriebsmittelGruppen.
        /// </summary>
        public List<DbBetriebsmittelGruppe> DbBetriebsmittelGruppen { get; set; }

        /// <summary>
        /// (Detailinfo) Die Einträge im Grid "Zuschlagsberechnung" (nur GAEB-Stämme).
        /// </summary>
        public List<ZuschlagsartGruppe> ZuschlagsartGruppen { get; set; }
    }

    public enum BetriebsmittelStammArt
    {
        FreieForm,
        Aut,
        Ger
    }

    /// <summary>
    /// Enthält die Kostenanteilbezeichnungen.
    /// </summary>
    public class BetriebsmittelStammBezeichnungen : BaseObject
    {
        public string LohnKostenanteil1 { get; set; }
        public string LohnKostenanteil2 { get; set; }

        public string SonstigeKostenKostenanteil1 { get; set; }
        public string SonstigeKostenKostenanteil2 { get; set; }
        public string SonstigeKostenKostenanteil3 { get; set; }
        public string SonstigeKostenKostenanteil4 { get; set; }
        public string SonstigeKostenKostenanteil5 { get; set; }
        public string SonstigeKostenKostenanteil6 { get; set; }

        public string NachunternehmerKostenanteil1 { get; set; }
        public string NachunternehmerKostenanteil2 { get; set; }
        public string NachunternehmerKostenanteil3 { get; set; }
        public string NachunternehmerKostenanteil4 { get; set; }
        public string NachunternehmerKostenanteil5 { get; set; }
        public string NachunternehmerKostenanteil6 { get; set; }
    }

    /// <summary>
    /// Informationen zu einem neu zu erzeugenden Betriebsmittelstamm.
    /// </summary>
    public class NewBetriebsmittelStammInfo : BaseObject
    {
        public string Nummer { get; set; }
        public string Bezeichnung { get; set; }
        public BetriebsmittelStammArt? Art { get; set; }
    }

    /// <summary>
    /// Eine Kostenart. Kann auch eine Kostenartengruppe sein.
    /// </summary>
    public class Kostenart : BaseObject
    {
        /// <summary>
        /// Die Bezeichung der Kostenart.
        /// </summary>
        public string Bezeichnung { get; set; }

        /// <summary>
        /// Die Nummer der Kostenart innerhalb der enthaltenden Gruppe (z.B. "2").
        /// </summary>
        public string NummerLokal { get; set; }

        /// <summary>
        /// Die vollständige Nummer (z.B. "3.1.2").
        /// </summary>
        public string Nummer { get; set; }

        /// <summary>
        /// Falls true, ist dies eine Kostenartenguppe.
        /// </summary>
        public bool IstGruppe { get; set; }

        /// <summary>
        /// Befüllt im Fall IstGruppe == true. Enthält die Child-Kostenarten.
        /// </summary>
        public List<Kostenart> Kostenarten { get; set; }
    }

    /// <summary>
    /// Informationen zu einer neu zu erzeugenden Kostenart.
    /// </summary>
    public class NewKostenartInfo
    {
        public string Bezeichnung { get; set; }
        public string Nummer { get; set; }
        public bool IstGruppe { get; set; }
    }

    public enum GeräteArt
    {
        Vorhaltegerät,
        Leistungsgerät,
        Investitionsgerät,
        Listenpreisgerät
    }

    public class Gerätefaktor : BaseObject
    {
        public string Nummer { get; set; }
        public GeräteArt? Art { get; set; }
        public string Bezeichnung { get; set; }
        public decimal? AbminderungsfaktorAV { get; set; }
        public decimal? AbminderungsfaktorRepLohn { get; set; }
        public decimal? AbminderungsfaktorRepMaterial { get; set; }
        public decimal? StundenProMonat { get; set; }
    }

    public class GlobaleVariable : BaseObject
    {
        public string Variable { get; set; }
        public bool? IstKalkulationsVariable { get; set; }
        public string Ansatz { get; set; }
    }

    public class Warengruppe : BaseObject
    {
        public int Nummer { get; set; }
        public string Bezeichnung { get; set; }
    }

    public class NewKostenkatalogInfo : BaseObject
    {
        public string Nummer { get; set; }
        public string Bezeichnung { get; set; }
    }

    public class NewZuschlagskatalogInfo : BaseObject
    {
        public string Nummer { get; set; }
        public string Bezeichnung { get; set; }
    }

    public class NewZuschlagsartInfo : BaseObject
    {
        public int Index { get; set; }
        public string Bezeichnung { get; set; }
        public ZuschlagsTyp? Zuschlagstyp { get; set; }
    }

    public class DbBetriebsmittelGruppe
    {
        public string Bezeichnung { get; set; }
        public BetriebsmittelArt? Art { get; set; }
    }

    public class ZuschlagsartGruppe : BaseObject
    {
        public ZuschlagsTyp? Art { get; set; }
        public ZuschlagsBasis? Berechnung { get; set; }
        public bool HatZwischensumme { get; set; }
    }

    public enum ZuschlagsBasis
    {
        AufHundert,
        ImHundert,
    }

    public class Zuschlagsgruppe : BaseObject
    {
        public string Nummer { get; set; }
        public string Bezeichnung { get; set; }
        public int? Stufe { get; set; }
    }

    public class NewZuschlagsgruppeInfo : BaseObject
    {
        public string Nummer { get; set; }
        public string Bezeichnung { get; set; }
        public int? Stufe { get; set; }
    }

    public class ZuschlagsgruppenWert : BaseObject
    {
        public string ZuschlagsgruppenNummer { get; set; }
        public decimal? Wert { get; set; }
    }

    /// <summary>
    /// Eine Zuschlagsart. Beschreibt eine Zuschlagsspalte in den Kosten- und Zuschlagskatalogen.
    /// </summary>
    public class Zuschlagsart : BaseObject
    {
        public int Index { get; set; }
        public string Bezeichnung { get; set; }
        public ZuschlagsTyp? Typ { get; set; }
    }

    public enum ZuschlagsTyp
    {
        Agk, Bgk, Gewinn, Wagnis
    }

    public class Kostenkatalog : BaseObject
    {
        public Guid Id { get; set; }
        public string Nummer { get; set; }
        public string Bezeichnung { get; set; }
        public string Beschreibung { get; set; }
        public bool IstStandard { get; set; }

        public Guid? ParentKostenkatalogId { get; set; }
    }

    public class Zuschlagskatalog : BaseObject
    {
        public Guid Id { get; set; }
        public string Nummer { get; set; }
        public string Bezeichnung { get; set; }
        public string Beschreibung { get; set; }
        public bool IstStandard { get; set; }

        public List<ZuschlagsgruppenWert> ZuschlagsgruppenWerte { get; set; }
    }

    public enum BetriebsmittelArt
    {
        Lohn,
        Material,
        Gerät,
        SonstigeKosten,
        Nachunternehmer,
        Baustein,

        LohnGruppe,
        MaterialGruppe,
        GerätGruppe,
        SonstigeKostenGruppe,
        NachunternehmerGruppe,
        BausteinGruppe
    }

    /// <summary>
    /// Informationen zu einem neu zu erzeugenden Betriebsmittel.
    /// </summary>
    public class NewBetriebsmittelInfo : BaseObject
    {
        /// <summary>
        /// Die ID der Betriebsmittelgruppe, unter dem das neue Betriebsmittel angelegt wird.
        /// Falls nicht befüllt, wird das neue Betriebsmittel unter der Root-Gruppe angelegt.
        /// </summary>
        public Guid? ParentGruppeId { get; set; }

        /// <summary>
        /// Die Art des zu erzeugenden Betriebsmittels. Kann eine Gruppe sein.
        /// </summary>
        public BetriebsmittelArt Art { get; set; }

        public string Nummer { get; set; }

        public string Bezeichnung { get; set; }
    }

    /// <summary>
    /// Ein Betriebsmittel (kann auch eine Betriebsmittelgruppe sein).
    /// </summary>
    public class Betriebsmittel : BaseObject
    {
        public Guid Id { get; set; }

        public BetriebsmittelArt Art { get; set; }

        public string Nummer { get; set; }

        public string NummerKomplett { get; set; }

        public string Bezeichnung { get; set; }


        public bool? Leistungsfähig { get; set; }

        public string Einheit { get; set; }

        public string Kostenart { get; set; }

        /// <summary>
        /// Liste von Kosten (eine pro Kostenebene, auf der die Kosten für dieses Betriebsmittel definiert sind). Ist normalerweise eine Detailinfo,
        /// das heißt, dieses Feld ist nur im Fall von Einzelabfragen befüllt. Allerdings erlaubt der Aufruf /build/global/betriebsmittelstaemme/{betriebsmittelStammId}/betriebsmittel
        /// über den "mitKosten"-Parameter das Auslesen meherer Betriebsmittel einschließlich Kosten.
        /// </summary>
        public List<BetriebsmittelKosten> Kosten { get; set; }

        /// <summary>
        /// (Detailinfo) Liste mit weiteren Kosten.
        /// </summary>
        public List<KalkulationsZeile> WeitereKosten { get; set; }

        public List<BetriebsmittelZuschlag> Zuschläge { get; set; }

        /// <summary>
        /// (Detailinfo) Spezielle Eigenschaften, die nur bei Einzelabfragen geladen werden.
        /// </summary>
        public BetriebsmittelDetails Details { get; set; }

        /// <summary>
        /// Falls das Betriebsmittel eine Gruppe ist, enthält dieses Objekt die passenden Eigenschaften.
        /// </summary>
        public BetriebsmittelGruppeDetails GruppeDetails { get; set; }

        /// <summary>
        /// Falls das Betriebsmittel ein Lohn ist, enthält dieses Objekt die passenden Eigenschaften.
        /// </summary>
        public BetriebsmittelLohnDetails LohnDetails { get; set; }

        /// <summary>
        /// Falls das Betriebsmittel ein Material ist, enthält dieses Objekt die passenden Eigenschaften.
        /// </summary>
        public BetriebsmittelMaterialDetails MaterialDetails { get; set; }

        /// <summary>
        /// Falls das Betriebsmittel eine Gerät ist, enthält dieses Objekt die passenden Eigenschaften.
        /// </summary>
        public BetriebsmittelGerätDetails GerätDetails { get; set; }

        /// <summary>
        /// Falls das Betriebsmittel ein Sonstige-Kosten-Objekt ist, enthält dieses Objekt die passenden Eigenschaften.
        /// </summary>
        public BetriebsmittelSonstigeKostenDetails SonstigeKostenDetails { get; set; }

        /// <summary>
        /// Falls das Betriebsmittel ein Nachunternehmer ist, enthält dieses Objekt die passenden Eigenschaften.
        /// </summary>
        public BetriebsmittelNachunternehmerDetails NachunternehmerDetails { get; set; }

        /// <summary>
        /// Falls das Betriebsmittel ein Baustein ist, enthält dieses Objekt die passenden Eigenschaften.
        /// </summary>
        public BetriebsmittelBausteinDetails BausteinDetails { get; set; }
    }

    public class BetriebsmittelDetails : BaseObject
    {
        /// <summary>
        /// Die Beschreibung als Text (ohne Formatierungen).
        /// </summary>
        public string Beschreibung { get; set; }

        public string Stichwörter { get; set; }

        public string Markierungskennzeichen { get; set; }

        public string StandardAnsatz { get; set; }

        public string DbBetriebsmittelGruppeBezeichnung; // = DBBetriebsmittelgruppe

        public string KostenartNummer { get; set; }
    }

    public class BetriebsmittelZuschlag : BaseObject
    {
        public string ZuschlagsgruppenNummer { get; set; }
        public int ArtIndex { get; set; }
        public Guid ZuschlagsebeneId { get; set; }
    }

    public class BetriebsmittelGruppeDetails : BaseObject
    {
        /// <summary>
        /// Die in dieser Gruppe enthaltenen Child-Betriebsmittel.
        /// </summary>
        public List<Betriebsmittel> BetriebsmittelList { get; set; }
    }

    public class BetriebsmittelLohnDetails : BaseObject
    {
        public bool? IstProduktiv { get; set; }
        public decimal? EinheitenFaktor { get; set; } // = Umrechnung auf Stunden(1/x)
        public string BasNummer { get; set; } // Nummer des Bauarbeitsschlüssels
    }

    public class BetriebsmittelMaterialDetails : BaseObject
    {
        public decimal? Ladezeit { get; set; }
    }

    public class BetriebsmittelGerätDetails : BaseObject
    {
        public BglArt? BasisBgl { get; set; } // = BGLArt

        public string GerätefaktorNummer { get; set; }

        public string KostenartAV { get; set; }
        public string KostenartRepLohn { get; set; }
        public string KostenartRepMaterial { get; set; }

        /// <summary>
        /// (Detailinfo) Enthält zusätzliche Geräte-Eigenschaften.
        /// </summary>
        public BetriebsmittelGerätDetailsSonstiges Sonstiges { get; set; }
    }

    public enum Antriebsart
    {
        Elektro,
        Diesel,
        Gas,
        Benzin
    }

    public class BetriebsmittelGerätDetailsSonstiges : BaseObject
    {
        public string BglNummer { get; set; } // = BGLNummer
        public string EdvKurztext { get; set; } // = BGLBezeichnung

        // Sonstiges
        public decimal? Vorhaltemenge { get; set; }
        public decimal? Vorhaltedauer { get; set; }
        public Antriebsart? Antriebsart1 { get; set; } // = AntriebsArt
        public Antriebsart? Antriebsart2 { get; set; }
        public decimal? Gewicht { get; set; }
        public decimal? TransportVolumen { get; set; }
        public decimal? Leistung1 { get; set; } // = GeraeteLeistung
        public decimal? Leistung2 { get; set; } // = GeraeteLeistung2

        // Bahnspezifische Angaben
        public string Fabrikat { get; set; }
        public int? Mindestradius { get; set; }
        public int? BeherrschbareSteigung { get; set; }
        public string Schallemissionspegel { get; set; }
        public int? Arbeitsgeschwindigkeit { get; set; }
        public int? GegenseitigeUeberhoehung { get; set; }
    }

    public class BetriebsmittelSonstigeKostenDetails : BaseObject
    {
        public string Kostenart1 { get; set; }
        public string Kostenart2 { get; set; }
        public string Kostenart3 { get; set; }
        public string Kostenart4 { get; set; }
        public string Kostenart5 { get; set; }
        public string Kostenart6 { get; set; }
    }

    public class BetriebsmittelNachunternehmerDetails : BaseObject
    {
        public string Kostenart1 { get; set; }
        public string Kostenart2 { get; set; }
        public string Kostenart3 { get; set; }
        public string Kostenart4 { get; set; }
        public string Kostenart5 { get; set; }
        public string Kostenart6 { get; set; }
    }

    public class BetriebsmittelBausteinDetails : BaseObject
    {
    }

    /// <summary>
    /// Enthält die Kosten eines Betriebsmittels.
    /// </summary>
    public class BetriebsmittelKosten : BaseObject
    {
        /// <summary>
        /// Die ID der Kostenebene, auf der die Kosten für das Betriebsmittel definiert sind (z.B. des Kostenkatalog).
        /// </summary>
        public Guid KostenebeneId { get; set; }

        /// <summary>
        /// Der Typ der Kostenebene (muss bei PUT-Operationen nicht angegeben werden).
        /// </summary>
        public KostenebeneTyp? KostenebeneTyp { get; set; }

        public BetriebsmittelKostenLohnDetails LohnDetails { get; set; }
        public BetriebsmittelKostenMaterialDetails MaterialDetails { get; set; }
        public BetriebsmittelKostenGerätDetails GerätDetails { get; set; }
        public BetriebsmittelKostenSonstigeKostenDetails SonstigeKostenDetails { get; set; }
        public BetriebsmittelKostenNachunternehmerDetails NachunternehmerDetails { get; set; }
    }

    public enum KostenebeneTyp
    {
        Kostenkatalog,
        Zuschlagskatalog,
        Kalkulation,
        Lv,
        Umlagegruppe,
        Projekt,
        Unterprojekt
    }

    public class BetriebsmittelKostenLohnDetails : BaseObject
    {
        public Money Kostenanteil1 { get; set; }
        public Money Kostenanteil2 { get; set; }

        public int? WarengruppeNummer { get; set; }
    }

    public class BetriebsmittelKostenMaterialDetails : BaseObject
    {
        public Money Listenpreis { get; set; }
        public decimal? Rabatt { get; set; }
        public decimal? Verlust { get; set; }
        public Money Manipulation { get; set; }
        public Money Transportkosten { get; set; }

        public int? WarengruppeNummer { get; set; }
    }

    public class BetriebsmittelKostenGerätDetails : BaseObject
    {
        public Money Neuwert { get; set; } // = Mittlerer Neuwert
        public Money Kostenanteil1 { get; set; }
        public Money Kostenanteil2 { get; set; }
        public decimal? AbschreibungVerzinsung { get; set; } // = A + V
        public decimal? Reparaturkosten { get; set; }
    }

    public class BetriebsmittelKostenSonstigeKostenDetails : BaseObject
    {
        public Money Kostenanteil1 { get; set; }
        public Money Kostenanteil2 { get; set; }
        public Money Kostenanteil3 { get; set; }
        public Money Kostenanteil4 { get; set; }
        public Money Kostenanteil5 { get; set; }
        public Money Kostenanteil6 { get; set; }

        public int? Warengruppe0Nummer { get; set; }
        public int? Warengruppe1Nummer { get; set; }
        public int? Warengruppe2Nummer { get; set; }
        public int? Warengruppe3Nummer { get; set; }
        public int? Warengruppe4Nummer { get; set; }
        public int? Warengruppe5Nummer { get; set; }
    }

    public class BetriebsmittelKostenNachunternehmerDetails : BaseObject
    {
        public Money Kostenanteil1 { get; set; }
        public Money Kostenanteil2 { get; set; }
        public Money Kostenanteil3 { get; set; }
        public Money Kostenanteil4 { get; set; }
        public Money Kostenanteil5 { get; set; }
        public Money Kostenanteil6 { get; set; }

        public int? Warengruppe0Nummer { get; set; }
        public int? Warengruppe1Nummer { get; set; }
        public int? Warengruppe2Nummer { get; set; }
        public int? Warengruppe3Nummer { get; set; }
        public int? Warengruppe4Nummer { get; set; }
        public int? Warengruppe5Nummer { get; set; }
    }

    /// <summary>
    /// Eine Zeile in den weiteren Kosten.
    /// </summary>
    public class KalkulationsZeile : BaseObject
    {
        public string Nummer { get; set; }
        public string Bezeichnung { get; set; }

        /// <summary>
        /// Befüllt, wenn die Zeile einen Verweis auf ein Betriebsmittel enthält.
        /// </summary>
        public KalkulationsZeileBetriebsmittelDetails BetriebsmittelDetails { get; set; }

        /// <summary>
        /// Befüllt, wenn die Zeile einen Variablenansatz enthält.
        /// </summary>
        public KalkulationsZeileVariablenDetails VariablenDetails { get; set; }

        /// <summary>
        /// Befüllt, wenn es sich um eine Kommentarzeile handelt.
        /// </summary>
        public KalkulationsZeileKommentarDetails KommentarDetails { get; set; }

        /// <summary>
        /// Befüllt, wenn es sich um eine Unterposition handelt. Diese kann mehrere Unterzeilen enthalten.
        /// </summary>
        public KalkulationsZeileUnterpositionDetails UnterpositionDetails { get; set; }
    }

    public class KalkulationsZeileBetriebsmittelDetails : BaseObject
    {
        public Guid BetriebsmittelId { get; set; }
        public BetriebsmittelArt? BetriebsmittelArt { get; set; } // aus Performancegründen speichern wir hier auch (optional) die Betriebsmittelart ab

        public string Ansatz { get; set; }
        public string BasNummer { get; set; }
    }

    public class KalkulationsZeileVariablenDetails : BaseObject
    {
        public string Variable { get; set; }
        public string Ansatz { get; set; }
    }

    public class KalkulationsZeileKommentarDetails : BaseObject
    {
        public string Kommentar { get; set; }
    }

    public class KalkulationsZeileUnterpositionDetails : BaseObject
    {
        public string Ansatz { get; set; }
        public string BasNummer { get; set; }

        /// <summary>
        /// Zeilen, die in dieser Zeile enthalten sind.
        /// </summary>
        public List<KalkulationsZeile> Zeilen { get; set; }
    }

    public enum BglArt
    {
        Keine,
        Oebgl,
        Dbgl
    }

    public enum Norm
    {
        Oenorm,
        Gaeb
    }

    /// <summary>
    /// Ein Leistungsverzeichnis (GAEB oder ÖNORM).
    /// </summary>
    public class Leistungsverzeichnis : BaseObject
    {
        public Guid Id { get; set; } // ist die ID der Box
        public string Nummer { get; set; }
        public string Bezeichnung { get; set; }
        public Norm? Norm { get; set; }
        public string Waehrung { get; set; }
        public LVKnoten RootKnoten { get; set; }
    }

    public class LVItemBase : BaseObject
    {
        public Guid Id { get; set; }
        public string Typ { get; set; }
        public string Nummer { get; set; }
        public string NummerKomplett { get; set; }
        public string Kurztext { get; set; }
        public string Teilleistungsnummer { get; set; }
        public decimal? Betrag { get; set; }
    }

    /// <summary>
    /// Ein LV-Knoten (z.B. Titel oder Leistungsgruppe).
    /// </summary>
    public class LVKnoten : LVItemBase
    {
        public List<LVKnoten> Knoten { get; set; }
        public List<LVPosition> Positionen { get; set; }
    }

    /// <summary>
    /// Eine LV-Position.
    /// </summary>
    public class LVPosition : LVItemBase
    {
        public string Einheit { get; set; }
        public decimal? Menge { get; set; }
    }

    /// <summary>
    /// Ein einfacher Geldbetrag (inklusive Währung).
    /// </summary>
    public class SimpleMoney
    {
        public SimpleMoney(string currency, decimal? value)
        {
            Currency = currency;
            Value = value;
        }

        [JsonProperty("cur")]
        public string Currency { get; private set; }

        [JsonProperty("val")]
        public decimal? Value { get; private set; }
    }

    /// <summary>
    /// Ein mehrfacher Geldbetrag (für unterschiedliche Währungen).
    /// </summary>
    public class Money : Collection<SimpleMoney>
    {
        public Money()
        {
        }

        public Money(IEnumerable<(string Code, decimal? Value)> beträge)
        {
            foreach (var betrag in beträge)
            {
                Add(new SimpleMoney(betrag.Code, betrag.Value));
            }
        }

        public void Add(string currency, decimal value)
        {
            Add(new SimpleMoney(currency, value));
        }

        public decimal? FirstValue
        {
            get => this.FirstOrDefault()?.Value;
            set => this[0] = new SimpleMoney(this[0].Currency, value);
        }
    }
}
