namespace Bank.Application.Exceptions;

/// <summary>
/// Base exception for customer actions.
/// </summary>
public class CustomerActionInputException : CustomerActionException
{
    private readonly IList<string> _errorMessages;


    /// <summary>
    /// Error messags generated during validation.
    /// </summary>
    public IReadOnlyCollection<string> Errors => _errorMessages.AsReadOnly();

    /// <summary>
    /// Base exception for customer actions.
    /// </summary>
    /// <param name="message">The exception message.</param>
    public CustomerActionInputException(string message, IEnumerable<string>? errorMessages = null) : base(message)
    {
        _errorMessages = errorMessages?.ToList() ?? [];
    }

    /// <summary>
    /// Base exception for customer actions.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">The inner exception.</param>
    public CustomerActionInputException(string message, Exception innerException, IEnumerable<string>? errorMessages = null) : base(message, innerException)
    {
        _errorMessages = errorMessages?.ToList() ?? [];
    }
}