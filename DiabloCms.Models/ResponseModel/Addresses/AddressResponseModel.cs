using DiabloCms.Entities.Models;
using DiabloCms.Models.Mapper;

namespace DiabloCms.Models.ResponseModel.Addresses
{
    public class AddressResponseModel : IMapFrom<Address>
    {
        public string Id { get; set; }

        public string Country { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Description { get; set; }

        public string ZipCode { get; set; }

        public string PhoneNumber { get; set; }
    }
}