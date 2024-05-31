namespace Bank.Infrastructure.Models;

public sealed record Account(int AccountId, int CustomerId, decimal Balance, AccountType AccountType, AccountStatus AccountStatus)
{
}
