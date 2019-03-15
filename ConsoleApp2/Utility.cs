using System;
using System.Collections.Generic;

public class RandomStringGenerator
{
    private static readonly Random random = new Random(DateTime.Now.Millisecond);

    public static string GenerateRandomString(int characters, List<char> symbolsToWorkWith)
    {
        var strToReturn = "";

        for (int i = 0; i < characters; i++)
        {
            switch (random.Next(1, 4))
            {
                case 1:
                    strToReturn += GetLetter();
                    break;
                case 2:
                    strToReturn += GetNumber();
                    break;
                case 3:
                    strToReturn += GetSymbol(symbolsToWorkWith);
                    break;
            }
        }
        return strToReturn;
    }

    private static int GetNumber()
    {
        for (; ; )
        {
            var num = random.Next(0, 9);

            if (num != 4 && num != 5)
            {
                return num;
            }
        }
    }

    private static char GetLetter()
    {
        int num = random.Next(0, 26);
        return (char)(65 + num);
    }

    private static char GetSymbol(List<char> symbolsToWorkWith)
    {
        int numberTotake = random.Next(0, symbolsToWorkWith.Count - 1);
        return symbolsToWorkWith[numberTotake];
    }


}
