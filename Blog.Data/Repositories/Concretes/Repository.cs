using System.Linq.Expressions;
using Blog.Core.Entities;
using Blog.Data.Context;
using Blog.Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repositories.Concretes;

public class Repository<T> : IRepository<T> where T : class, IEntityBase, new()
{
    private readonly AppDbContext _dbContext;

    public Repository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    //TODO : Tabloya ulaşmamız gerekiyor
    private DbSet<T> Table
    {
        get => _dbContext.Set<T>();
    }
    
    /*
     * Asenkron olarak tüm T türündeki varlıkları getirmeyi sağlar GetAllAsync metodu.
     * İsteğe bağlı olarak bir filtreleme koşulu (predicate) ve dahil edilecek özellikler (includeProperties) sağlanabilir.
     * Task<List<T>> döndürür, yani bir liste döndürecektir.
     * dahil edilecek özellikler (includeProperties) sağlanabilir. 
     */
    
    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, 
        params Expression<Func<T, Object>> [] includeProperties)
    {
        IQueryable<T> query = Table;
        if (predicate is not null)
        {
            query = query.Where(predicate); 
        }

        if (includeProperties.Any())
        {
            foreach (var item in includeProperties)
            {
                query = query.Include(item);
            }
        }
        return await query.ToListAsync();
    }
    
    //Tek başına kullanılan Task void görevi görecek. Ulastığımız Table sayesinde tabloya veri eklicek
    public async Task AddAsync(T entity)
    {
        await Table.AddAsync(entity);
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = Table;
        query = query.Where(predicate);
        if (includeProperties.Any())
        {
            foreach (var item in includeProperties)
            {
                query = query.Include(item);
            }
        }

        return await query.SingleAsync();
    }

    public async Task<T> GetByGuidAsync(Guid id)
    {
        return await Table.FindAsync(id);
    }

    public async Task<T> UpdateAsync(T entity)
    {
        await Task.Run(() => Table.Update(entity));
        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        await Task.Run(() => Table.Remove(entity));
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
        return await Table.AnyAsync(predicate);
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
    {
        return await Table.CountAsync(predicate);
    }
    
}