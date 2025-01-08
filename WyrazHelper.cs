using KrzyzowkowyPomocnik.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrzyzowkowyPomocnik
{
    internal class WyrazHelper : Wyraz
    {

        public WyrazHelper(Wyraz wyraz)
        {
            //skopiuj wszystkie pola z wyrazu
            this.id = wyraz.id;
            this.kierunek = wyraz.kierunek;
            this.dlugosc = wyraz.dlugosc;
            this.poczatek = wyraz.poczatek;
            this.litery = wyraz.litery;
            this.opis = wyraz.opis;
            this.wynik = wyraz.wynik;

        }



        public override string ToString()
        {
            return $"{this.opis}: {this.wynik}";
        }

}
}
