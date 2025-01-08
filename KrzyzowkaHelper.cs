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
        private Krzyzowka krzyzowka;

        public void LoadFromFile(string filename)
        {
            // wczytaj krzyżówkę z pliku
            string jsonString = File.ReadAllText(filename);
            krzyzowka = JsonSerializer.Deserialize<Krzyzowka>(jsonString) ?? new Krzyzowka();
        }
    }
}
