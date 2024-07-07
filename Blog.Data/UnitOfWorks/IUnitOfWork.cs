using Blog.Core.Entities;
using Blog.Data.Repositories.Contracts;

namespace Blog.Data.UnitOfWorks;

public interface IUnitOfWork : IAsyncDisposable
{
       IRepository<T> GetRepository<T>() where T : class, IEntityBase, new();
       Task<int> SaveAsync();
       int Save();
}