using AutoMapper;
using Bank.Domain.Models;
using Bank.Domain.Repositories;
using Bank.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Principal;

namespace Bank.Infrastructure.Repositories;

/// <inheritdoc cref="IAccountRepository"/>
public sealed class AccountRepository(BankDbContext context, IMapper mapper) : IRepository<Domain.Models.Account, Domain.Models.AccountSearch>
{
    private readonly BankDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    /// <inheritdoc cref="IAccountRepository.GetById(int)"/>
    public async Task<Domain.Models.Account> GetByIdAsync(int id)
    {
        return _mapper.Map<Models.Account?, Domain.Models.Account>(await _context.Accounts.FindAsync(id));
    }

    /// <inheritdoc cref="IRepository{T}.GetAllAsync()"/>
    public Task<IEnumerable<Domain.Models.Account>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc cref="IRepository{T}.AddAsync(T)"/>
    public async Task<Domain.Models.Account> AddAsync(Domain.Models.Account entity)
    {
        var accountModel = _mapper.Map<Models.Account>(entity);

        await _context.Accounts.AddAsync(accountModel);

        // Add transactions
        await AddTransactions(entity);

        await _context.SaveChangesAsync();

        return _mapper.Map<Domain.Models.Account>(accountModel);
    }

    /// <inheritdoc cref="IRepository{T}.UpdateAsync(T)"/>
    public async Task<Domain.Models.Account> UpdateAsync(Domain.Models.Account entity)
    {
        var existingAccountModel = await _context.Accounts.FindAsync(entity.AccountId);

        var accountModel = _mapper.Map<Models.Account>(entity);

        if (existingAccountModel is { })
        {
            // update
            _context.Entry(existingAccountModel).CurrentValues.SetValues(accountModel);
        }
        else
        {
            throw new Exception("Account not found");
        }

        // Add transactions
        await AddTransactions(entity);

        await _context.SaveChangesAsync();

        return _mapper.Map<Domain.Models.Account>(accountModel);
    }

    /// <inheritdoc cref="IRepository{T}.RemoveAsync(int)"/>
    public Task<Domain.Models.Account> RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc cref="IRepository{T}.Find(Expression{Func{T, bool}})"/>
    public async Task<IEnumerable<Domain.Models.Account>> Find(Domain.Models.AccountSearch searchCriteria)
    {
        // Build the predicate for the database models
        var param = Expression.Parameter(typeof(Models.Account), "a");
        Expression? expr = null;

        if (searchCriteria.CustomerId.HasValue)
        {
            var left = Expression.Property(param, nameof(Models.Account.CustomerId));
            var right = Expression.Constant(searchCriteria.CustomerId.Value);
            expr = Expression.Equal(left, right);
        }

        if (searchCriteria.AccountStatus.HasValue)
        {
            var left = Expression.Property(param, nameof(Models.Account.AccountStatus));
            var right = Expression.Constant(_mapper.Map<Models.AccountStatus>(searchCriteria.AccountStatus.Value));
            expr = expr is null ? Expression.Equal(left, right) : Expression.AndAlso(expr, Expression.Equal(left, right));
        }

        if (expr is null)
        {
            return [];
        }

        var lambda = Expression.Lambda<Func<Models.Account, bool>>(expr, param);

        // Use the built predicate to filter the database models
        var accountModels = await _context.Accounts
            .Where(lambda)
            .ToListAsync();

        return _mapper.Map<IEnumerable<Domain.Models.Account>>(accountModels);
    }

    private async Task AddTransactions(Domain.Models.Account account)
    {
        // Add transactions
        var transactionModels = account.CompletedCustomerActions
            .Select(ca => new Models.Transaction(default, ca.AccountId, ca.Amount, GetTransactionType(ca), DateTime.UtcNow));

        if (transactionModels.Any())
        {
            await _context.Transactions.AddRangeAsync(transactionModels);
        }
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
}
