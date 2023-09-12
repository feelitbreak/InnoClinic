namespace InnoClinic.Domain.Entities
{
    public class Patient : BaseProfile
    {
        public DateTime DateOfBirth { get; set; }

        public bool IsLinkedToAccount { get; set; }
    }
}
