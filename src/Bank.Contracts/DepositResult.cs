﻿namespace Bank.Contracts;

/// <summary>
/// Result of a deposit.
/// </summary>
/// <param name="CustomerId">The customer id.</param>
/// <param name="AccountId">The account id.</param>
/// <param name="Balance">The balance after the deposit.</param>
/// <param name="Succeeded">Did this operation succeed or not.</param>
public sealed record class DepositResult(int CustomerId, int AccountId, decimal Balance, bool Succeeded)
{
}
