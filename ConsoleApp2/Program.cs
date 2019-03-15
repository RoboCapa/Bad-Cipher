using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    private static int GetAlphabetPosition(char character) => character - 64;
    private static char GetAlphabetFromPosition(int position) => (char)(position + 64);
    private static List<char> symbolsToWorkWith = GenerateSymbolsWithoutAt();

    static void Main()
    {

        var randomString = RandomStringGenerator.GenerateRandomString(15, symbolsToWorkWith);
        Console.WriteLine($"[{randomString.Length}] | {randomString}");

        Task1(symbolsToWorkWith, randomString);
        Salt(randomString);
        Console.ReadKey();
    }
    private static string Salt(string str)
    {
        var stringToReturn = "";

        foreach (var @char in str)
        {
            if (int.TryParse(@char.ToString(), out var result))
                stringToReturn += @char;
            else if (symbolsToWorkWith.Contains(@char))
                stringToReturn += @char;
            else
            {
                var position = GetAlphabetPosition(@char);
                stringToReturn += position.ToString() + (position % 2 == 0 ? 4 : 5).ToString();
            }
        }
        var doubleDigitIndexes = new List<int>();
        int? firstDigit = null;
        for (int i = 0; i < stringToReturn.Length; i++)
        {
            if (!int.TryParse(stringToReturn[i].ToString(), out var value))
            {
                firstDigit = null;
                continue;
            }

            if (firstDigit == null && (value == 1 || value == 2))
            {
                firstDigit = value;
            }
            else if (firstDigit == 1 && (value >= 0 && value <= 9))
            {
                doubleDigitIndexes.Add(i - 1);
                firstDigit = null;
            }
            else if (firstDigit == 2 && (value >= 0 && value <= 6))
            {
                doubleDigitIndexes.Add(i - 1);
                firstDigit = null;
            }
            else
            {
                firstDigit = null;
            }

        }
        Console.WriteLine($"First Step: {stringToReturn}");

        var newReturnString = stringToReturn;
        foreach (var index in doubleDigitIndexes)
        {
            var value = GetAlphabetFromPosition(int.Parse($"{stringToReturn[index]}{stringToReturn[index + 1]}"));
            newReturnString = newReturnString.Remove(index, 2);

            newReturnString = newReturnString.Insert(index, value.ToString() + "@");
        }

        Console.WriteLine($"Second Step: {newReturnString}");
        return newReturnString;
    }

    private static void Task1(List<char> symbolsToWorkWith, string randomString)
    {
        var numbers = "";
        var symbols = "";
        var strings = "";
        foreach (var c in randomString)
        {
            if (int.TryParse(c.ToString(), out var result))
                numbers += c;
            else if (symbolsToWorkWith.Contains(c))
                symbols += c;
            else
                strings += c;
        }

        Console.WriteLine(numbers);
        Console.WriteLine(symbols);
        Console.WriteLine(strings);
    }

    private static List<char> GenerateSymbolsWithoutAt()
    {
        var list = new List<char>();
        for (int i = 33; i <= 47; i++)
        {
            list.Add((char)i);
        }
        for (int i = 58; i <= 63; i++)
        {
            list.Add((char)i);
        }
        for (int i = 91; i <= 96; i++)
        {
            list.Add((char)i);
        }
        for (int i = 123; i <= 126; i++)
        {
            list.Add((char)i);
        }

        return list;
    }

}