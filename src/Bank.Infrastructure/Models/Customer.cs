namespace Bank.Infrastructure.Models;

public sealed record Customer(int CustomerId, string FirstName, string LastName, string PinHashed)
{
}
