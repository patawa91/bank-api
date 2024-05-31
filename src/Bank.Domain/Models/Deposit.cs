namespace Bank.Domain.Models;

/// <summary>
/// Deposit money into an account.
/// </summary>
public sealed record Deposit(int CustomerId, int AccountId, decimal Amount) : AccountCustomerAction(CustomerId, AccountId, Amount)
{
}
