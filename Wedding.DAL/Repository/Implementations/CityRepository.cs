using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Wedding.DAL.Data;
using Wedding.DAL.Data.Entities;
using Wedding.DAL.Repository.Abstractions;

namespace Wedding.DAL.Repository.Implementations
{
    public class CityRepository : BaseRepository<City, int>, ICityRepository
    {
        public CityRepository(WeddingContext weddingContext) : base(weddingContext)
        {
        }

        protected override Expression<Func<City, bool>> GetByIdExpression(int id)
        {   
            return c => c.Id == id;
        }

        protected override IQueryable<City> IncludeChildrens(IQueryable<City> query)
        {
            return query
                .Include(c => c.Salons);
        }
    }
}
