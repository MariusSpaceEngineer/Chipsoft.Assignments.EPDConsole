namespace Chipsoft.Assignments.EPDConsole.Exceptions
{
    public class DuplicateEntityException : Exception
    {
        public DuplicateEntityException(string entityName)
            : base($"Een {entityName} met dezelfde unieke gegevens bestaat al.") { }
    }
}
