
namespace Harkh_backend.src.Abstractions;

public interface IBaseRepository<T> where T : class
{
    public IEnumerable<T> FindAll();
    public T? FindOne(Guid id);
    // public T? Find(Expression<Func<T, bool>> match, string[] includes = null);
    // public IEnumerable<T> FindAllByName(Expression<Func<T, bool>> match, string[] includes = null);
    public T CreateOne(T newObj);
    public T UpdateOne(T updatedObj);
    public T? DeleteOne(Guid id);
}
