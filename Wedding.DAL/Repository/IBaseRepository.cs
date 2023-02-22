namespace Wedding.DAL.Repository
{
    public interface IBaseRepository<TEntity, TId>
        where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync();  

        Task<TEntity> GetByIdAsync(TId id);

        Task Create(TEntity entity);

        Task<TEntity> Update(TEntity entity);

        Task Delete(TEntity entity);

        IQueryable<TEntity> GetQuery();
    }
}