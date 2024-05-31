namespace Bank.Infrastructure.Models;

public sealed record class Transaction(int TransactionId, int AccountId, decimal Amount, TransactionType TransactionType, DateTime TransactionDate)
{
}
