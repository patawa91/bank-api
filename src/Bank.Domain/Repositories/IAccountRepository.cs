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
    Account Save(Account account);

    /// <summary>
    /// Get an account by id.
    /// </summary>
    /// <param name="id">Id of the account.</param>
    /// <returns>The account</returns>
    Account GetById(int id);
}
