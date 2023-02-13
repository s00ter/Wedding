using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wedding.DAL.Data.Entities;

namespace Wedding.DAL.Repository.Abstractions
{
    public interface IWareCategoryRepository : IBaseRepository<WareCategory, Guid>
    {

    }
}
