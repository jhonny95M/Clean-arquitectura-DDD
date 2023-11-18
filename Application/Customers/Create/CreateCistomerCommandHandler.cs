using Domain.Customers;
using Domain.Primitives;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Customers.Create;
internal sealed class CreateCistomerCommandHandler : IRequestHandler<CreateCustomerCommand,ErrorOr<Unit>>
{
    private readonly ICustomerRepository customerRepository;
    private readonly IUnitOfWork unitOdWork;

    public CreateCistomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOdWork)
    {
        this.customerRepository = customerRepository?? throw new ArgumentNullException(nameof(customerRepository));
        this.unitOdWork = unitOdWork?? throw new ArgumentNullException(nameof(unitOdWork));;
    }

    public async Task<ErrorOr<Unit>> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        try
        {
            if (PhoneNumber.Create(command.PhoneNumber) is not PhoneNumber phoneNumber)
                return Error.Validation("Customer.PhonNumber", "Phone number has not format.");
            if (Address.Create(command.Country, command.Line1, command.Line2, command.City, command.State, command.ZipCode)
            is not Address address)
                return Error.Validation("Customer.Address", "Address is not valid.");
            var customer = new Customer(
                new CustomerId(Guid.NewGuid()),
                command.Name,
                command.LastName,
                command.Email,
                phoneNumber,
                address,
                true
            );
            await customerRepository.Add(customer);
            await unitOdWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }catch (Exception ex)
        {
            return Error.Failure("CreateCustomer.Failure ", ex.Message);
        }
    }
}