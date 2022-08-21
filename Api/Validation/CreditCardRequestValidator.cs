using Contracts.Requests;
using FluentValidation;

namespace Api.Validation;

public class CreditCardRequestValidator : AbstractValidator<CreditCardRequest>
{
    public CreditCardRequestValidator()
    {
        RuleFor(model => model.CardNumber)
            .NotEmpty()
            .CreditCard()
            .WithMessage("Please submit a valid Credit Card.");
        RuleFor(model => model.CardHolderName)
            .NotEmpty()
            .WithMessage("Please submit the name of the card holder.");
        RuleFor(model => model.ExpirationDate)
            .NotEmpty()
            .WithMessage("Please enter a valid date.");
        RuleFor(model => model.Cvv)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(3);
        RuleFor(model => model.Balance)
            .NotEmpty()
            .GreaterThan(0);
    }
}