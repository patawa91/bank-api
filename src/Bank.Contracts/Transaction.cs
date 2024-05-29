namespace Bank.Contracts;

/// <summary>
/// Represents a transaction that can be taken against an account.
/// </summary>
public abstract record Transaction(int CustomerId, int AccountId, decimal Amount)
{
}
