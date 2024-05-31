using Bank.Domain.Models;

namespace Bank.Domain.Repositories;

/// <summary>
/// Read/Write account data from a data store.
/// </summary>
public interface IAccountRepository
{
    /// <summary>
    /// Save an account.
    /// </summary>
    /// <param name="account">Account to save.</param>
    Task<Account> SaveAsync(Account account);

    /// <summary>
    /// Get an account by id.
    /// </summary>
    /// <param name="id">Id of the account.</param>
    /// <returns>The account</returns>
    Task<Account> GetByIdAsync(int id);

    /// <summary>
    /// Get count by customer id.
    /// </summary>
    /// <param name="id">Id of customer.</param>
    /// <returns>Number of account</returns>
    Task<int> GetOpenCountByCustomerIdAsync(int id);
}
