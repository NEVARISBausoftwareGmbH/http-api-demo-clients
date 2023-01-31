using Lv_Viewer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nevaris.Build.ClientApi;

namespace KalkulationApp
{
    public class LvPosition : LvItem
    {
        public LvPosition(Nevaris.Build.ClientApi.LvPosition? lvPosition) 
            : base(lvPosition)
        {
            Id = lvPosition != null ? lvPosition.Id : throw new ArgumentNullException(nameof(lvPosition));
            ItemTyp = lvPosition?.ItemTyp;
        }

        public LvItemTyp? ItemTyp { get; private set; }
        public Guid Id { get; private set; }
    }
}
