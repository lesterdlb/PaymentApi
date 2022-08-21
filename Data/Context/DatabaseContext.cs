using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public class DatabaseContext : DbContext
{
    public DbSet<Param> Params { get; set; }
    public DbSet<CreditCard> CreditCards { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseInMemoryDatabase("PaymentApiDB");
    }
}