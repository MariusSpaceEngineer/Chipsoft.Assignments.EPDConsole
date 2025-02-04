namespace Chipsoft.Assignments.EPDConsole.Exceptions
{
    public class DuplicateEntityException : Exception
    {
        public DuplicateEntityException(string entityName)
            : base($"An {entityName} with the same unique details already exists.") { }
    }
}
