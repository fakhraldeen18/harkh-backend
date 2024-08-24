using Todo_backend.src.Entities;
using Microsoft.EntityFrameworkCore;
namespace Todo_backend.src.Databases;
public class DatabaseContext : DbContext // DbContext is built in class to give me access to database (gateway to database)
{
    public DbSet<User> Users { get; set; }
    public DbSet<ToDo> ToDos { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}







