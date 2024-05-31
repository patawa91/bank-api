namespace Bank.Infrastructure.Models;

public enum TransactionType
{
    Unknown = 0,
    AccountCreation = 1,
    AccountClose = 2,
    Deposit = 3,
    Withdrawal = 4
}
