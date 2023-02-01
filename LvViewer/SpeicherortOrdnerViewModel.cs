using Nevaris.Build.ClientApi;

namespace Lv_Viewer;

/// <summary>
/// ViewItem für einen Ordner innerhalb eines Speicherorts (inklusive Wurzelordner).
/// </summary>
public class SpeicherortOrdnerViewModel
{
    public SpeicherortOrdnerViewModel(Speicherort speicherort, SpeicherortOrdner? ordner, string pfad)
    {
        Speicherort = speicherort;
        Ordner = ordner;
        Pfad = pfad;
    }

    /// <summary>
    /// Der Speicherort.
    /// </summary>
    public Speicherort Speicherort { get; }
    
    /// <summary>
    /// Der Ordner (null für die Wurzelebene).
    /// </summary>
    public SpeicherortOrdner? Ordner { get; }

    /// <summary>
    /// Der vollständige Pfad (Speicherort-Name + Ordner-Namen)
    /// </summary>
    public string Pfad { get; }
}