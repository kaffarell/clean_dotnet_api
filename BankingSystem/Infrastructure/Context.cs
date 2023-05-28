using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Infrastructure;

public class Context : DbContext
{
    public Context(DbContextOptions options) : base(options) { }

    public DbSet<Account> Accounts {get; set;}
    public DbSet<User> Users {get; set;}
}