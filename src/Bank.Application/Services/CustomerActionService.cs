using AutoMapper;
using Bank.Application.Exceptions;
using Bank.Contracts;
using Bank.Domain.Exceptions;
using Bank.Domain.Models;
using Bank.Domain.Services;

namespace Bank.Application.Services;

/// <inheritdoc cref="ICustomerActionService"/>
public sealed class CustomerActionService(IAccountCustomerService AccountCustomerService, IMapper Mapper) : ICustomerActionService
{
    private readonly IAccountCustomerService _accountCustomerService = AccountCustomerService;
    private readonly IMapper _mapper = Mapper;

    /// <inheritdoc cref="ICustomerActionService.AccountCloseResult(Contracts.AccountClose)"/>
    public AccountCloseResult AccountCloseResult(Contracts.AccountClose accountClose)
    {
        try
        {
            // simple validate
            if (accountClose is not { }) 
            {
                throw new ArgumentException("The account close object cannot be null.", nameof(accountClose));
            }

            // map in
            var accountCloseDomain = _mapper.Map<Contracts.AccountClose, Domain.Models.AccountClose>(accountClose);

            // close account
            _accountCustomerService.CloseAccount(accountCloseDomain);

            // map out
            return _mapper.Map<Domain.Models.AccountClose, AccountCloseResult>(accountCloseDomain);
        }
        catch (AccountCustomerValidationException ve)
        {
            throw new CustomerActionInputException("There were input errors for a customer action.", ve.Errors);
        }
        catch (Exception ex)
        {
            throw new CustomerActionInputException("There was an error for a customer action.", ex);
        }
    }

    /// <inheritdoc cref="ICustomerActionService.CreateAccount(AccountCreate)"/>
    public AccountCreatedResult CreateAccount(AccountCreate accountCreate)
    {
        try
        {
            // simple validate
            if (accountCreate is not { })
            {
                throw new ArgumentException("The account create object cannot be null.", nameof(accountCreate));
            }

            // map in
            var domainAccountType = _mapper.Map<Contracts.AccountType, Domain.Models.AccountType>(accountCreate.AccountTypeId);

            // create account
            var domainAccount = _accountCustomerService.CreateAccount(accountCreate.CustomerId, accountCreate.InitialDeposit, domainAccountType);

            // map out
            return _mapper.Map<Domain.Models.Account, AccountCreatedResult>(domainAccount);
        }
        catch (AccountCustomerValidationException ve)
        {
            throw new CustomerActionInputException("There were input errors for a customer action.", ve.Errors);
        }
        catch (Exception ex)
        {
            throw new CustomerActionInputException("There was an error for a customer action.", ex);
        }
    }

    /// <inheritdoc cref="ICustomerActionService.Deposit(Contracts.Deposit)"/>
    public DepositResult Deposit(Contracts.Deposit deposit)
    {
        try
        {
            // simple validate
            if (deposit is not { })
            {
                throw new ArgumentException("The deposit object cannot be null.", nameof(deposit));
            }

            // map in
            var domainDeposit = _mapper.Map<Contracts.Deposit, Domain.Models.Deposit>(deposit);

            // create account
            var domainAccount = _accountCustomerService.Deposit(domainDeposit);

            // map out
            return _mapper.Map<Domain.Models.Account, DepositResult>(domainAccount);
        }
        catch (AccountCustomerValidationException ve)
        {
            throw new CustomerActionInputException("There were input errors for a customer action.", ve.Errors);
        }
        catch (Exception ex)
        {
            throw new CustomerActionInputException("There was an error for a customer action.", ex);
        }
    }

    /// <inheritdoc cref="ICustomerActionService.Withdraw(Withdrawal)"/>
    public WithdrawalResult Withdraw(Contracts.Withdrawal withdrawal)
    {
        try
        {
            // simple validate
            if (withdrawal is not { })
            {
                throw new ArgumentException("The withdrawal object cannot be null.", nameof(withdrawal));
            }

            // map in
            var domainWithdrawal = _mapper.Map<Contracts.Withdrawal, Domain.Models.Withdrawal>(withdrawal);

            // create account
            var domainAccount = _accountCustomerService.Withdraw(domainWithdrawal);

            // map out
            return _mapper.Map<Domain.Models.Account, WithdrawalResult>(domainAccount);
        }
        catch (AccountCustomerValidationException ve)
        {
            throw new CustomerActionInputException("There were input errors for a customer action.", ve.Errors);
        }
        catch (Exception ex)
        {
            throw new CustomerActionInputException("There was an error for a customer action.", ex);
        }
    }
}
