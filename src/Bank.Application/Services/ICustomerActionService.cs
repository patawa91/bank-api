using Bank.Contracts;

namespace Bank.Application.Services;

/// <summary>
/// Service for managing different actions customers can perform.
/// </summary>
public interface ICustomerActionService
{
    /// <summary>
    /// Deposit money into an account.
    /// </summary>
    /// <param name="deposit">Deposit data.</param>
    /// <returns>Result of deposit.</returns>
    DepositResult Deposit(Deposit deposit);

    /// <summary>
    /// Withdraw money from an account.
    /// </summary>
    /// <param name="withdrawal">Withdrawal data.</param>
    /// <returns>Result of withdraw.</returns>
    WithdrawalResult Withdraw(Withdrawal withdrawal);

    /// <summary>
    /// Create a new account for a customer.
    /// </summary>
    /// <param name="accountCreate">Create data.</param>
    /// <returns>Result of create.</returns>
    AccountCreatedResult CreateAccount(AccountCreate accountCreate);

    /// <summary>
    /// Close an account.
    /// </summary>
    /// <param name="accountClose">Close data.</param>
    /// <returns>Result of closing.</returns>
    AccountCloseResult AccountCloseResult(AccountClose accountClose);
}
