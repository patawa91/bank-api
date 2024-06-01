using Bank.Domain.Exceptions;
using Bank.Domain.Models;
using Bank.Domain.Repositories;

namespace Bank.Domain.Services;

/// <inheritdoc cref="IAccountCustomerService"/>
public sealed class AccountCustomerService(IRepository<Bank.Domain.Models.Account, Bank.Domain.Models.AccountSearch> AccountRepository, IRepository<Bank.Domain.Models.Customer, Bank.Domain.Models.CustomerSearch> CustomerRepository) : IAccountCustomerService
{
    private readonly IRepository<Bank.Domain.Models.Account, Bank.Domain.Models.AccountSearch> _accountRepository = AccountRepository;
    private readonly IRepository<Bank.Domain.Models.Customer, Bank.Domain.Models.CustomerSearch> _customerRepository = CustomerRepository;
    private readonly decimal _minimumInitialCreationAmount = 100;

    /// <inheritdoc cref="IAccountCustomerService.CloseAccount(Domain.Models.AccountClose)"/>
    public async Task<Account> CloseAccountAsync(AccountClose accountClose)
    {
        // get the account
        var account = await _accountRepository.GetByIdAsync(accountClose.AccountId);

        // make sure the account exists
        if(account is not { })
        {
            throw new AccountCustomerValidationException(account, accountClose, [AccountCustomerValidationException.CloseAccountMustExist()]);
        }

        // close account
        account.Close(accountClose);
        
        // save account
        account = await _accountRepository.UpdateAsync(account);
        return account; 
    }

    /// <inheritdoc cref="IAccountCustomerService.CreateAccount(int, decimal, AccountType)"/>
    public async Task<Account> CreateAccountAsync(int customerId, decimal amount, AccountType accountType)
    {
        // get customer from customer repository and make sure it exists
        var customer = await _customerRepository.GetByIdAsync(customerId);

        // make sure the customer exists
        if (customer is not { })
        {
            throw new AccountCustomerValidationException(null, null, [AccountCustomerValidationException.CreateAccountCustomerMustExist()]);
        }

        // validation
        if (amount < _minimumInitialCreationAmount)
        {
            throw new AccountCustomerValidationException(null, null, [AccountCustomerValidationException.CreateAccountMustHaveInitialAmount(100)]);
        }

        if(accountType is not AccountType.Checking and not AccountType.Savings)
        {
            throw new AccountCustomerValidationException(null, null, [AccountCustomerValidationException.CreateAccountMustBeValidTypes()]);
        }

        var accountCount = (await _accountRepository.Find(new AccountSearch { CustomerId = customerId, AccountStatus = AccountStatus.Open})).Count();

        if( accountCount == 0 && accountType is not AccountType.Savings)
        {
            throw new AccountCustomerValidationException(null, null, [AccountCustomerValidationException.CreateFirstAccountMustBeSavings()]);
        }

        // create new account
        var account = new Account(null, customerId, amount, accountType, AccountStatus.Open);

        // save it to the account repository
        account = await _accountRepository.AddAsync(account);

        return account;
    }

    /// <inheritdoc cref="IAccountCustomerService.Deposit(Domain.Models.Deposit)"/>
    public async Task<Account> DepositAsync(Deposit deposit)
    {
        // get the account
        var account = await _accountRepository.GetByIdAsync(deposit.AccountId);

        // make sure the account exists
        if (account is not { })
        {
            throw new AccountCustomerValidationException(account, deposit, [AccountCustomerValidationException.DepositAccountMustExist()]);
        }

        // deposit
        account.Deposit(deposit);

        // save account
        account = await _accountRepository.UpdateAsync(account);

        return account;
    }

    /// <inheritdoc cref="IAccountCustomerService.Withdraw(Withdrawal)"/>
    public async Task<Account> WithdrawAsync(Withdrawal withdrawal)
    {
        // get the account
        var account = await _accountRepository.GetByIdAsync(withdrawal.AccountId);

        // make sure the account exists
        if (account is not { })
        {
            throw new AccountCustomerValidationException(account, withdrawal, [AccountCustomerValidationException.WithdrawalAccountMustExist()]);
        }

        // withdraw
        account.Withdraw(withdrawal);

        // save account
        account = await _accountRepository.UpdateAsync(account);

        return account;
    }
}
