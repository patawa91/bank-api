namespace Bank.Domain.Models;

/// <summary>
/// Criteria for searching accounts.
/// </summary>
public class AccountSearch
{
    /// <summary>
    /// Id of the customer.
    /// </summary>
    public int? CustomerId { get; set; } = null;

    /// <summary>
    /// Account status. e.g. Open, Closed.
    /// </summary>
    public AccountStatus? AccountStatus { get; set; } = null;
}
