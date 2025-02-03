namespace Chipsoft.Assignments.EPDConsole.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PhysicianId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
