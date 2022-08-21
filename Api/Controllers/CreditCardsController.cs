using Contracts.Requests;
using Data.Services.CreditCards;
using Data.Context;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CreditCardsController : ControllerBase
{
    private readonly DatabaseContext _context;
    private readonly ICreditCardService _creditCardService;
    private readonly IValidator<CreditCardRequest> _cardRequestValidator;

    public CreditCardsController(
        ICreditCardService creditCardService,
        IValidator<CreditCardRequest> cardRequestValidator,
        DatabaseContext context)
    {
        _creditCardService = creditCardService;
        _cardRequestValidator = cardRequestValidator;
        _context = context;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get()
    {
        return Ok(await _context.Params.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var response = await _creditCardService.Get(id);

        return response.Match<IActionResult>(
            card => Created($"/api/creditcards/{card.Id}", card),
            error => BadRequest(error.First())
        );
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreditCardRequest request)
    {
        var validation = await _cardRequestValidator.ValidateAsync(request);

        if (!validation.IsValid)
        {
            return BadRequest(validation.Errors?.Select(e => new
            {
                e.ErrorCode,
                e.PropertyName,
                e.ErrorMessage
            }));
        }

        var response = await _creditCardService.Create(request);
        return Created($"/api/creditcards/{response.Id}", response);
    }
}