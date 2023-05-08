using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Wedding.DAL.Data;
using Wedding.DAL.Data.Entities;
using Wedding.DAL.Repository.Abstractions;

namespace Wedding.DAL.Repository.Implementations;

public class ServiceRepository : BaseRepository<Service, Guid>, IServiceRepository
{
    public ServiceRepository(WeddingContext weddingContext) : base(weddingContext)
    {
    }

    protected override Expression<Func<Service, bool>> GetByIdExpression(Guid id)
    {
        return s => s.Id == id;
    }

    protected override IQueryable<Service> IncludeChildrens(IQueryable<Service> query)
    {
        return query
            .Include(s => s.OrderServices);
    }
}