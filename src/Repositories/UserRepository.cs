using Todo_backend.src.Abstractions;
using Todo_backend.src.Databases;
using Todo_backend.src.Entities;
using Microsoft.EntityFrameworkCore;

namespace Todo_backend.src.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DbSet<User> _users;
    private readonly DatabaseContext _databaseContext;

    public UserRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        _users = databaseContext.Users;
    }

    public User CreateOne(User NewUser)
    {
        _users.Add(NewUser);
        _databaseContext.SaveChanges();
        return NewUser;
    }

    public User? DeleteOne(Guid id)
    {
        User? FindUser = FindOne(id);
        if (FindUser == null) return null;
        _users.Remove(FindUser);
        _databaseContext.SaveChanges();
        return FindUser;
    }

    public IEnumerable<User> FindAll()
    {
        IEnumerable<User> users = _users;
        return users;
    }

    public User? FindOne(Guid id)
    {
        User? FindOne = _users.FirstOrDefault(u => u.Id == id);
        return FindOne;
    }

    public User UpdateOne(User UpdatedUser)
    {
        _users.Update(UpdatedUser);
        _databaseContext.SaveChanges();
        return UpdatedUser;
    }
}