using Blog.Core.Entities;
using Blog.Data.Context;
using Blog.Data.Repositories.Concretes;
using Blog.Data.Repositories.Contracts;

namespace Blog.Data.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext dbContext;

    public UnitOfWork(AppDbContext appDbContext)
    {
        dbContext = appDbContext;
    }
    
    public async ValueTask DisposeAsync()
    {
        await dbContext.DisposeAsync();
    }

    public IRepository<T> GetRepository<T>() where T : class, IEntityBase, new()
    {
        return new Repository<T>(dbContext);
    }

    public async Task<int> SaveAsync()
    {
        return await dbContext.SaveChangesAsync();
    }

    public int Save()
    {
        return dbContext.SaveChanges();
    }
}