using Application.Data;
using Domain.Customers;
using Domain.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence;
public class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
{
    private readonly IPublisher publisher;

    public ApplicationDbContext(DbContextOptions options,IPublisher publisher):base(options)
    {
        this.publisher = publisher;
    }

    public DbSet<Customer> Customers { get; set; }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var domainEvents=ChangeTracker.Entries<AggregateRoot>()
        .Select(c=>c.Entity)
        .Where(c=>c.GetDomainEvents().Any())
        .SelectMany(c=>c.GetDomainEvents());        
        var result=await base.SaveChangesAsync(cancellationToken);
        foreach (var item in domainEvents)
        await publisher.Publish(item,cancellationToken);
        return result;

    }
}