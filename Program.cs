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



string wybor = "";
string[] poleceniaWyjscia = { "quit", "exit", "q" };
string lastCommand = "";
do
{
    krzyzowkaHelper.Rysuj();
    Console.WriteLine("Podaj komendę (quit, exit, q - wyjście):");
    Console.WriteLine("Polecenia dot wyrazów, np. 12v /polecenie, albo 12v wyraz, 12v to oczywiście 12 pionowo, 12h - 12 poziomo");
    wybor = Console.ReadLine();
    if (wybor == "!!")
        wybor = lastCommand;
    else
    {
        if (wybor.Length > 0)
            lastCommand = wybor;
    }


    if (CzyZaczynaSieOdCyfry(wybor))
    {

        var parsedTekst = StringParser.ParseString(wybor);

        if (parsedTekst.polecenie.Length == 0 && parsedTekst.id > 0 && parsedTekst.kierunek.Length == 1)
        {
            krzyzowkaHelper.SetWyraz(parsedTekst.id, parsedTekst.kierunek, parsedTekst.tekst);
        }


    }
    else
    {
        switch (wybor)
        {

            case "save":
                krzyzowkaHelper.SaveToFile("krzyzowka.json");
                break;




        }
    }


} while (!poleceniaWyjscia.Contains(wybor));

Console.WriteLine("Koniec");




bool CzyZaczynaSieOdCyfry(string text)
{
    if (string.IsNullOrEmpty(text))
        return false; // Sprawdzenie, czy ciąg jest pusty lub null

    return text[0] >= '0' && text[0] <= '9';
}