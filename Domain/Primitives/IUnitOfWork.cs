namespace Domain.Primitives;
public interface IUnitOdWork
{
    Task<int>SaveChangesAsync(CancellationToken cancellationToken=default);
}