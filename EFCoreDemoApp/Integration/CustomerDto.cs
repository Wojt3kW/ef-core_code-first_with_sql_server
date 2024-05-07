using EFCoreDemoApp.DBModels;

namespace EFCoreDemoApp.Integration
{
    public record class CustomerDto(int Id, string Name, AddressDto Address)
    {
        public CustomerDto(Customer customer) :
            this(customer.Id, customer.Name, new AddressDto(customer.Address))
        {

        }
    }
}
