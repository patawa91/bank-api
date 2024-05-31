using Bank.Domain.Models;

namespace Bank.Domain.Services;

/// <summary>
/// Service for dealing with customer actions for accounts.
/// </summary>
public interface IAccountCustomerService
{
    /// <summary>
    /// Deposit money into an account.
    /// </summary>
    /// <param name="deposit">Deposit data.</param>
    Account Deposit(Deposit deposit);

    /// <summary>
    /// Withdraw money from an account.
    /// </summary>
    /// <param name="withdrawal">Withdrawal data.</param>
    Account Withdraw(Withdrawal withdrawal);

    /// <summary>
    /// Create a new account for a customer.
    /// </summary>
    /// <param name="customerId">Id of the customer.</param>
    /// <param name="amount">Inititial amount in the account.</param>
    /// <param name="accountType">Type of account. e.g. checking/savings.</param>
    Account CreateAccount(int customerId, decimal amount, AccountType accountType);

    /// <summary>
    /// Close an account.
    /// </summary>
    /// <param name="accountClose">Closing data.</param>
    Account CloseAccount(AccountClose accountClose);
}
