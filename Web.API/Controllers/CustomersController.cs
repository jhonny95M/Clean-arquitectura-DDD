using Application.Customers.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class CustomersController : ApiController
    {
        private readonly ISender mediator;
        public CustomersController(ISender sender)
        {
            this.mediator = sender;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
        {
            var createCustomer=await mediator.Send(command);
            return createCustomer.Match(customer=>Ok(),erros=>Problem(erros));
        }
    }
}
