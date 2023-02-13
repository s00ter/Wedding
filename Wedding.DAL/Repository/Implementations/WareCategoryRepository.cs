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

        public async Task<(int Total, List<Ware> Wares)> GetWaresByFilter
            (int skip, int take, Guid categoryId, int? priceFrom, int? priceTo, string search)
        {
            var query = Context.Wares
                .Where(w => w.CategoryId == categoryId
                        && (priceFrom.HasValue && priceFrom >= w.RetailPrice)
                        && (priceTo.HasValue && priceTo <= w.RetailPrice)
                        && (search != null && w.Name.ToLower().Contains(search.ToLower())));

            var total = await query.CountAsync();

            var result = await query
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return (total, result);
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
