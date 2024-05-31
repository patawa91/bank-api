namespace Bank.Infrastructure.Models;

public enum AccountStatus
{
    Unknown = 0,
    /// <summary>
    /// The account is open.
    /// </summary>
    Open = 1,

    /// <summary>
    /// The account is closed.
    /// </summary>
    Closed = 2
}
