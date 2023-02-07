using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Wedding.DAL.Data;
using Wedding.DAL.Data.Entities;
using Wedding.DAL.Repository.Abstractions;

namespace Wedding.DAL.Repository.Implementations
{
    public class WareRepository : BaseRepository<Ware, Guid>, IWareRepository
    {
        public WareRepository(WeddingContext weddingContext) : base(weddingContext)
        {
        }

        protected override Expression<Func<Ware, bool>> GetByIdExpression(Guid id)
        {
            return w => w.Id == id;
        }

        protected override IQueryable<Ware> IncludeChildrens(IQueryable<Ware> query)
        {
            return query
                .Include(w => w.Category)
                .Include(w => w.OrderWares);
        }
    }
}
