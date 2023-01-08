using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class AddressDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string SurName { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string ZipCode { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}
