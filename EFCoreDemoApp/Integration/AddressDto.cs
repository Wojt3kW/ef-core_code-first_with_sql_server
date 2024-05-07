using EFCoreDemoApp.DBModels;

namespace EFCoreDemoApp.Integration
{
    public record class AddressDto(string Line1, string? Line2, string City, string Country, string PostCode)
    {
        public AddressDto(Address address)
            : this(address.Line1, address.Line2, address.City, address.Country, address.PostCode)
        {

        }
    }
}
