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
    public class WareCategoryRepository : BaseRepository<WareCategory, Guid>, IWareCategoryRepository
    {
        public WareCategoryRepository(WeddingContext weddingContext) : base(weddingContext)
        {
        }

        protected override Expression<Func<WareCategory, bool>> GetByIdExpression(Guid id)
        {
            return c => c.Id == id;
        }

        protected override IQueryable<WareCategory> IncludeChildrens(IQueryable<WareCategory> query)
        {
            return query
                .Include(w => w.Wares);
        }
    }
}
