using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lv_Viewer
{
    public static class FormattedTextTemplate
    {
        public static string GetVorlage() =>
            "<!DOCTYPE html>" +
                "<html>" +
                    "<head>" +
                        "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge,chrome=1\">" +
                        "<style>" +
                            "<body>" +
                                "{ font-size: 9px; }" +
                            "</body>" +
                            "al { background-color: rgb(255, 255, 23); }" +
                            "bl { background-color: rgb(13, 164, 13); }" +
                        "</style>" +
                    "</head>" +
                  "</html>";
    }
}
