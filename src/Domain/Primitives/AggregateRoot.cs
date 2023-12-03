namespace Domain.Primitives;
public abstract class AggregateRoot
{
    private readonly List<DomainEvent> domainEvents=new();
    public ICollection<DomainEvent>GetDomainEvents()=>domainEvents;
    protected void Raise(DomainEvent domainEvent)=>domainEvents.Add(domainEvent);
}