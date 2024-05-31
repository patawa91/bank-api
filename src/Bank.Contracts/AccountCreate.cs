namespace Bank.Contracts;

/// <summary>
/// The account to create.
/// <param name="CustomerId">The customer id.</param>
/// <param name="InitialDeposit">The initial amount to deposit.</param>
/// <param name="AccountTypeId">The type of account this is.</param>
/// </summary>
public sealed record AccountCreate(int CustomerId, decimal InitialDeposit, AccountType AccountTypeId)
{
}
