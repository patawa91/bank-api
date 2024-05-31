namespace Bank.Contracts;

/// <summary>
/// Result of creating an account.
/// </summary>
/// <param name="CustomerId">The customer id.</param>
/// <param name="AccountId">The account id.</param>
/// <param name="AccountTypeId">The type of account this is.</param>
/// <param name="Balance">The balance of the account after this operation.</param>
/// <param name="Succeeded">Did this operation succeed or not.</param>
public sealed record AccountCreatedResult(int CustomerId, int AccountId, AccountType AccountTypeId, decimal Balance, bool Succeeded)
{
}
