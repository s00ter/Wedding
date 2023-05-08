using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Wedding.DAL.Data;

namespace Wedding.DAL.Repository;

public abstract class BaseRepository<TEntity, TId> : IBaseRepository<TEntity, TId>
    where TEntity : class
{
    protected abstract Expression<Func<TEntity, bool>> GetByIdExpression(TId id);

    protected abstract IQueryable<TEntity> IncludeChildrens(IQueryable<TEntity> query);

    protected WeddingContext Context;

    protected BaseRepository(WeddingContext weddingContext)
    {
        Context = weddingContext;
    }

    public async Task Create(TEntity entity)
    {
        Context.Add(entity);
        await Context.SaveChangesAsync();
    }

    public async Task Delete(TEntity entity)
    {
        Context.Remove(entity);
        await Context.SaveChangesAsync();
    }

    public IQueryable<TEntity> GetQuery()
    {
        var query = Context.Set<TEntity>()
            .AsNoTracking();

        return query;
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        var showAll = Context.Set<TEntity>().AsQueryable();

        showAll = IncludeChildrens(showAll);

        return await showAll.ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(TId id)
    {
        var showOne = Context.Set<TEntity>();
        return await showOne.FirstOrDefaultAsync(GetByIdExpression(id));
    }

    public async Task<TEntity> Update(TEntity entity)
    {
        Context.Update(entity);
        await Context.SaveChangesAsync();

        return entity;
    }
}