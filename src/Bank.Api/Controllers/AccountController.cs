using Bank.Application.Services;
using Bank.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Api.Controllers;

[ApiController]
[Route("v1/[controller]")]
/// <summary>
/// Handles requests for all account operations.
/// </summary>
public class AccountController(ICustomerActionService CustomerActionService) : ControllerBase
{
    private readonly ICustomerActionService _customerActionService = CustomerActionService;

    [HttpPost("")]
    public async Task<IActionResult> AccountCreate([FromBody] AccountCreate accountCreate)
    {
        return Ok(await _customerActionService.CreateAccountAsync(accountCreate));
    }

    [HttpPut("{accountId}/close")]
    public async Task<IActionResult> AccountClose(int accountId, [FromBody] AccountClose accountClose)
    {
        return Ok(await _customerActionService.CloseAccountAsync(accountId, accountClose));
    }

    [HttpPost("{accountId}/deposit")]
    public async Task<IActionResult> Deposit(int accountId, [FromBody] Deposit deposit)
    {
        return Ok(await _customerActionService.DepositAsync(accountId, deposit));
    }

    [HttpPost("{accountId}/withdrawal")]
    public async Task<IActionResult> Withdrawal(int accountId, [FromBody] Withdrawal withdrawal)
    {
        return Ok(await _customerActionService.WithdrawAsync(accountId, withdrawal));
    }


}
