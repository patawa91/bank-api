using Bank.Domain.Exceptions;

namespace Bank.Domain.Models;

/// <summary>
/// A bank account.
/// </summary>
/// <param name="AccountId">Id of the account.</param>
/// <param name="CustomerId">Id of customer who owns the account.</param>
/// <param name="Balance">Current balance of the account.</param>
/// <param name="AccountType">The type of account. e.g. Checking, Savings.</param>
public sealed class Account(int? AccountId, int CustomerId, decimal Balance, AccountType AccountType, AccountStatus AccountStatus)
{
    /// <summary>
    /// Id of the account.
    /// </summary>
    public int? AccountId { get; } = AccountId;

    /// <summary>
    /// Id of customer who owns the account.
    /// </summary>
    public int CustomerId { get; } = CustomerId;

    /// <summary>
    /// Current balance of the account.
    /// </summary>
    public decimal Balance { get; private set; } = Balance;

    /// <summary>
    /// The type of account. e.g. Checking, Savings.
    /// </summary>
    public AccountType AccountType { get; } = AccountType;

    private readonly IList<AccountCustomerAction> _completedCustomerActions = [];

    /// <summary>
    /// Customer actions that have been completed.
    /// </summary>
    public IReadOnlyCollection<AccountCustomerAction> CompletedCustomerActions => _completedCustomerActions.AsReadOnly();

    public AccountStatus Status { get; private set; } = AccountStatus;

    /// <summary>
    /// Deposit money into an account.
    /// </summary>
    /// <param name="deposit">Deposit data.</param>
    public void Deposit(Deposit deposit)
    {
        // Validate
        Validate(deposit);

        // Update balance
        Balance += deposit.Amount;

        // Add to completed actions
        _completedCustomerActions.Add(deposit);
    }

    /// <summary>
    /// Withdraw money from an account.
    /// </summary>
    /// <param name="withdrawal">Withdrawal data.</param>
    public void Withdraw(Withdrawal withdrawal)
    {
        // Validate
        Validate(withdrawal);

        // Update balance
        Balance -= withdrawal.Amount;

        // Add to completed actions
        _completedCustomerActions.Add(withdrawal);
    }

    public void Close(AccountClose closeAccount)
    {
        // Validate
        Validate(closeAccount);
       
        // Update status
        Status = AccountStatus.Closed;

        // Add to completed actions
        _completedCustomerActions.Add(closeAccount);
    }

    private void Validate(Deposit deposit)
    {
        List<string> errors = [];
        if (deposit is not { })
        {
            errors.Add(AccountCustomerValidationException.DepositMustExist());
            throw new AccountCustomerValidationException(this, deposit, errors);
        }

        if (!deposit.CustomerId.Equals(CustomerId))
        {
            errors.Add(AccountCustomerValidationException.DepositCustomerMustMatch());
        }

        if (!deposit.AccountId.Equals(AccountId))
        {
            errors.Add(AccountCustomerValidationException.DepositAccountMustMatch());
        }

        if (deposit.Amount <= 0)
        {
            errors.Add(AccountCustomerValidationException.DepositMustBeGreaterThanZero());
        }

        if (errors.Any())
        {
            throw new AccountCustomerValidationException(this, deposit, errors);
        }
    }

    private void Validate(Withdrawal withdrawal)
    {
        List<string> errors = [];
        if (withdrawal is not { })
        {
            errors.Add(AccountCustomerValidationException.WithdrawalMustExist());
            throw new AccountCustomerValidationException(this, withdrawal, errors);
        }

        if (!withdrawal.CustomerId.Equals(CustomerId))
        {
            errors.Add(AccountCustomerValidationException.WithdrawalCustomerMustMatch());
        }

        if (!withdrawal.AccountId.Equals(AccountId))
        {
            errors.Add(AccountCustomerValidationException.WithdrawalAccountMustMatch());
        }

        if (withdrawal.Amount <= 0)
        {
            errors.Add(AccountCustomerValidationException.WithdrawalMustBeGreaterThanZero());
        }

        if (Balance - withdrawal.Amount < 0)
        {
            errors.Add(AccountCustomerValidationException.WithdrawalEndBalanceMustBeGreaterEqualToZero());
        }

        if (errors.Any())
        {
            throw new AccountCustomerValidationException(this, withdrawal, errors);
        }
    }

    private void Validate(AccountClose closeAccount)
    {
        List<string> errors = [];
        if (closeAccount is not { })
        {
            errors.Add(AccountCustomerValidationException.CloseAccountMustExist());
            throw new AccountCustomerValidationException(this, closeAccount, errors);
        }

        if (!closeAccount.CustomerId.Equals(CustomerId))
        {
            errors.Add(AccountCustomerValidationException.DepositCustomerMustMatch());
        }

        if (Balance != 0)
        {
            errors.Add(AccountCustomerValidationException.CloseAccountBalanceMustBeZero());
        }

        if (errors.Any())
        {
            throw new AccountCustomerValidationException(this, closeAccount, errors);
        }
    }
}
