namespace InnoClinic.Domain.Models
{
    public class PasswordModel
    {
        public byte[] Key { get; set; }

        public byte[] Salt { get; set; }
    }
}
