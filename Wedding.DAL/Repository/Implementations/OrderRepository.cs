using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Wedding.DAL.Data;
using Wedding.DAL.Data.Entities;
using Wedding.DAL.Repository.Abstractions;

namespace Wedding.DAL.Repository.Implementations
{
    public class OrderRepository : BaseRepository<Order, Guid>, IOrderRepository
    {
        public OrderRepository(WeddingContext weddingContext) : base(weddingContext)
        {
        }

        protected override Expression<Func<Order, bool>> GetByIdExpression(Guid id)
        {
            return o => o.Id == id;
        }

        protected override IQueryable<Order> IncludeChildrens(IQueryable<Order> query)
        {
            return query
                .Include(o => o.PickupSalon)
                .Include(o => o.Client)
                .Include(o => o.OrderServices)
                .Include(o => o.OrderWares);
        }
    }
}
