using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrzyzowkowyPomocnik.Model
{

    public class Wyraz
    {
        public int id { get; set; }
        public string kierunek { get; set; }
        public int dlugosc { get; set; }
        public Wspolrzedne poczatek { get; set; }

        /// <summary>
        /// litery do hasła, numerowane od zera
        /// </summary>
        public int[] litery { get; set; }
        public string opis { get; set; }
        public string wynik { get; set; }
    }

    public class Wspolrzedne
    {
        public int x { get; set; }
        public int y { get; set; }
    }
}
