using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lv_Viewer
{
    public static class FormattedTextTemplate
    {
        public static string GetVorlageAusschreiber() =>
            "<!DOCTYPE html>" +
                "<html>" +
                    "<head>" +
                        "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge,chrome=1\">" +
                        "<style>" +                            
                            "al { background-color: rgb(255, 255, 23); }" +
                            "bl { background-color: rgb(13, 164, 13); }" +
                            "p { font-family: Segoe UI; }" +
                            "span { font-size: 0.80em; font-family: Segoe UI; }" +
                            "TextComplement { background-color: rgb(255, 255, 23); }" +
                        "</style>" +
                    "</head>" +
                  "</html>";

        public static string GetVorlageBieter() =>
            "<!DOCTYPE html>" +
                "<html>" +
                    "<head>" +
                        "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge,chrome=1\">" +
                        "<style>" +
                            "al { background-color: rgb(255, 255, 23); }" +
                            "bl { background-color: rgb(13, 164, 13); }" +
                            "p { font-family: Segoe UI; }" +
                            "span { font-size: 0.80em; font-family: Segoe UI; }" +
                            "TextComplement { background-color: rgb(13, 164, 13); }" +
                        "</style>" +
                    "</head>" +
                  "</html>";
    }
}
