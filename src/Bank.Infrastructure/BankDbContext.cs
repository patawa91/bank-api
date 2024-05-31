using Microsoft.EntityFrameworkCore;

namespace Bank.Infrastructure;

public class BankDbContext : DbContext
{
    public BankDbContext(DbContextOptions<BankDbContext> options) : base(options)
    {
    }

    public DbSet<Models.Customer> Customers { get; set; }
    public DbSet<Models.Account> Accounts { get; set; }
    public DbSet<Models.Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.Account>()
            .HasOne<Models.Customer>()
            .WithMany()
            .HasForeignKey(a => a.CustomerId);

        //modelBuilder.Entity<Models.Customer>().HasData(
        //    new Models.Customer(1, "John", "Doe", "1234"),
        //    new Models.Customer (2, "Jane", "Doe", "5678"),
        //    new Models.Customer (3, "Alice", "Smith", "9012")
        //);
    }
}
