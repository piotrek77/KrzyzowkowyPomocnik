using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrzyzowkowyPomocnik
{
    internal class Pole
    {

        public static char ZnakPusty = '.';


        public int x { get; set; }
        public int y { get; set; }
        public char litera { get; set; }
        public bool doHasla { get; set; }

        public Pole()
        {
            this.x = 0;
            this.y = 0;
            this.litera = ZnakPusty;
            this.doHasla = false;
        }
    }
}
