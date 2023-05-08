using Microsoft.AspNetCore.Mvc;
using Wedding.DAL.Repository;

namespace Wedding.AdminPanel.Controllers.Abstractions;

public abstract class ReadWriteControllerBase<TEntity,TId,TRepository> : Controller
    where TEntity : class 
    where TRepository : IBaseRepository<TEntity,TId>
{
    protected ReadWriteControllerBase(TRepository repository)
    {
        Repository = repository;
    }

    protected readonly TRepository Repository;


}