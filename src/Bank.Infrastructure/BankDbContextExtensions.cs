namespace Bank.Infrastructure;

public static class BankDbContextExtensions
{
    public static void SeedData(this BankDbContext context)
    {
        if (!context.Customers.Any())
        {
            context.Customers.AddRange(
           new Models.Customer(1, "John", "Doe", "1234"),
           new Models.Customer(2, "Jane", "Doe", "5678"),
           new Models.Customer(3, "Alice", "Smith", "9012"));
        }


        context.SaveChanges();
    }
}
