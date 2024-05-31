namespace Bank.Application.Models;

public sealed record InputError(InputErrorAccountInfo AccountInfo, InputErrorActionInfo ActionInfo, IEnumerable<string> Errors)
{
}

public sealed record InputErrorAccountInfo(int? AccountId, int CustomerId, decimal Balance, string AccountType, string AccountStatus)
{

}

public sealed record InputErrorActionInfo(int AccountId, int CustomerId, decimal Amount)
{

}
