// See https://aka.ms/new-console-template for more information
using KrzyzowkowyPomocnik;

Console.WriteLine("Hello, World!");


KrzyzowkaHelper krzyzowkaHelper = new KrzyzowkaHelper();
krzyzowkaHelper.LoadFromFile("krzyzowka.json");
//krzyzowkaHelper.SaveToFile("krzyzowka2.json");



//Slownik slownik = Slownik.GetInstance();

foreach (var item in krzyzowkaHelper.Wyrazy)
{
    Console.WriteLine(item);
}

krzyzowkaHelper.Rysuj();
Console.WriteLine("Koniec");