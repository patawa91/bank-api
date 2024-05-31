namespace Bank.Application.Exceptions;

/// <summary>
/// Base exception for customer actions.
/// </summary>
public class CustomerActionException : Exception
{
    /// <summary>
    /// Base exception for customer actions.
    /// </summary>
    /// <param name="message">The exception message.</param>
    public CustomerActionException(string message) : base(message)
    {
    }

    /// <summary>
    /// Base exception for customer actions.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">The inner exception.</param>
    public CustomerActionException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public static CustomerActionException NewGeneralErrorWithException(Exception ex) => new("There was an error for a customer action.", ex);
}
