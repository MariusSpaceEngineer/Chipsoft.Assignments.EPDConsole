using System.ComponentModel.DataAnnotations;

public class Patient
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    [Phone(ErrorMessage = "Ongeldig telefoonnummer formaat.")]
    public string PhoneNumber { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Ongeldig e-mailadres formaat.")]
    public string Email { get; set; }

    public List<Appointment> Appointments { get; set; }
}
