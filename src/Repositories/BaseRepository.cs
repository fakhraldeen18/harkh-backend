using Harkh_backend.src.Abstractions;
using Harkh_backend.src.Databases;
using Microsoft.EntityFrameworkCore;

namespace Harkh_backend.src.Repositories;
public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly DatabaseContext _databaseContext;
    private readonly DbSet<T> data;

    public BaseRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        data = _databaseContext.Set<T>();
    }

    public T CreateOne(T newObj)
    {
        data.Add(newObj);
        return newObj;
    }

    public T? DeleteOne(Guid id)
    {
        T? findObj = FindOne(id);
        if (findObj == null) return null;
        data.Remove(findObj);
        return findObj;
    }

    public IEnumerable<T> FindAll()
    {
        return data;
    }

    public T? FindOne(Guid id)
    {
        T? obj = data.Find(id);
        return obj;
    }

    public T UpdateOne(T updatedObj)
    {
        data.Update(updatedObj);
        return updatedObj;
    }

    // public T? Find(Expression<Func<T, bool>> match, string[] includes = null)
    // {
    //     IQueryable<T> query = data;
    //     if (includes != null)
    //         foreach (var include in includes)
    //             query = query.Include(include);
    //     var obj = query.SingleOrDefault(match);
    //     return obj;
    // }
    
    // public T? Find(Expression<Func<T, bool>> match)
    // {
    //     var obj = data.SingleOrDefault(match);
    //     return obj;
    // }


    // public IEnumerable<T> FindAllByName(Expression<Func<T, bool>> match, string[] includes = null)
    // {
    //     IQueryable<T> query = data;
    //     if (includes != null)
    //         foreach (var include in includes)
    //             query = query.Include(include);
    //     var obj = query.Where(match);
    //     return obj;
    // }
}
