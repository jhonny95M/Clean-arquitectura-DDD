using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Customers.Create;

public class CreateCustomerCommandValidator:AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MaximumLength(50);
        RuleFor(c => c.LastName).NotEmpty().MaximumLength(50).WithName("Last Name");
        RuleFor(c => c.Email).NotEmpty().EmailAddress().MaximumLength(255);
        RuleFor(c => c.PhoneNumber).NotEmpty().MaximumLength(9).WithName("Phone Number");
        RuleFor(c => c.Country).NotEmpty().MaximumLength(3);
        RuleFor(c => c.Line1).NotEmpty().MaximumLength(20).WithName("Line 1");
        RuleFor(c => c.Line2).NotEmpty().MaximumLength(20).WithName("Line 2");
        RuleFor(c => c.City).NotEmpty().MaximumLength(40);
        RuleFor(c => c.State).NotEmpty().MaximumLength(40);
        RuleFor(c => c.ZipCode).NotEmpty().MaximumLength(10).WithName("Zip Code");
    }
}
