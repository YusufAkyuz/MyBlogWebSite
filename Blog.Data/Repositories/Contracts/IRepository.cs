using System.Linq.Expressions;
using Blog.Core.Entities;

namespace Blog.Data.Repositories.Contracts;

public interface IRepository<T> where T : class, IEntityBase, new()
{
    Task AddAsync(T Entity);

    Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null,
        params Expression<Func<T, Object>>[] includeProperties);

    Task<T> GetAsync(Expression<Func<T, bool>> predicate,
        params Expression<Func<T, Object>>[] includeProperties);

    Task<T> GetByGuidAsync(Guid id);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    //AnyAsync metodu, genellikle bir veritabanı sorgusunu gerçekleştirmek için kullanılır.
    //Örneğin, bir koleksiyonda belirli bir koşulu sağlayan varlık var mı yok mu diye kontrol etmek için kullanılabilir.
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    //CountAsync metodu, belirli bir koşulu sağlayan varlık sayısını saymak için kullanılır.
    //Bu metodun amacı veritabanında veya herhangi bir koleksiyonda belirli bir filtre
    //veya koşulu sağlayan nesnelerin sayısını döndürmektir.
    Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
}