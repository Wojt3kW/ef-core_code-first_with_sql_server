using EFCoreDemoApp.DBModels;

namespace EFCoreDemoApp.Integration
{
    public record class OrderDto(int Id, string Contents, AddressDto ShippingAddress, AddressDto BillingAddress, CustomerDto Customer)
    {
        public OrderDto(Order order)
            : this(order.Id, order.Contents, new AddressDto(order.ShippingAddress), new AddressDto(order.BillingAddress), new CustomerDto(order.Customer))
        {

        }
    }
}
