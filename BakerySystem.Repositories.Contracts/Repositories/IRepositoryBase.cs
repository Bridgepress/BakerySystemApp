using BakerySystem.Domain.Entities;

namespace BakerySystem.Repositories.Contracts.Repositories
{
    public interface IRepositoryBase<TEntity>
        where TEntity : EntityBase
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity?> GetByIdAsync(Guid id, CancellationToken token);

        TEntity Create(TEntity entity);

        TEntity Update(TEntity entity);

        void Delete(TEntity entity);
    }
}
