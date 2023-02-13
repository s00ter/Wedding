﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wedding.DAL.Repository
{
    public interface IBaseRepository<TEntity, TId>
        where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync();  

        Task<TEntity> GetByIdAsync(TId id);

        Task Create(TEntity entity);

        Task<TEntity> Update(TEntity entity);

        Task Delete(TEntity entity);
    }
}