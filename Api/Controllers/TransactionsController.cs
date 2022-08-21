using Contracts.Requests;
using Data.Services.Transactions;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _transactionService;
    private readonly IValidator<TransactionRequest> _transactionValidator;
    
    public TransactionsController(
        ITransactionService transactionService, 
        IValidator<TransactionRequest> transactionValidator)
    {
        _transactionService = transactionService;
        _transactionValidator = transactionValidator;
    }

    [HttpPut]
    public async Task<IActionResult> Put(TransactionRequest request)
    {
        var validation = await _transactionValidator.ValidateAsync(request);
        
        if (!validation.IsValid)
        {
            return BadRequest(validation.Errors?.Select(e => new
            {
                e.ErrorCode,
                e.PropertyName,
                e.ErrorMessage
            }));
        }
        
        var response = await _transactionService.Add(request);

        return response.Match<IActionResult>(
            transaction => Created($"/api/transactions/{transaction.Id}", transaction),
            error => BadRequest(error.First())
        );
    }
}