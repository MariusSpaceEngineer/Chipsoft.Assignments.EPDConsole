using Chipsoft.Assignments.EPDConsole.Models;
using System.ComponentModel.DataAnnotations;

public class Appointment
{
    public int Id { get; set; }

    [Required]
    public int PatientId { get; set; }

    public Patient Patient { get; set; }

    [Required]
    public int PhysicianId { get; set; }

    public Physician Physician { get; set; }

    [Required]
    [FutureDate(ErrorMessage = "Appointment date must be in the future.")]
    public DateTime Date { get; set; }

    [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
    public string Description { get; set; }
}

public class FutureDateAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value is DateTime dateTime)
        {
            return dateTime > DateTime.Now;
        }
        return false;
    }
}
