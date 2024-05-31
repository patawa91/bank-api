using Bank.Domain.Models;

namespace Bank.Domain.Repositories;

/// <summary>
/// Read/Write customer data from a data store.
/// </summary>
public interface ICustomerRepository
{
    /// <summary>
    /// Get a customer by their unique identifier.
    /// </summary>
    /// <param name="customerId">Id of customer.</param>
    /// <returns>The customer.</returns>
    Customer GetById(int customerId);
}
