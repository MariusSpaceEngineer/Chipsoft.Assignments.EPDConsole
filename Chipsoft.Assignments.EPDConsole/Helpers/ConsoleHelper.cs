using System.Globalization;

public static class ConsoleHelper
{
    private static void DisplayErrorMessage(string message)
    {
        Console.WriteLine(message);
        Console.WriteLine("Druk op een toets om opnieuw te proberen...");
        Console.ReadKey();
    }

    public static string GetStringInput(string prompt, bool isOptional = false)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine(prompt);
            var input = Console.ReadLine()?.Trim();

            if (isOptional || !string.IsNullOrWhiteSpace(input))
            {
                return input;
            }
            else
            {
                DisplayErrorMessage("Ongeldige invoer. Probeer het opnieuw.");
            }
        }
    }

    public static string GetValidatedInput(string prompt, Func<string, bool> validate, string errorMessage = "Ongeldige invoer.")
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine(prompt);
            var input = Console.ReadLine()?.Trim();

            if (!string.IsNullOrWhiteSpace(input) && validate(input))
            {
                return input;
            }
            else
            {
                DisplayErrorMessage(errorMessage);
            }
        }
    }

    public static T GetSelectionInput<T>(string prompt, List<T> items, Func<T, string> display)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine(prompt);
            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {display(items[i])}");
            }

            if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= items.Count)
            {
                return items[index - 1];
            }
            else
            {
                DisplayErrorMessage("Ongeldige invoer. Probeer het opnieuw.");
            }
        }
    }

    public static int GetIntInput(string prompt)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine(prompt);
            if (int.TryParse(Console.ReadLine(), out int value))
            {
                return value;
            }
            else
            {
                DisplayErrorMessage("Ongeldig getal. Probeer het opnieuw.");
            }
        }
    }

    public static DateTime GetDateInput(string prompt, string format, bool mustBeFuture = false)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine(prompt);
            var input = Console.ReadLine();

            if (DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                if (mustBeFuture && date <= DateTime.Now)
                {
                    DisplayErrorMessage("De datum moet in de toekomst liggen. Probeer het opnieuw.");
                    continue;
                }
                return date;
            }
            else
            {
                DisplayErrorMessage("Ongeldige datum. Probeer het opnieuw.");
            }
        }
    }


    public static bool GetBoolInput(string prompt)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"{prompt} (J/N)");
            var input = Console.ReadLine()?.Trim().ToLower();

            if (input == "j")
            {
                return true;
            }
            else if (input == "n")
            {
                return false;
            }
            else
            {
                DisplayErrorMessage("Voer 'J' of 'N' in.");
            }
        }
    }
}
