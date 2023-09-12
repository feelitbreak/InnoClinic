using InnoClinic.Domain.Entities;

namespace InnoClinic.Domain.DTOs
{
    public class OfficeDto
    {
        public byte[]? Photo { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string HouseNumber { get; set; }

        public string OfficeNumber { get; set; }

        public long RegistryPhoneNumber { get; set; }

        public static void ToDomain(OfficeDto officeDto, Office office)
        {
            office.City = officeDto.City.Trim();
            office.HouseNumber = officeDto.HouseNumber.Trim();
            office.OfficeNumber = officeDto.OfficeNumber.Trim();
            office.Photo = officeDto.Photo;
            office.RegistryPhoneNumber = officeDto.RegistryPhoneNumber;
            office.Street = officeDto.Street.Trim();
        }
    }
}
