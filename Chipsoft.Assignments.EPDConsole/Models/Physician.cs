namespace Chipsoft.Assignments.EPDConsole.Models
{
    public class Physician
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
