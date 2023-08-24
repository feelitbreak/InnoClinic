namespace InnoClinic.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public bool IsActive { get; set; } = true;

        private DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
