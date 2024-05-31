using AutoMapper;
using Bank.Application.Exceptions;
using Bank.Application.Models;
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
    public async Task<AccountCloseResult> CloseAccountAsync(int accountId, Contracts.AccountClose accountClose)
    {
        try
        {
            // simple validate
            if (accountClose is not { }) 
            {
                throw new ArgumentException("The account close object cannot be null.", nameof(accountClose));
            }

            if (accountClose.AccountId != accountId)
            {
                throw new ArgumentException("The accountId of payload does not match accountId of url.", nameof(accountClose));
            }

            // map in
            var accountCloseDomain = _mapper.Map<Contracts.AccountClose, Domain.Models.AccountClose>(accountClose);

            // close account
            await _accountCustomerService.CloseAccountAsync(accountCloseDomain);

            // map out
            return _mapper.Map<Domain.Models.AccountClose, AccountCloseResult>(accountCloseDomain);
        }
        catch (AccountCustomerValidationException ve)
        {
            throw CustomerActionInputException.NewWithErrors(Map(ve));
        }
        catch (Exception ex)
        {
            throw CustomerActionException.NewGeneralErrorWithException(ex);
        }
    }

    /// <inheritdoc cref="ICustomerActionService.CreateAccount(AccountCreate)"/>
    public async Task<AccountCreatedResult> CreateAccountAsync(AccountCreate accountCreate)
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
            var domainAccount = await _accountCustomerService.CreateAccountAsync(accountCreate.CustomerId, accountCreate.InitialDeposit, domainAccountType);

            // map out
            return _mapper.Map<Domain.Models.Account, AccountCreatedResult>(domainAccount);
        }
        catch (AccountCustomerValidationException ve)
        {
            throw CustomerActionInputException.NewWithErrors(Map(ve));
        }
        catch (Exception ex)
        {
            throw CustomerActionException.NewGeneralErrorWithException(ex);
        }
    }

    /// <inheritdoc cref="ICustomerActionService.Deposit(Contracts.Deposit)"/>
    public async Task<DepositResult> DepositAsync(int accountId, Contracts.Deposit deposit)
    {
        try
        {
            // simple validate
            if (deposit is not { })
            {
                throw new ArgumentException("The deposit object cannot be null.", nameof(deposit));
            }

            if (deposit.AccountId != accountId)
            {
                throw new ArgumentException("The accountId of payload does not match accountId of url.", nameof(deposit));
            }

            // map in
            var domainDeposit = _mapper.Map<Contracts.Deposit, Domain.Models.Deposit>(deposit);

            // create account
            var domainAccount = await _accountCustomerService.DepositAsync(domainDeposit);

            // map out
            return _mapper.Map<Domain.Models.Account, DepositResult>(domainAccount);
        }
        catch (AccountCustomerValidationException ve)
        {
            throw CustomerActionInputException.NewWithErrors(Map(ve));
        }
        catch (Exception ex)
        {
            throw CustomerActionException.NewGeneralErrorWithException(ex);
        }
    }

    /// <inheritdoc cref="ICustomerActionService.Withdraw(Withdrawal)"/>
    public async Task<WithdrawalResult> WithdrawAsync(int accountId, Contracts.Withdrawal withdrawal)
    {
        try
        {
            // simple validate
            if (withdrawal is not { })
            {
                throw new ArgumentException("The withdrawal object cannot be null.", nameof(withdrawal));
            }

            if (withdrawal.AccountId != accountId)
            {
                throw new ArgumentException("The accountId of payload does not match accountId of url.", nameof(withdrawal));
            }

            // map in
            var domainWithdrawal = _mapper.Map<Contracts.Withdrawal, Domain.Models.Withdrawal>(withdrawal);

            // create account
            var domainAccount = await _accountCustomerService.WithdrawAsync(domainWithdrawal);

            // map out
            return _mapper.Map<Domain.Models.Account, WithdrawalResult>(domainAccount);
        }
        catch (AccountCustomerValidationException ve)
        {
            throw CustomerActionInputException.NewWithErrors(Map(ve));
        }
        catch (Exception ex)
        {
            throw CustomerActionException.NewGeneralErrorWithException(ex);
        }
    }

    private InputError Map(AccountCustomerValidationException ve)
    {
        var accountInfo = ve.Account is { } ?  _mapper.Map<Domain.Models.Account, InputErrorAccountInfo>(ve.Account) : null;
        //var actionInfo = ve.Action is { } ? _mapper.Map<Domain.Models.AccountCustomerAction, InputErrorActionInfo>(ve.Action) : null;
        var actionInfo = ve.Action is { } ? _mapper.Map(ve.Action, ve.Action.GetType(), typeof(InputErrorActionInfo)) as InputErrorActionInfo : null;
        return new InputError(accountInfo!, actionInfo!, ve.Errors);
    }
}
