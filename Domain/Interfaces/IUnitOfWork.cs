namespace Domain.Interfaces;

public interface IUnitOfWork
{
    void Begin();
    Task BeginAsync();
    void Commit();
    Task CommitAsync();
    void Dispose();
    void Rollback();
    Task RollbackAsync();
}
