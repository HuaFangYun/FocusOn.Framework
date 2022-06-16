namespace MiniSolution.ApplicationContracts.DTO;

public interface IHasQueryable<TEntity> where TEntity : class
{
    IQueryable<TEntity> Query(IQueryable<TEntity> source);
}
