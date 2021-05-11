using System.ComponentModel.DataAnnotations;
using DiabloCms.Shared.ConstContent;

namespace DiabloCms.Models.RequestModel.Addresses
{
    using static ModelConstants.Address;

    public class AddressRequestModel
    {
        [Required] [MaxLength(CountryLength)] public string Country { get; set; }

        [Required] [MaxLength(StateLength)] public string State { get; set; }

        [Required] [MaxLength(CityLength)] public string City { get; set; }

        [Required]
        [MaxLength(DescriptionLength)]
        public string Description { get; set; }

        [MaxLength(PostalCodeLength)] public string ZipCode { get; set; }

        [Required]
        [MinLength(MinPhoneNumberLength)]
        [MaxLength(PhoneNumberLength)]
        [RegularExpression(PhoneNumberRegularExpression)]
        public string PhoneNumber { get; set; }
    }
}