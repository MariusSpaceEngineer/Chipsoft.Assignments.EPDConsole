using System.ComponentModel.DataAnnotations;

public static class ValidationHelper
{
    public static bool IsValidPhoneNumber(string phoneNumber)
    {
        var phoneAttribute = new PhoneAttribute();
        return phoneAttribute.IsValid(phoneNumber);
    }

    public static bool IsValidEmail(string email)
    {
        var emailAttribute = new EmailAddressAttribute();
        return emailAttribute.IsValid(email);
    }

    public static bool IsValidName(string name)
    {
        return !string.IsNullOrWhiteSpace(name) && !name.Any(char.IsDigit);
    }

    public static bool IsValidDate(DateTime date)
    {
        return date > DateTime.Now;
    }

    // Validate model using DataAnnotations
    public static void ValidateModel<T>(T model)
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(model);

        if (!Validator.TryValidateObject(model, validationContext, validationResults, true))
        {
            var errors = string.Join(", ", validationResults.Select(vr => vr.ErrorMessage));
            throw new ArgumentException("Fout: ongeldige invoer. Controleer de gegevens en probeer opnieuw.");
        }
    }
}
