using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
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

        public async Task<(decimal Min, decimal Max)> GetCategoryPricesRange(Guid categoryId)
        {
            var result = await Context.WareCategories
                .Where(x => x.Id == categoryId)
                .Select(x => new
                {
                    Min = x.Wares
                        .Select(w => w.Price)
                        .Min(),
                    Max = x.Wares
                        .Select(w => w.Price)
                        .Max()
                })
                .FirstAsync();

            return (result.Min ,result.Max);
        }

        public async Task<(int Total, List<Ware> Wares)> GetWaresByFilter
            (int skip, int take, Guid categoryId, int? priceFrom, int? priceTo, string search, bool priceDesc)
        {
            var query = Context.Wares
                .Where(w => w.CategoryId == categoryId
                            && (!priceFrom.HasValue || priceFrom <= w.RetailPrice)
                            && (!priceTo.HasValue || priceTo >= w.RetailPrice)
                            && (search == null || w.Name.ToLower().Contains(search.ToLower())));

            query = priceDesc
                ? query.OrderByDescending(w => w.Price)
                : query.OrderBy(w => w.Price);

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