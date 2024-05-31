using Bank.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Api.Controllers;

[ApiController]
[Route("v1/[controller]")]
/// <summary>
/// Handles requests for all account operations.
/// </summary>
public class AccountController : ControllerBase
{
    [HttpPost("")]
    public async Task<IActionResult> AccountCreate([FromBody] AccountCreate accountCreate)
    {
        return Ok(new AccountCreatedResult(accountCreate.CustomerId, default, accountCreate.AccountTypeId, accountCreate.InitialDeposit, true));
    }

    [HttpPost("{accountId}/close")]
    public async Task<IActionResult> AccountClose(int accountId, [FromBody] AccountClose accountClose)
    {
        return Ok(new AccountCloseResult(accountClose.CustomerId, accountId, true));
    }

    [HttpPost("{accountId}/deposit")]
    public async Task<IActionResult> Deposit(int accountId, [FromBody] Deposit deposit)
    {
        return Ok(new DepositResult(deposit.CustomerId,accountId, deposit.Amount, true));
    }

    [HttpPost("{accountId}/withdrawal")]
    public async Task<IActionResult> Withdrawal(int accountId, [FromBody] Withdrawal withdrawal)
    {
        return Ok(new WithdrawalResult(withdrawal.CustomerId, accountId, withdrawal.Amount, true));
    }


}
