using Bank.Domain.Exceptions;
using Bank.Domain.Models;
using Bank.Domain.Repositories;

namespace Bank.Domain.Services;

/// <inheritdoc cref="IAccountCustomerService"/>
public sealed class AccountCustomerService(IAccountRepository AccountRepository, ICustomerRepository CustomerRepository) : IAccountCustomerService
{
    private readonly IAccountRepository _accountRepository = AccountRepository;
    private readonly ICustomerRepository _customerRepository = CustomerRepository;

    /// <inheritdoc cref="IAccountCustomerService.CloseAccount(Domain.Models.AccountClose)"/>
    public Account CloseAccount(AccountClose accountClose)
    {
        // get the account
        var account = _accountRepository.GetById(accountClose.AccountId);

        // make sure the account exists
        if(account is not { })
        {
            throw new AccountCustomerValidationException(null, accountClose, [AccountCustomerValidationException.CloseAccountMustExist()]);
        }

        // close account
        account.Close(accountClose);
        
        // save account
        account = _accountRepository.Save(account);
        return account; 
    }

    /// <inheritdoc cref="IAccountCustomerService.CreateAccount(int, decimal, AccountType)"/>
    public Account CreateAccount(int customerId, decimal amount, AccountType accountType)
    {
        // get customer from customer repository and make sure it exists
        var customer = _customerRepository.GetById(customerId);

        // make sure the customer exists
        if (customer is not { })
        {
            throw new AccountCustomerValidationException(null, null, [AccountCustomerValidationException.CreateAccountCustomerMustExist()]);
        }

        // validation
        if (amount < 100)
        {
            throw new AccountCustomerValidationException(null, null, [AccountCustomerValidationException.CreateAccountMustHaveInitialAmount(100)]);
        }

        if(accountType is not AccountType.Checking and not AccountType.Savings)
        {
            throw new AccountCustomerValidationException(null, null, [AccountCustomerValidationException.CreateAccountMustBeValidTypes()]);
        }

        // create new account
        var account = new Account(null, customerId, amount, accountType, AccountStatus.Open);

        // save it to the account repository
        account = _accountRepository.Save(account);

        return account;
    }

    /// <inheritdoc cref="IAccountCustomerService.Deposit(Domain.Models.Deposit)"/>
    public Account Deposit(Deposit deposit)
    {
        // get the account
        var account = _accountRepository.GetById(deposit.AccountId);

        // make sure the account exists
        if (account is not { })
        {
            throw new AccountCustomerValidationException(null, deposit, [AccountCustomerValidationException.DepositAccountMustExist()]);
        }

        // deposit
        account.Deposit(deposit);

        // save account
        account = _accountRepository.Save(account);

        return account;
    }

    /// <inheritdoc cref="IAccountCustomerService.Withdraw(Withdrawal)"/>
    public Account Withdraw(Withdrawal withdrawal)
    {
        // get the account
        var account = _accountRepository.GetById(withdrawal.AccountId);

        // make sure the account exists
        if (account is not { })
        {
            throw new AccountCustomerValidationException(null, withdrawal, [AccountCustomerValidationException.WithdrawalAccountMustExist()]);
        }

        // withdraw
        account.Withdraw(withdrawal);

        // save account
        account = _accountRepository.Save(account);

        return account;
    }
}
