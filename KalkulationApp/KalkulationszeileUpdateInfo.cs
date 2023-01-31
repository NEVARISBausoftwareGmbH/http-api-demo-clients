using Nevaris.Build.ClientApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalkulationApp
{
    internal class KalkulationszeileUpdateInfo
    {
        public KalkulationszeileUpdateInfo(KalkulationsZeile? kalkulationsZeile)
        {

        }

        public KalkulationsZeile? VariableSkZeile { get; set; }
        public KalkulationsZeile? EigenleistungZeile { get; set; }        
    }
}
