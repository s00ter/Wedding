using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Wedding.DAL.Data;
using Wedding.DAL.Data.Entities.Abstractions;

namespace Wedding.DAL.Repository
{
    public abstract class BaseRepository<TEntity, TId> : IBaseRepository<TEntity, TId>
        where TEntity : class
    {
        protected abstract Expression<Func<TEntity, bool>> GetByIdExpression(TId id);

        protected abstract IQueryable<TEntity> IncludeChildrens(IQueryable<TEntity> query);

        protected WeddingContext WeddingContext;

        protected BaseRepository(WeddingContext weddingContext)
        {
            WeddingContext = weddingContext;
        }

        public async Task Create(TEntity entity)
        {
            WeddingContext.Add(entity);
            await WeddingContext.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity)
        {
            WeddingContext.Remove(entity);
            await WeddingContext.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            var showAll = WeddingContext.Set<TEntity>().AsQueryable();

            showAll = IncludeChildrens(showAll);

            return await showAll.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(TId id)
        {
            var showOne = WeddingContext.Set<TEntity>();
            return await showOne.FirstOrDefaultAsync(GetByIdExpression(id));
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            WeddingContext.Update(entity);
            await WeddingContext.SaveChangesAsync();

            return entity;
        }
    }
}
