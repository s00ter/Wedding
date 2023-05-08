using Wedding.DAL.Data.Entities;

namespace Wedding.DAL.Repository.Abstractions;

public interface IWareCategoryRepository : IBaseRepository<WareCategory, Guid>
{
    Task<(int Total, List<Ware> Wares)> GetWaresByFilter
        (int skip, int take, Guid categoryId, int? priceFrom, int? priceTo, string search, bool priceDesc);

    Task<(decimal Min, decimal Max)> GetCategoryPricesRange
        (Guid categoryId);
}