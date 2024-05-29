namespace Bank.Contracts;

/// <summary>
/// The account to close.
/// </summary>
/// <param name="CustomerId">The customer id.</param>
/// <param name="AccountId">The account id.</param>
public sealed record AccountClose(int CustomerId, int AccountId)
{
}
