using Bank.Domain.Models;

namespace Bank.Domain.Exceptions;

/// <summary>
/// Exception thrown when an error occurs while performing an action on an account initiated by a customer.
/// </summary>
public sealed class AccountCustomerValidationException : Exception
{
    /// <summary>
    /// The action that caused the exception.
    /// </summary>
    public AccountCustomerAction? Action { get; private set; } = null;

    /// <summary>
    /// The account associated with the exception.
    /// </summary>
    public Account? Account { get;}

    private readonly IList<string> _errorMessages;


    /// <summary>
    /// Error messags generated during validation.
    /// </summary>
    public IReadOnlyCollection<string> Errors => _errorMessages.AsReadOnly();

    /// <summary>
    /// Exception thrown when an error occurs while performing an action on an account initiated by a customer.
    /// </summary>
    public AccountCustomerValidationException(Account? account = null, AccountCustomerAction? action = null, IEnumerable<string>? errorMessages = null) : base($"Error(s) happened while validating an account customer action. Check errors collection and properties on this exception.")
    {
        Action = action;
        Account = account;
        _errorMessages = errorMessages?.ToList() ?? [];
    }

    /// <summary>
    /// Exception thrown when an error occurs while performing an action on an account initiated by a customer.
    /// </summary>
    public AccountCustomerValidationException(string message, Exception innerException, Account? account = null, AccountCustomerAction? action = null, IEnumerable<string>? errorMessages = null) : base(message, innerException)
    {
        Action = action;
        Account = account;
        _errorMessages = errorMessages?.ToList() ?? [];
    }

    /// <summary>
    /// Message when deposit doesn't exist.
    /// </summary>
    public static string DepositMustExist() => "Deposit must exist.";

    /// <summary>
    /// Message when deposit is not greater than zero.
    /// </summary>
    public static string DepositMustBeGreaterThanZero() => "Deposit must be greater than zero.";

    /// <summary>
    /// Message when the deposit customer is not the account customer.
    /// </summary>
    public static string DepositCustomerMustMatch() => "Deposit customer must match account customer.";

    /// <summary>
    /// Message when the deposit account is not the account.
    /// </summary>
    public static string DepositAccountMustMatch() => "Deposit account must match account.";

    /// <summary>
    /// Message when deposit, account obj must exist.
    /// </summary>
    public static string DepositAccountMustExist() => "When doing a deposit the account object must exist.";

    /// <summary>
    /// Message when close account obj doesn't exist.
    /// </summary>
    public static string CloseAccountMustExist() => "Close account object must exist.";

    /// <summary>
    /// Message when close account doesn't have zero balance.
    /// </summary>
    public static string CloseAccountBalanceMustBeZero() => "To close an account the balance must be zero for the account.";

    /// <summary>
    /// Message when withdrawal doesn't exist.
    /// </summary>
    public static string WithdrawalMustExist() => "Withdrawal must exist.";

    /// <summary>
    /// Message when withdrawal is not greater than zero.
    /// </summary>
    public static string WithdrawalMustBeGreaterThanZero() => "Deposit must be greater than zero.";

    /// <summary>
    /// Message when withdrawal makes balance less than zero.
    /// </summary>
    public static string WithdrawalEndBalanceMustBeGreaterEqualToZero() => "Withdrawal will make balance in account less than zero.";

    /// <summary>
    /// Message when the withdrawal customer is not the account customer.
    /// </summary>
    public static string WithdrawalCustomerMustMatch() => "Withdrawal customer must match account customer.";

    /// <summary>
    /// Message when the withdrawal account is not the account.
    /// </summary>
    public static string WithdrawalAccountMustMatch() => "Withdrawal account must match account.";

    /// <summary>
    /// Message when withdrawel, account obj must exist.
    /// </summary>
    public static string WithdrawalAccountMustExist() => "When doing a withdrawal the account object must exist.";

    /// <summary>
    /// Message when the customer does not exist.
    /// </summary>
    public static string CreateAccountCustomerMustExist() => "Customer must exist when creating account.";

    /// <summary>
    /// Message when the customer initial balance is too low.
    /// </summary>
    public static string CreateAccountMustHaveInitialAmount(int minimumBalance) => $"Customer must provide at least ${minimumBalance} to create account.";

    /// <summary>
    /// Message when the account type is not valid.
    /// </summary>
    public static string CreateAccountMustBeValidTypes() => "Account must be of Savings or Checking to set up a new account.";

    /// <summary>
    /// Message when first account and not savings.
    /// </summary>
    public static string CreateFirstAccountMustBeSavings() => "Account must be of Savings for first account.";


}
