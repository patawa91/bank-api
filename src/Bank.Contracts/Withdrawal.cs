namespace Bank.Contracts;

/// <summary>
/// Withdraw money from an account.
/// </summary>
/// <param name="CustomerId">The customer id.</param>
/// <param name="AccountId">The account id.</param>
/// <param name="Amount">The amount to deposit.</param>
public sealed record Withdrawal(int CustomerId, int AccountId, decimal Amount)
{
}
