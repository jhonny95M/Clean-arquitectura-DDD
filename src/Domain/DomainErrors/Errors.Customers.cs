using ErrorOr;

namespace Domain.DomainErrors;
public partial class Errors
{
    public static class Customer
    {
        public static Error PhoneNumberWithBadFormat=>Error.Validation("Customer.PhonNumber", "Phone number has not format.");
        public static Error AddressWithBadFormat=>Error.Validation("Customer.Address", "Address is not valid.");
    }
}