namespace Bank.Contracts;

public sealed record Withdrawal : Transaction
{
    public Withdrawal(int customerId, int accountId, decimal amount) : base(customerId, accountId, amount)
    {
    }
}
