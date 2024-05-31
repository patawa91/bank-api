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
    /// <param name="accountId">Id of the account.</param>
    /// <param name="deposit">Deposit data.</param>
    /// <returns>Result of deposit.</returns>
    Task<DepositResult> DepositAsync(int accountId, Deposit deposit);

    /// <summary>
    /// Withdraw money from an account.
    /// </summary>
    /// <param name="accountId">Id of the account.</param>
    /// <param name="withdrawal">Withdrawal data.</param>
    /// <returns>Result of withdraw.</returns>
    Task<WithdrawalResult> WithdrawAsync(int accountId, Withdrawal withdrawal);

    /// <summary>
    /// Create a new account for a customer.
    /// </summary>
    /// <param name="accountCreate">Create data.</param>
    /// <returns>Result of create.</returns>
    Task<AccountCreatedResult> CreateAccountAsync(AccountCreate accountCreate);

    /// <summary>
    /// Close an account.
    /// </summary>
    /// <param name="accountId">Id of the account.</param>
    /// <param name="accountClose">Close data.</param>
    /// <returns>Result of closing.</returns>
    Task<AccountCloseResult> CloseAccountAsync(int accountId, AccountClose accountClose);
}
