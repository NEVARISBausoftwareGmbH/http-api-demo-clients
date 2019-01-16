using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nevaris.Build.ClientApi
{
    /// <summary>
    /// Basisklasse aller Model-Klassen.
    /// </summary>
    public class BaseObject
    {
        /// <summary>
        /// Dictionary, in dem beim Deserialisieren Feld-Werte abgespeichert werden, für die es keine entspechende
        /// Property im Model gibt. Ermöglicht ein versionierungstolerantes Serialisieren und Deserialisieren
        /// von Model-Objekten ohne Informationsverlust.
        /// </summary>
        [JsonExtensionData]
        public Dictionary<string, object> AdditionalProperties { get; set; }
    }

    public class VersionInfo : BaseObject
    {
        /// <summary>
        /// Die NEVARIS Build-Programmversion.
        /// </summary>
        public string ProgramVersion { get; set; }

        /// <summary>
        /// Die Versionsnummer der HTTP API. Diese folgt den Regeln der sematischen Versionierung.
        /// </summary>
        public string ApiVersion { get; set; }
    }

    public enum AdressArt
    {
        Organisation,
        Person
    }

    public enum GesperrtArt
    {
        Nein = 0,
        Ja,
        Bereich,
        Hinweis
    }

    public class GeoCoordinate
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class NewAdresseInfo
    {
        public AdressArt AdressArt { get; set; }
    }

    public class Adresse
    {
        public string Code { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public AdressArt AdressArt { get; set; }

        public string Name { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Kürzel { get; set; }
        public string LandCode { get; set; }
        public bool IstPostfachInVerwendung { get; set; }
        public string Titel { get; set; }
        public string UstId { get; set; }
        public string Telefon { get; set; }
        public string Fax { get; set; }
        public string EMail { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string Name4 { get; set; }
        public string Ort { get; set; }
        public string Straße { get; set; }
        public string Plz { get; set; }
        public string Postfach { get; set; }
        public string PostfachPlz { get; set; }
        public string PostfachOrt { get; set; }
        public string MobilFirma { get; set; }
        public string MobilPrivat { get; set; }
        public string Internet { get; set; }
        public bool IstBauleistender { get; set; }
        public string Skype { get; set; }
        public string LoginName { get; set; }
        public string Suchbegriff { get; set; }
        public string Briefanschrift { get; set; }
        public string SozialesNetzwerk1 { get; set; }
        public string SozialesNetzwerk1Name { get; set; }
        public string SozialesNetzwerk2 { get; set; }
        public string SozialesNetzwerk2Name { get; set; }
        public string Adresszusatz { get; set; }
        public string BundeslandCode { get; set; }
        public string LandkreisCode { get; set; }
        public string AnredeCode { get; set; }
        public string KonzernCode { get; set; }
        public string ZentraleCode { get; set; }
        public string SperrhinweisCode { get; set; }
        public string SpracheCode { get; set; }
        public string AdressQuelleCode { get; set; }

        // TODO
        //public string GruppeCode { get; set; }
        //public Guid? BildId { get; set; }

        public string VerweisAufAdresseCode { get; set; }

        public bool IstDebitorVorhanden { get; set; }
        public bool IstKreditorVorhanden { get; set; }
        public string Handelsregister { get; set; }
        public string UnsereKundenNummerDort { get; set; }
        public string SteuernummerGesellschaft { get; set; }
        public string UrsprungsCode { get; set; }
        public bool IstEigeneAdresse { get; set; }
        public string TitelImAnschreiben { get; set; }
        public DateTime? Geburtsdatum { get; set; }
        public GesperrtArt GesperrtArt { get; set; }
        public DateTime? GültigAb { get; set; }
        public DateTime? GültigBis { get; set; }
        public bool IstDuplikat { get; set; }
        public int? VollständigkeitInProzent { get; set; }
        public decimal? Saldo { get; set; }
        public string ExternerCode { get; set; }
        public string Auslandsvorwahl { get; set; }
        public string DurchwahlFax { get; set; }
        public string DurchwahlZentrale { get; set; }
        public Guid? Guid { get; set; }
        public string Hauptanschlussnummer { get; set; }
        public bool? IsReadOnlyNumber { get; set; }
        public string Ortskennzahl { get; set; }
        public string OutlookEntryId { get; set; }
        public int? Ähnlichkeit { get; set; }
        public GeoCoordinate GeoPosition { get; set; }
        public string Notiz { get; set; }
        public string Beschreibung { get; set; }

        public List<Adressat> Adressaten { get; set; }
        public List<Bankverbindung> Bankverbindungen { get; set; }
        public List<AdressBranche> Branchen { get; set; }
        public List<AdressGewerk> Gewerke { get; set; }
    }

    public class Adressat
    {
        public string Code { get; set; }
        public string AnredeCode { get; set; }
        public string PrivatadresseCode { get; set; }
        public string Titel { get; set; }
        public string TitelImAnschreiben { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Telefon { get; set; }
        public string Fax { get; set; }
        public string Mobil { get; set; }
        public string EMail { get; set; }
        public string EMailAbteilung { get; set; }
        public string Url { get; set; }
        public string Skype { get; set; }
        public string AbteilungCode { get; set; }
        public string FunktionCode { get; set; }
        public bool IstInaktiv { get; set; }
        public DateTime? Austrittsdatum { get; set; }
        public string Raum { get; set; }
        public string Info { get; set; }
        public string Beschreibung { get; set; }
        public string Notiz { get; set; }
        public string SpracheCode { get; set; }
        public string Briefanschrift { get; set; }
        public string Durchwahl { get; set; }
        public string DurchwahlFax { get; set; }
        public Guid? Guid { get; internal set; }
    }

    public class Bankverbindung
    {
        public string Iban { get; set; }
        public string Bic { get; set; }
        public string Bankname { get; set; }
    }

    public class AdressBranche
    {
        public string BrancheCode { get; set; }
        public string Bezeichnung { get; set; }
        public string Beschreibung { get; set; }
    }

    public class AdressGewerk
    {
        public string GewerkCode { get; set; }
        public string Bezeichnung { get; set; }
        public string Beschreibung { get; set; }
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

        public List<Leistungszeitraum> Leistungszeiträume { get; set; }
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

        public string ListenpreisGeraetKostenanteil1 { get; set; }
        public string ListenpreisGeraetKostenanteil2 { get; set; }

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

    /// <summary>
    /// Beschreibt einen Zuschläg, der in einem Betriebsmittelstamm zur Verfügung steht. Der Wert des Zuschlags
    /// wird per Zuschlagskatalog.ZuschlagsgruppenWerte festgelegt.
    /// </summary>
    public class Zuschlagsgruppe : BaseObject
    {
        public string Nummer { get; set; }
        public string Bezeichnung { get; set; }
        public int? Stufe { get; set; }
    }

    /// <summary>
    /// Der Wert einer Zuschlagsgruppe innerhalb eines Zuschlagskatalogs.
    /// </summary>
    public class ZuschlagsgruppenWert : BaseObject
    {
        /// <summary>
        /// Verweist auf eine Zuschlagsgruppe.
        /// </summary>
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
        /// Liste von Kosten (eine pro Kostenebene, auf der die Kosten für dieses Betriebsmittel definiert sind).
        /// Ist normalerweise eine Detailinfo, das heißt, dieses Feld ist nur im Fall von Einzelabfragen befüllt.
        /// Allerdings erlaubt der Aufruf /build/global/betriebsmittelstaemme/{betriebsmittelStammId}/betriebsmittel
        /// über den "mitKosten"-Parameter das Auslesen meherer Betriebsmittel einschließlich Kosten.
        /// </summary>
        public List<BetriebsmittelKosten> Kosten { get; set; }

        /// <summary>
        /// Enthält berechnete Kosten und Preise. Diese sind abhängig von der gewählten Kosten- und Zuschlagsebene.
        /// Ist normalerweise eine Detailinfo, das heißt, dieses Feld ist nur im Fall von Einzelabfragen befüllt.
        /// Allerdings erlaubt der Aufruf /build/global/betriebsmittelstaemme/{betriebsmittelStammId}/betriebsmittel
        /// über den "mitKosten"-Parameter das Auslesen meherer Betriebsmittel einschließlich berechneter Kosten und Preise.
        /// </summary>
        public BetriebsmittelKostenDetails KostenDetails { get; set; }

        /// <summary>
        /// (Detailinfo) Liste mit weiteren Kosten.
        /// </summary>
        public List<KalkulationsZeile> WeitereKosten { get; set; }

        /// <summary>
        /// (Detailinfo) Die Zuschläge, die auf diesem Betriebsmittel definiert sind.
        /// </summary>
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

    /// <summary>
    /// Ein Zuschlag , der auf einem Betriebsmittel definiert ist.
    /// </summary>
    public class BetriebsmittelZuschlag : BaseObject
    {
        /// <summary>
        /// Verweist auf eine ZuschlagsartGruppe.
        /// </summary>
        public string ZuschlagsgruppenNummer { get; set; }

        /// <summary>
        /// Verweist auf eine Zuschlagsart.
        /// </summary>
        public int ArtIndex { get; set; }

        /// <summary>
        /// Die ID des Kosten- oder Zuschlagskatalogs.
        /// </summary>
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
    /// Enthält berechnete Betriebsmittelkosten.
    /// </summary>
    public class BetriebsmittelKostenDetails : BaseObject
    {
        /// <summary>
        /// Die Betriebsmittelkosten (ohne Berücksichtigung der weiteren Kosten).
        /// </summary>
        public Money Kosten { get; set; }

        /// <summary>
        /// Der Betriebsmittelpreis (ohne Berücksichtigung der weiteren Kosten).
        /// </summary>
        public Money Preis { get; set; }

        /// <summary>
        /// Die Gesamtbetrag der weiteren Kosten.
        /// </summary>
        public Money WeitereKosten { get; set; }

        /// <summary>
        /// Die Gesamtkosten.
        /// </summary>
        public Money KostenGesamt { get; set; }

        /// <summary>
        /// Der Gesamtpreis.
        /// </summary>
        public Money PreisGesamt { get; set; }
    }

    /// <summary>
    /// Enthält die Kostenebenen-spezifischen Kosten eines Betriebsmittels.
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

        /// <summary>
        /// Befüllt, wenn das Betriebsmittel ein Lohn ist.
        /// </summary>
        public BetriebsmittelKostenLohnDetails LohnDetails { get; set; }

        /// <summary>
        /// Befüllt, wenn das Betriebsmittel ein Material ist.
        /// </summary>
        public BetriebsmittelKostenMaterialDetails MaterialDetails { get; set; }

        /// <summary>
        /// Befüllt, wenn das Betriebsmittel ein Gerät ist.
        /// </summary>
        public BetriebsmittelKostenGerätDetails GerätDetails { get; set; }

        /// <summary>
        /// Befüllt, wenn das Betriebsmittel ein Sonstige-Kosten-Objekt ist.
        /// </summary>
        public BetriebsmittelKostenSonstigeKostenDetails SonstigeKostenDetails { get; set; }

        /// <summary>
        /// Befüllt, wenn das Betriebsmittel ein Nachunternehmer ist.
        /// </summary>
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
        Unterprojekt,
        BetriebsmittelKalkulationsZeile
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

    public class KalkulationsZeileDetails : BaseObject
    {
        public decimal? Ergebnis { get; set; }
        public decimal? MengeGesamt { get; set; }
        public decimal? LeistungsMenge { get; set; }
        public Money KostenProEinheit { get; set; }
        public Money Kosten { get; set; }
        public Money Preis { get; set; }
        public decimal? StundenProduktiv { get; set; }
        public Money ZuschlagGesamt { get; set; }
    }

    /// <summary>
    /// Eine Zeile in den weiteren Kosten oder in einem Kalkulationsblatt der Detailkalkulation.
    /// </summary>
    public class KalkulationsZeile : BaseObject
    {
        /// <summary>
        /// Die ID ist bei GET-Zugriffen immer befüllt. Für PUT-Operationen, d.h. für
        /// PUT /build/{projektId}/kalkulationen/{kalkulationId}/kalkulationsBlaetter/{positionId} und
        /// PUT /build/global/betriebsmittel/{betriebsmittelId}
        /// kann sie fehlen. In diesem Fall wird die Zeile neu angelegt.
        /// </summary>
        public Guid? Id { get; set; }

        public string Nummer { get; set; }

        public string Bezeichnung { get; set; }

        public string Kommentar { get; set; }

        public string Einheit { get; set; }

        public bool IstInaktiv { get; set; }

        /// <summary>
        /// (Detailinfo) Enthält weitere Eigenschaften der Kalkulationszeile, insbesondere berechnete Werte.
        /// </summary>
        public KalkulationsZeileDetails Details { get; set; }

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

        /// <summary>
        /// Befüllt, wenn es sich um einen Rückgriff handelt.
        /// </summary>
        public RückgriffZeileDetails RückgriffDetails { get; set; }

        /// <summary>
        /// Befüllt, wenn es sich um eine Summenzeile handelt.
        /// </summary>
        public SummenKalkulationsZeileDetails SummenDetails { get; set; }
    }

    public class KalkulationsZeileBetriebsmittelDetails : BaseObject
    {
        public Guid BetriebsmittelId { get; set; }
        public BetriebsmittelArt? BetriebsmittelArt { get; set; } // aus Performancegründen speichern wir hier auch (optional) die Betriebsmittelart ab

        public string Ansatz { get; set; }
        public string Variable { get; set; }
        public string BasNummer { get; set; }
    }

    public class KalkulationsZeileVariablenDetails : BaseObject
    {
        public string Variable { get; set; }
        public string Ansatz { get; set; }
    }

    public class KalkulationsZeileKommentarDetails : BaseObject
    {
    }

    public class RückgriffZeileDetails : BaseObject
    {
        public Guid PositionId { get; set; }

        public Guid? UnterpositionsZeileId { get; set; }

        public string Ansatz { get; set; }
        public string Variable { get; set; }
    }

    public enum SummenKalkulationsZeileArt
    {
        Relativ = 0,
        Absolut = 1,
    }

    public class SummenKalkulationsZeileDetails : BaseObject
    {
        public SummenKalkulationsZeileArt? Art { get; set; }
        public string Modifikator { get; set; }
        public Money Kosten { get; set; }
        public Money Preis { get; set; }
        public decimal? StundenProduktiv { get; set; }
    }

    public class KalkulationsZeileUnterpositionDetails : BaseObject
    {
        public string Ansatz { get; set; }
        public string Variable { get; set; }
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
        Gaeb,
        Frei
    }

    public enum NormExakt
    {
        None = 0,
        OENORMA2063 = 1,
        OENORMB2063BIS2018R1 = 2,
        GAEB90 = 3,
        GAEBXML = 4,
        GAEB2000 = 5,
        FREIEFORM = 6,
        GAEBXML32 = 7,
        OENORMA2063V2015 = 8,
        OENORMB2063 = 9
    }


    public enum LvArt
    {
        Ausschreibung,
        FreieAusschreibung,
        Vergabe,
        Auftrag,
        FreierAuftragEingehend,
        FreierAuftragAusgehend,
        Kostenschaetzung,
        Anfrage,
        Angebot,
        FreiesAngebot,
        AuftragAusfuehrend,
        Subvergabe,
        SubVergabeAusfuehren
    }

    public enum KalkulationsArt
    {
        NullKalkulation,
        AbgestimmteNullKalkulation,
        OptimierteKalkulation,
        AngebotsKalkulation,
        AuftragsKalkulation,
        Arbeitskalkulation
    }

    public class NewKalkulationInfo
    {
        public string Nummer { get; set; }
        public string Bezeichnung { get; set; }
        public KalkulationsArt? KalkulationsArt { get; set; }
        public Guid BetriebsmittelStammId { get; set; }
        public Guid? KostenkatalogId { get; set; }
        public Guid? ZuschlagskatalogId { get; set; }
    }

    /// <summary>
    /// Eine zu einem Leistungsverzeichnis gehörende Kalkulation.
    /// </summary>
    public class Kalkulation : BaseObject
    {
        public Guid Id { get; set; }
        public Guid LvId { get; set; }
        public string Nummer { get; set; }
        public string Bezeichnung { get; set; }
        public KalkulationsArt? Art { get; set; }

        /// <summary>
        /// Liste von untergeordneten Kalkulationen. Ist nur befüllt, wenn die Kalkulationen
        /// als Teil eines Leistungsverzeichnisses, d.h. per /build/{projektId}/leistungsverzeichnisse/{lvId} geladen wurden.
        /// </summary>
        public List<Kalkulation> Kalkulationen { get; set; }

        /// <summary>
        /// Enthält Informationen über die Zahl der Nachkommastellen verschiedener Feldtypen.
        /// </summary>
        public KalkulationNachkommastellen Nachkommastellen { get; set; }
    }

    public class KalkulationNachkommastellen : BaseObject
    {
        public int? Ansatz { get; set; }
        public int? AnsatzUI { get; set; }
        public int? KostenPreise { get; set; }
        public int? KostenPreiseUI { get; set; }
    }

    /// <summary>
    /// Ein Kalkulationsblatt. Dieses enthält die Kalkulationszeilen zu einer Position.
    /// Als Id fungiert das Tupel (KalkulationId, PositionId).
    /// </summary>
    public class KalkulationsBlatt : BaseObject
    {
        public Guid KalkulationId { get; set; }

        public Guid PositionId { get; set; }

        public string Nummer { get; set; }

        public string Bezeichnung { get; set; }

        /// <summary>
        /// (Detailinfo) Objekt mit weiteren Eigenschaften, insbesondere berechnete Werte (z.B. Einheitspreis).
        /// </summary>
        public KalkulationsBlattDetails Details { get; set; }

        /// <summary>
        /// (Detailinfo) Liste von Kalkulationszeilen (hierarchisch aufgebaut).
        /// </summary>
        public List<KalkulationsZeile> Zeilen { get; set; }
    }

    public class NewKalkulationsBlattInfo
    {
        public Guid PositionId { get; set; }
    }

    /// <summary>
    /// Detailinformationen (insbesondere berechnete Werte) eines Kalkulationsblattes
    /// </summary>
    public class KalkulationsBlattDetails : BaseObject
    {
        public string PositionsNummerKomplett { get; set; }

        public decimal? Menge { get; set; }
        public decimal? StundenProduktiv { get; set; }
        public decimal? StundenProduktivGesamt { get; set; }
        public Money Kosten { get; set; }
        public Money KostenGesamt { get; set; }
        public Money Preis { get; set; }
        public Money PreisGesamt { get; set; }
        public Money Einheitspreis { get; set; }
        public Money EinheitspreisGesamt { get; set; }

        public Dictionary<string, Money> Preisanteile { get; set; }
    }

    public class PreisanteilInfo : BaseObject
    {
        public string Code { get; set; }
        public string Bezeichnung { get; set; }
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
        public NormExakt? NormExakt { get; set; }
        public LvArt? Art { get; set; }
        public string Waehrung { get; set; }

        public List<PreisanteilInfo> PreisanteilInfos { get; set; }

        /// <summary>
        /// (Detailinfo) Der Wurzelknoten einschließlich untergeordneter Knoten und Positionen.
        /// </summary>
        public LvKnoten RootKnoten { get; set; }

        /// <summary>
        /// (Detailinfo) Die Wurzelkalkulationen einschließlich untergeordneter Kalkulationen.
        /// </summary>
        public List<Kalkulation> RootKalkulationen { get; set; }
    }

    public enum Herkunftskennzeichen
    {
        /// <summary>
        /// Position aus einem Leistungsbuch (LB).
        /// </summary>
        LB,

        /// <summary>
        /// + ... Position aus einem Ergänzungs-LB
        /// </summary>
        ErgLB,

        /// <summary>
        /// Z ... Frei formulierte Position (Z-Position)
        /// </summary>
        Z
    }

    public class LvItemBase : BaseObject
    {
        public Guid Id { get; set; }
        public string Typ { get; set; }
        public string Nummer { get; set; }
        public string NummerKomplett { get; set; }
        public string Kurztext { get; set; }
        public string Langtext { get; set; }
        public string Teilleistungsnummer { get; set; }
        public Money Betrag { get; set; }

        public Herkunftskennzeichen? Herkunftskennzeichen { get; set; }
    }

    /// <summary>
    /// Ein LV-Knoten (z.B. Titel oder Leistungsgruppe).
    /// </summary>
    public class LvKnoten : LvItemBase
    {
        public List<LvKnoten> Knoten { get; set; }
        public List<LvPosition> Positionen { get; set; }
    }

    /// <summary>
    /// Eine LV-Position.
    /// </summary>
    public class LvPosition : LvItemBase
    {
        public string Einheit { get; set; }
        public string Art { get; set; }
        public decimal? LvMenge { get; set; }
        // TODO Prognosemengen
        public Dictionary<string, Money> Preisanteile { get; set; }
        public Money Einheitspeis { get; set; }
        public bool IstFixpreis { get; set; }
        public bool IstIntern { get; set; }
    }

    public class Aufmaßblatt : BaseObject
    {
        public Guid Id { get; set; }
        public MengenArt? MengenArt { get; set; }
        public string Nummer { get; set; }
        public string Bezeichnung { get; set; }
    }

    public enum RechnungsStatus
    {
        Eingegangen = 0,
        Geprüft = 1,
        Freigegeben = 2,
        Bezahlt = 3,
        Ersetzt = 4,
        Gesendet = 10,
        Erfasst = 11,
    }

    public enum RechnungsArt
    {
        Einzelrechnung = 0,
        Abschlagsrechnung = 1,
        Teilschlussrechnung = 2,
        Schlussrechnung = 3,
        Regierechnung = 4,
        Vorauszahlung = 6,
        Pauschalrechnung = 7,
        AbschlagsrechnungPauschal = 8,
        AbschlagsrechnungNichtKumuliert = 9,
        AbschlagsrechnungNichtKumuliertPauschal = 10,
        SchlussrechnungPauschal = 11,
        TeilschlussrechnungPauschal = 12,
    }

    public class NewRechnungInfo : BaseObject
    {
        public string Nummer { get; set; }
        public string Bezeichnung { get; set; }
    }

    public class Rechnung : BaseObject
    {
        public Guid Id { get; set; }
        public string Nummer { get; set; }
        public string Bezeichnung { get; set; }
        public DateTime? Rechnungsdatum { get; set; }
        public string ExterneRechnungsnummer { get; set; }
        public DateTime? Eingangsdatum { get; set; }
        public DateTime? GesendetAm { get; set; }
        public RechnungsStatus? Status { get; set; }
        public List<Zahlung> Zahlungen { get; set; }
        public int? LaufendeNummerKapsel { get; set; }
        public RechnungsArt? Art { get; set; }
        public decimal? ProzentDerAuftragssumme { get; set; }
        public bool? IstInforechnung { get; set; }
        public decimal? NettoUngeprüft { get; set; }
        public decimal? NettoForderungKorrektur { get; set; }
        public bool? IstGeschützt { get; set; }
        public bool? BürgschaftBankhaftbrief { get; set; }
        public DateTime? GewährleistungBis { get; set; }
        public DateTime? RückgabeGewährleistungseinbehaltBar { get; set; }
    }

    public class Zahlung : BaseObject
    {
        public Guid Id { get; set; }
        public DateTime? Zahlungsdatum { get; set; }
        public decimal? Zahlbetrag { get; set; }
        public decimal? Skontobetrag { get; set; }
        public string Bemerkung { get; set; }
    }

    public class AbrechnungsMerkmal : BaseObject
    {
        public Guid Id { get; set; }
        public string Nummer { get; set; }
        public string Bezeichnung { get; set; }
        public string BezeichnungKomplett { get; set; }
        public List<AbrechnungsMerkmal> Merkmale { get; set; }
    }

    public class Leistungszeitraum : BaseObject
    {
        public Guid Id { get; set; }
        public string Nummer { get; set; }
        public string Bezeichnung { get; set; }
        public DateTime? Beginn { get; set; }
        public DateTime? Ende { get; set; }
    }

    public class NewPositionsblockInfo : BaseObject
    {
        public MengenArt? MengenArt { get; set; }
        public Guid? PositionId { get; set; }
        public Guid? VerherigePositionId { get; set; }
        public Guid? AufmaßblattId { get; set; }
        public Guid? LeistungszeitraumId { get; set; }
        public Guid? RechnungId { get; set; }
    }

    public class Positionsblock : BaseObject
    {
        public Guid Id { get; set; }
        public Guid? PositionId { get; set; }
        public Guid? PositionKorrigiertId { get; set; }

        /// <summary>
        /// Die vollständige Positionsnummer.
        /// </summary>
        public string Nummer { get; set; }
        public string NummerKorrigiert { get; set; }
        public string Kurztext { get; set; }
        public string Einheit { get; set; }

        public Guid? AufmaßblattId { get; set; }
        public Guid? LeistungszeitraumId { get; set; }
        public Guid? RechnungId { get; set; }

        public decimal? Menge { get; set; }

        public List<Aufmaßzeile> Aufmaßzeilen { get; set; }

        public List<Guid> MerkmalIds { get; set; }

        public bool? ErzeugtInKorrektur { get; set; }
    }

    public enum MengenArt
    {
        Lv = 0,
        Abrechnung = 1,
        Umlagemenge = 2,
        Bautagebuch = 3,
        AbrechnungKorrigiert = 4,
        Rechnung = 5,
        Rechnungkorrigiert = 6,
        Prognose1 = 10,
        Prognose2 = 11,
        Prognose3 = 12,
        Prognose4 = 13,
        Prognose5 = 14,
        Prognose6 = 15,
        Prognose7 = 16,
        Prognose8 = 17,
        Prognose9 = 18,
        Prognose10 = 19
    }

    public class Aufmaßzeile : BaseObject
    {
        public Guid Id { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public AufmaßzeilenArt Art { get; set; }
        public AufmaßzeilenArt? ArtKorrigiert { get; set; }
        public string InternerKommentar { get; set; }
        public string Inhalt { get; set; }
        public string InhaltKorrigiert { get; set; }
        public string Variable { get; set; }
        public string AdresseVon { get; set; }
        public string AdresseBis { get; set; }
        public decimal? Faktor { get; set; }
        public decimal? FaktorKorrigiert { get; set; }
        public Formel Formel { get; set; }
        public Formel FormelKorrigiert { get; set; }
        public bool? ErzeugtInKorrektur { get; set; }
        public decimal? Menge { get; set; }
    }

    public enum AufmaßzeilenArt
    {
        Ansatz = 0,
        Formel,
        Kommentar
    }

    public class Formel
    {
        public int Id { get; set; }
        public List<FormelParameter> Params { get; set; }
    }

    public class FormelParameter
    {
        public string Name { get; set; }
        public decimal? Value { get; set; }
        public string Variable { get; set; }
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

        public override string ToString()
        {
            return Currency + " " + Value;
        }
    }

    /// <summary>
    /// Ein mehrfacher Geldbetrag (für unterschiedliche Währungen).
    /// </summary>
    public class Money : Collection<SimpleMoney>
    {
        public Money()
        {
        }

        public static Money FromValues(IEnumerable<(string Currency, decimal? Value)> values)
        {
            if (values == null)
            {
                return null;
            }

            Money result = null;

            foreach (var betrag in values)
            {
                if (result == null)
                {
                    result = new Money();
                }

                result.Add(betrag.Currency, betrag.Value);
            }

            return result;
        }

        public void Add(string currency, decimal? value)
        {
            Add(new SimpleMoney(currency, value));
        }

        public decimal? FirstValue
        {
            get => this.FirstOrDefault()?.Value;
            set => this[0] = new SimpleMoney(this[0].Currency, value);
        }

        public override string ToString()
        {
            return string.Join("; ", this);
        }
    }
}
