namespace Bank.Domain.Models;

/// <summary>
/// Withdraw money from an account.
/// </summary>
public sealed record Withdrawal(int CustomerId, int AccountId, decimal Amount) : AccountCustomerAction(CustomerId, AccountId, Amount)
{
}
