namespace Bank.Contracts;

/// <summary>
/// Deposit money into an account.
/// </summary>
/// <param name="CustomerId">The customer id.</param>
/// <param name="AccountId">The account id.</param>
/// <param name="Amount">The amount to deposit.</param>
public sealed record Deposit(int CustomerId, int AccountId, decimal Amount)
{
}