namespace Bank.Domain.Models;

/// <summary>
/// A customer of the bank.
/// </summary>
/// <param name="CustomerId">Id of customer.</param>
/// <param name="FirstName">First name.</param>
/// <param name="LastName">Last name</param>
public class Customer(int CustomerId, string FirstName, string LastName)
{
    /// <summary>
    /// Id of customer.
    /// </summary>
    public int CustomerId { get; } = CustomerId;

    /// <summary>
    /// First name.
    /// </summary>
    public string FirstName { get; } = FirstName;

    /// <summary>
    /// Last name.
    /// </summary>
    public string LastName { get; } = LastName;
}
