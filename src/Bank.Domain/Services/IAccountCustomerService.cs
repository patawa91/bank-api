﻿using Bank.Domain.Models;

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
    Task<Account> DepositAsync(Deposit deposit);

    /// <summary>
    /// Withdraw money from an account.
    /// </summary>
    /// <param name="withdrawal">Withdrawal data.</param>
    Task<Account> WithdrawAsync(Withdrawal withdrawal);

    /// <summary>
    /// Create a new account for a customer.
    /// </summary>
    /// <param name="customerId">Id of the customer.</param>
    /// <param name="amount">Inititial amount in the account.</param>
    /// <param name="accountType">Type of account. e.g. checking/savings.</param>
    Task<Account> CreateAccountAsync(int customerId, decimal amount, AccountType accountType);

    /// <summary>
    /// Close an account.
    /// </summary>
    /// <param name="accountClose">Closing data.</param>
    Task<Account> CloseAccountAsync(AccountClose accountClose);
}
