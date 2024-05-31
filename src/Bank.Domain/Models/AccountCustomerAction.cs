namespace Bank.Domain.Models;

/// <summary>
/// An action performed by a customer.
/// </summary>
/// <param name="CustomerId">Id of the customer.</param>
/// <param name="AccountId">Id of the account.</param>
/// <param name="Amount">The amount associated with the action.</param>
public abstract record AccountCustomerAction(int CustomerId, int AccountId, decimal Amount)
{
}
