using FluentValidation;
using Ordering.API.Commands;

namespace Ordering.API.Validations;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(command => command.BuyerEmail).NotEmpty();
        RuleFor(command => command.BuyerName).NotEmpty();
        RuleFor(command => command.Street).NotEmpty();
        RuleFor(command => command.City).NotEmpty();
        RuleFor(command => command.County).NotEmpty();
        RuleFor(command => command.Country).NotEmpty();
        RuleFor(command => command.PostCode).NotEmpty();
        RuleFor(command => command.OrderItems).Must(be => be.Any()).WithMessage("No order items found");
    }
}