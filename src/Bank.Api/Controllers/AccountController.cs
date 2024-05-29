using Bank.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Api.Controllers;

[ApiController]
[Route("[controller]")]
/// <summary>
/// Handles requests for all account operations.
/// </summary>
public class AccountController : ControllerBase
{
    [HttpPost("{accountId}/deposit")]
    public async Task<IActionResult> Deposit(int accountId, [FromBody] Deposit accountCreate)
    {
        return Ok(new TransactionResult(accountCreate.CustomerId,accountId,accountCreate.Amount, true));
    }
}
