using System.Globalization;

public static class ConsoleHelper
{
    private static void DisplayErrorMessage(string message)
    {
        Console.WriteLine(message);
        Console.WriteLine("Druk op een toets om opnieuw te proberen...");
        Console.ReadKey();
    }

    // Prompts the user for a string input.
    // If the input is optional, it allows empty input.
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

    // Checks to see if the input is not empty and if it is valid according to the validate function.
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

    // Prompts the user to select an item from a list.
    // The details of each item are displayed using the provided display function.
    public static T GetSelectionInput<T>(string prompt, List<T> items, params Func<T, string>[] display)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine(prompt);

            for (int i = 0; i < items.Count; i++)
            {
                // Join the details of each item using a hyphen separator.
                string details = string.Join(" - ", display.Select(func => func(items[i])));
                Console.WriteLine($"{i + 1} - {details}");
            }

            var keyInfo = Console.ReadKey(intercept: true);
            if (keyInfo.Key == ConsoleKey.Escape)
            {
                // Return default (null) to indicate cancellation.
                return default;
            }
            else
            {
                // Combine the first pressed key with the rest of the input.
                string input = keyInfo.KeyChar + Console.ReadLine();
                if (IsValidSelection(input, items.Count, out int index))
                {
                    return items[index - 1];
                }
                else
                {
                    DisplayErrorMessage("Ongeldige invoer. Probeer het opnieuw.");
                }
            }
        }
    }

    // Validates the selection input.
    private static bool IsValidSelection(string input, int itemCount, out int index)
    {
        return int.TryParse(input, out index) && index >= 1 && index <= itemCount;
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

    // Prompts the user for a date input.
    // The date must be in the specified format and optionally in the future.
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

    public static void ShowAppointmentDetails(IEnumerable<Appointment> appointments, string title)
    {
        Console.Clear();
        Console.WriteLine(title);
        foreach (var appointment in appointments)
        {
            Console.WriteLine($"Patiënt: {appointment.Patient.Name} - Arts: {appointment.Physician.Name} - Datum: {appointment.Date} - Beschrijving: {appointment.Description}");
        }
        Console.WriteLine("");
    }

    public static void PromptToContinue()
    {
        Console.WriteLine("Druk op een toets om terug te keren naar het menu...");
        Console.ReadKey();
    }
}
