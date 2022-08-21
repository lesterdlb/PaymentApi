using Contracts.Requests;
using FluentValidation;

namespace Api.Validation;

public class TransactionRequestValidator : AbstractValidator<TransactionRequest>
{
    public TransactionRequestValidator()
    {
        RuleFor(model => model.CardId)
            .NotEmpty()
            .WithMessage("Please submit a Card Id.");
        RuleFor(model => model.Date)
            .NotEmpty()
            .WithMessage("Please submit the date of the transaction.");
        RuleFor(model => model.Amount)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Please enter the amount of the transaction.");
    }
}