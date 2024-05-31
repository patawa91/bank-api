using Bank.Application.Models;

namespace Bank.Application.Exceptions;

/// <summary>
/// Base exception for customer actions.
/// </summary>
public class CustomerActionInputException : CustomerActionException
{
    public InputError? InputError { get; private set; }

    /// <summary>
    /// Base exception for customer actions.
    /// </summary>
    /// <param name="message">The exception message.</param>
    public CustomerActionInputException(string message, InputError? inputError = null) : base(message)
    {
        InputError = inputError;
    }

    /// <summary>
    /// Base exception for customer actions.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">The inner exception.</param>
    public CustomerActionInputException(string message, Exception innerException, InputError? inputError = null) : base(message, innerException)
    {
        InputError = inputError;
    }

    public static CustomerActionInputException NewWithErrors(InputError? inputError = null) => new("There were input errors for a customer action.", inputError);
    
}