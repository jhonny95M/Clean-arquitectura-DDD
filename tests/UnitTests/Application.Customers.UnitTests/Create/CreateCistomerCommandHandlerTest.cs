using Application.Customers.Create;
using Domain.Customers;
using Domain.DomainErrors;
using Domain.Primitives;

namespace Application.Customers.UnitTests.Create;

public class CreateCistomerCommandHandlerTest
{
    private readonly Mock<ICustomerRepository> mockCustomerRepository;
    private readonly Mock<IUnitOfWork> mockUnitOfWork;
    private readonly CreateCistomerCommandHandler handler;

    public CreateCistomerCommandHandlerTest()
    {
        this.mockCustomerRepository = new Mock<ICustomerRepository>();
        this.mockUnitOfWork = new Mock<IUnitOfWork>();
        this.handler = new CreateCistomerCommandHandler(mockCustomerRepository.Object,mockUnitOfWork.Object);
    }

    [Fact]
    public async Task HandleCreateCustomer_WhenPhoneNumberHasBadFormat_ShouldReturnValidationError()
    {
        //Arrange
        CreateCustomerCommand command =new CreateCustomerCommand("Fernando","Ventura","fe93@mc.com","33049439443","","","","","","");
        //Act
        var result=await handler.Handle(command,default);
        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorOr.ErrorType.Validation);
        result.FirstError.Code.Should().Be(Errors.Customer.PhoneNumberWithBadFormat.Code);
        result.FirstError.Description.Should().Be(Errors.Customer.PhoneNumberWithBadFormat.Description);
    }
}