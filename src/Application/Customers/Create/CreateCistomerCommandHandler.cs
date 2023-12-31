using Domain.Customers;
using Domain.DomainErrors;
using Domain.Primitives;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Customers.Create;
public sealed class CreateCistomerCommandHandler : IRequestHandler<CreateCustomerCommand,ErrorOr<Unit>>
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
            if (PhoneNumber.Create(command.PhoneNumber) is not PhoneNumber phoneNumber)
                return Errors.Customer.PhoneNumberWithBadFormat;
            if (Address.Create(command.Country, command.Line1, command.Line2, command.City, command.State, command.ZipCode)
            is not Address address)
                return Errors.Customer.AddressWithBadFormat;
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
       
    }
}