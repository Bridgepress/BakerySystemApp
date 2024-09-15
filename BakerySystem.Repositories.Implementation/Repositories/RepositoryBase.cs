using BakerySystem.DataAccess;
using BakerySystem.Domain.Entities;
using BakerySystem.Repositories.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BakerySystem.Repositories.Implementation.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : EntityBase
    {
        protected readonly ApplicationDbContext Context;

        protected RepositoryBase(ApplicationDbContext context)
        {
            Context = context;
        }

        public IQueryable<TEntity> GetAll() => Context.Set<TEntity>();

        public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken token)
            => await Context.Set<TEntity>().SingleOrDefaultAsync(entity => entity.Id == id, token);

        public TEntity Create(TEntity entity)
        {
            Context.Add(entity);
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            Context.Update(entity);
            return entity;
        }

        public void Delete(TEntity entity)
        {
            Context.Remove(entity);
        }
    }
}
