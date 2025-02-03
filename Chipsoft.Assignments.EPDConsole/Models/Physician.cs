using System.ComponentModel.DataAnnotations;

namespace Chipsoft.Assignments.EPDConsole.Models
{
    public class Physician
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Specialization { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
