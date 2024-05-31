using AutoMapper;
using Bank.Domain.Models;
using Bank.Domain.Repositories;
using Bank.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Bank.Infrastructure.Repositories;

/// <inheritdoc cref="IAccountRepository"/>
public sealed class AccountRepository(BankDbContext context, IMapper mapper) : IAccountRepository
{
    private readonly BankDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    /// <inheritdoc cref="IAccountRepository.GetById(int)"/>
    public async Task<Domain.Models.Account> GetByIdAsync(int id)
    {
        return _mapper.Map<Models.Account?, Domain.Models.Account>(await _context.Accounts.FindAsync(id));
    }

    /// <inheritdoc cref="IAccountRepository.Save(Account)"/>
    public async Task<Domain.Models.Account> SaveAsync(Domain.Models.Account account)
    {
        var existingAccountModel = await _context.Accounts.FindAsync(account.AccountId);

        var accountModel = _mapper.Map<Models.Account>(account);

        if (existingAccountModel is { })
        {
            // update
            _context.Entry(existingAccountModel).CurrentValues.SetValues(accountModel);
        }
        else
        {
            // insert
            
            await _context.Accounts.AddAsync(accountModel);
        }

        // Add transactions
        var transactionModels = account.CompletedCustomerActions
            .Select(ca => new Models.Transaction(default, ca.AccountId, ca.Amount, GetTransactionType(ca), DateTime.UtcNow));

        if (transactionModels.Any())
        {
            await _context.Transactions.AddRangeAsync(transactionModels);
        }

        await _context.SaveChangesAsync();

        return _mapper.Map<Domain.Models.Account>(accountModel);
    }

    private static TransactionType GetTransactionType(AccountCustomerAction action) 
    { 
        return action switch 
        { 
            Deposit => TransactionType.Deposit, 
            Withdrawal => TransactionType.Withdrawal, 
            AccountClose => TransactionType.AccountClose,
            _ => throw new InvalidOperationException("Invalid action type") 
        };
    }

    public async Task<int> GetOpenCountByCustomerIdAsync(int id)
    {
        return await _context.Accounts.CountAsync(a => a.CustomerId == id && a.AccountStatus == Models.AccountStatus.Open);
    }
}
