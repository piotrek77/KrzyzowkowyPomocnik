using System;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

public class StringParser
{



    public static (int id, string kierunek, string polecenie, string tekst) ParseString(string input)
    {
        List<string> tekst = new List<string>();
        string[] elementy = input.Split(' ');


        int directionIndex = 0;
        while (directionIndex < elementy[0].Length && char.IsDigit(elementy[0][directionIndex]))
        {
            directionIndex++;
        }
        string numberStr = input.Substring(0, directionIndex);
        int number = Convert.ToInt32(numberStr);
        string direction = elementy[0].Substring(directionIndex, 1);
        string polecenie = "";
        if (elementy[1].StartsWith('/'))
        {
            polecenie = elementy[1].Substring(1);
        }
        else
        {
            tekst.Add(elementy[1]);
        }
        for(int i = 2; i < elementy.Length; i++)
        {
            tekst.Add(elementy[i]);
        }
        Console.WriteLine($"Numer: {number}");
        Console.WriteLine($"Kierunek: {direction}");
        Console.WriteLine($"Polecenie: {polecenie}");
        string tekstCaly = string.Join(" ", tekst);
        Console.WriteLine($"Tekst: {tekstCaly}");
        return (number, direction, polecenie, tekstCaly);
    }
    public static void ParseStringOld(string input)
    {
        // Znajdź indeks pierwszego znaku niebędącego cyfrą (początek kierunku)
        int directionIndex = 0;
        while (directionIndex < input.Length && char.IsDigit(input[directionIndex]))
        {
            directionIndex++;
        }

        // Wyodrębnij numer
        string number = input.Substring(0, directionIndex);

        // Wyodrębnij kierunek
        string direction = input.Substring(directionIndex, 1);

        // Sprawdź, czy jest polecenie
        string command = null;
        int commandIndex = input.IndexOf('/');
        if (commandIndex >= 0)
        {
            // Usuń spacje przed i po '/'
            command = input.Substring(commandIndex).Trim();
            int commandEnd = command.IndexOf(' ');
            if (commandEnd >= 0)
            {
                command = command.Substring(0, commandEnd);
            }
            // Zaktualizuj indeks końca polecenia
            commandIndex = commandIndex + command.Length;
        }

        // Wyodrębnij tekst
        int textIndeks = command.IndexOf(" ");
        string text = command.Substring(textIndeks+ 1);

        // Wyświetl wyniki
        Console.WriteLine($"Numer: {number}");
        Console.WriteLine($"Kierunek: {direction}");
        Console.WriteLine($"Polecenie: {command ?? "Brak"}");
        Console.WriteLine($"Tekst: {text}");
    }
}