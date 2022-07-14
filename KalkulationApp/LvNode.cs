using Nevaris.Build.ClientApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalkulationApp
{
    public class LvNode : LvItem
    {
        public LvNode(LvKnoten? lvKnoten)
            : base(lvKnoten)
        {
            
        }                      
    }
}
