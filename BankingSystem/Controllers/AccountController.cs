using BankingSystem.Application.Commands;
using BankingSystem.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly ILogger<AccountController> _logger;
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator, ILogger<AccountController> logger)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [Route("getAll")]
    public async Task<IEnumerable<Account>> Get()
    {
        var response = await _mediator.Send(new GetAllAccountsQuery()); 
        return response;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAccountCommand command)
    {
        var result = await _mediator.Send(command); 
        return Ok(result);
    }

    [HttpDelete]
    public void Delete()
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    [Route("Deposit")]
    public async Task<IActionResult> Deposit([FromBody] DepositAccountCommand command)
    {
        var result = await _mediator.Send(command); 
        return result == true ? Ok() : BadRequest();
    }

    [HttpPost]
    [Route("Withdraw")]
    public async Task<IActionResult> Withdraw([FromBody] WithdrawAccountCommand command)
    {
        var result = await _mediator.Send(command); 
        return result == true ? Ok() : BadRequest();
    }
}
