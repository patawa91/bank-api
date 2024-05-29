namespace Bank.Contracts;

public sealed record Deposit : Transaction
{
    public Deposit(int customerId, int accountId, decimal amount) : base(customerId, accountId, amount)
    {
    }
}