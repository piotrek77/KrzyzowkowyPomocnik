using KrzyzowkowyPomocnik.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KrzyzowkowyPomocnik
{
    internal class KrzyzowkaHelper
    {


        Pole[,] plansza;


        private Krzyzowka krzyzowka;
        private List<WyrazHelper> wyrazy;
        public List<WyrazHelper> Wyrazy
        {
            get
            {
                return this.wyrazy;
            }
        }
        public void LoadFromFile(string filename)
        {
            // wczytaj krzyżówkę z pliku
            string jsonString = File.ReadAllText(filename);
            var wyrazy = JsonSerializer.Deserialize<List<Wyraz>>(jsonString) ?? new List<Wyraz>();



            krzyzowka = new Krzyzowka
            {
                wyrazy = wyrazy
            };
            this.wyrazy = new List<WyrazHelper>();

            foreach (Wyraz wyraz in wyrazy)
            {
                WyrazHelper wyrazHelper = new WyrazHelper(wyraz);
                this.wyrazy.Add(wyrazHelper);
            }

            Weryfikuj();

            RobPlansze();
        }

        private void Weryfikuj()
        {
            foreach (var wyraz in wyrazy)
            {
                if (wyraz.kierunek != "v" && wyraz.kierunek != "h")
                {
                    throw new Exception("Nieprawidłowy kierunek wyrazu");
                }

                if (wyraz.dlugosc < 1)
                {
                    throw new Exception("Nieprawidłowa długość wyrazu <1");
                }

                if (wyraz.poczatek.x < 0 || wyraz.poczatek.y < 0)
                {
                    throw new Exception("Nieprawidłowe współrzędne początku wyrazu");
                }

                if (wyraz.litery.Length > wyraz.dlugosc)
                {
                    throw new Exception("Nieprawidłowa liczba liter do hasła");
                }
            }


        }
        private void RobPlansze()
        {
            //obliczamy wymiary planszy
            int wymiarX = 0;
            int wymiarY = 0;
            foreach (var wyraz in wyrazy)
            {
                if (wyraz.kierunek == "v")
                {
                    if (wyraz.poczatek.x + 1 > wymiarX)
                    {
                        wymiarX = wyraz.poczatek.x + 1;
                    }
                    if (wyraz.poczatek.y + wyraz.dlugosc > wymiarY)
                    {
                        wymiarY = wyraz.poczatek.y + wyraz.dlugosc;
                    }
                }
                else
                {
                    if (wyraz.poczatek.x + wyraz.dlugosc > wymiarX)
                    {
                        wymiarX = wyraz.poczatek.x + wyraz.dlugosc;
                    }
                    if (wyraz.poczatek.y + 1 > wymiarY)
                    {
                        wymiarY = wyraz.poczatek.y + 1;
                    }
                }
            }
            Console.WriteLine($"wymiary: x={wymiarX}, y={wymiarY}");
            plansza = new Pole[wymiarX, wymiarY];

            for (int i = 0; i < wymiarX; i++)
            {
                for (int j = 0; j < wymiarY; j++)
                {
                    plansza[i, j] = new Pole();
                }
            }



            char znakPusty = Pole.ZnakPusty;
            //uzupełnianie planszy już istniejącej
            foreach (var wyraz in wyrazy)
            {
                if (wyraz.wynik.Length > 0)
                {
                    if (wyraz.kierunek == "h") //poziomo
                    {
                        for (int i = wyraz.poczatek.x; i < wyraz.poczatek.x + wyraz.dlugosc; i++)
                        {
                            int indeks = i - wyraz.poczatek.x;
                            char litera = wyraz.wynik.Length > indeks ? wyraz.wynik[i - wyraz.poczatek.x] : ' ';
                            if (plansza[i, wyraz.poczatek.y].litera != znakPusty && litera != ' ' && litera != plansza[i, wyraz.poczatek.y].litera)
                            {
                                Console.WriteLine($"Kolizja liter, wyraz: {wyraz.id}{wyraz.kierunek}, na pozyji {i},{wyraz.poczatek.y}");
                            }
                            else
                            {
                                plansza[i, wyraz.poczatek.y].litera = litera;
                            }
                            plansza[i, wyraz.poczatek.y].doHasla = false;
                            if (wyraz.litery.Contains(i - wyraz.poczatek.x))
                            {
                                plansza[i, wyraz.poczatek.y].doHasla = true;
                            }
                        }




                    }
                    else
                    {
                        //czyli kierunek == "v"

                        for (int i = wyraz.poczatek.y; i < wyraz.poczatek.y + wyraz.dlugosc; i++)
                        {
                            int indeks = i - wyraz.poczatek.y;
                            char litera = wyraz.wynik.Length > indeks ? wyraz.wynik[i - wyraz.poczatek.y] : ' ';
                            if (plansza[wyraz.poczatek.x, i].litera != znakPusty && litera != ' ' && litera != plansza[wyraz.poczatek.x, i].litera)
                            {
                                Console.WriteLine($"Kolizja liter, wyraz: {wyraz.id}{wyraz.kierunek}, na pozyji {wyraz.poczatek.x},{i}");
                            }
                            else
                            {
                                plansza[wyraz.poczatek.x, i].litera = litera;
                            }

                            plansza[wyraz.poczatek.x, i].doHasla = false;
                            if (wyraz.litery.Contains(i - wyraz.poczatek.y))
                            {
                                plansza[wyraz.poczatek.x, i].doHasla = true;
                            }
                        }
                    }
                }
            }
        }

        public void SaveToFile(string filename)
        {
            // wczytaj krzyżówkę z pliku
            //string jsonString = File.ReadAllText(filename);
            //var wyrazy = JsonSerializer.Deserialize<List<Wyraz>>(jsonString) ?? new List<Wyraz>();
            string jsonString = JsonSerializer.Serialize(Wyrazy, options: new JsonSerializerOptions() { WriteIndented = true });
            File.WriteAllText(filename, jsonString);


        }


        public void Rysuj()
        {
            int SZEROKOSC = 3;
            for (int j = 0; j < plansza.GetLength(0); j++)
            {
                Console.Write(j.ToString().PadLeft(SZEROKOSC) + "|");
            }
            Console.WriteLine();
            for (int j = 0; j < plansza.GetLength(0); j++)
            {
                Console.Write("---" + "|");
            }
            Console.WriteLine();
            for (int i = 0; i < plansza.GetLength(1); i++)
            {
                for (int j = 0; j < plansza.GetLength(0); j++)
                {
                    Console.Write(" " + plansza[j, i].litera + " |");

                }
                Console.WriteLine();
                for (int j = 0; j < plansza.GetLength(0); j++)
                {
                    Console.Write("---" + "|");
                }
                Console.WriteLine();
            }
        }
    }
}
