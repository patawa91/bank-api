namespace Bank.Contracts;

/// <summary>
/// Represents the result of a transaction such as a deposit or withdrawal.
/// </summary>
public sealed record class TransactionResult(int CustomerId, int AccountId, decimal Balance, bool Succeeded)
{
}
