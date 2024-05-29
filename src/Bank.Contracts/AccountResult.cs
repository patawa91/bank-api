namespace Bank.Contracts;

/// <summary>
/// The result of an account operation.
/// </summary>
/// <param name="CustomerId">The customer id.</param>
/// <param name="AccountId">The account id.</param>
/// <param name="Succeeded">Did this operation succeed or not.</param>
public record AccountResult(int CustomerId, int AccountId, bool Succeeded)
{
}
