using Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence.Repositories;
public class CustomerRespository : ICustomerRepository
{
    private readonly ApplicationDbContext context;

    public CustomerRespository(ApplicationDbContext context)
    {
        this.context = context?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Add(Customer customer)=>
    await context.Customers.AddAsync(customer);

    public async Task<Customer?> GetByIdAsync(CustomerId id)=>
    await context.Customers.SingleOrDefaultAsync(c=>c.Id==id);
}