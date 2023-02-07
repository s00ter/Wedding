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
    public class SalonRepository : BaseRepository<Salon, int>, ISalonRepository
    {
        public SalonRepository(WeddingContext weddingContext) : base(weddingContext)
        {
        }

        protected override Expression<Func<Salon, bool>> GetByIdExpression(int id)
        {
            return s => s.Id == id;
        }

        protected override IQueryable<Salon> IncludeChildrens(IQueryable<Salon> query)
        {
            return query
                .Include(s => s.City)
                .Include(s => s.Orders);
        }
    }
}
