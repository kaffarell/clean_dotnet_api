using BankingSystem.Application.Commands;
using BankingSystem.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace BankingSystem.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IMediator _mediator;

    public UserController(IMediator mediator, ILogger<UserController> logger)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [Route("getAll")]
    public async Task<IEnumerable<User>> GetAsync()
    {
        var response = await _mediator.Send(new GetAllUserQuery());
        return response;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        var result = await _mediator.Send(command); 
        return Ok(result);
    }
}
