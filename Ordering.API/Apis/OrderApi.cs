using Microsoft.AspNetCore.Http.HttpResults;
using Ordering.API.Commands;
using Ordering.API.Model;

namespace Ordering.API.Apis;

public static class OrderApi
{
    public static RouteGroupBuilder MapOrderApi(this RouteGroupBuilder app)
    {
        app.MapPost("/", CreateOrderAsync);
        return app;
    }

    public static async Task<Results<Ok, ProblemHttpResult>> CreateOrderAsync(
        CheckoutBasketRequest request,
        [AsParameters] OrderService service)
    {
        service.Logger.LogInformation("Checking out order with customer email: {BuyerEmail}", request.BuyerEmail);

        var createOrderCommand = new CreateOrderCommand(
            request.Items,
            request.BuyerEmail,
            request.BuyerName,
            request.Street,
            request.City,
            request.County,
            request.Country,
            request.PostCode);

        var response = await service.Mediator.Send(createOrderCommand);
        return TypedResults.Ok();
    }
}

public record CheckoutBasketRequest(
    string BuyerEmail,
    string BuyerName,
    string Street,
    string City,
    string County,
    string Country,
    string PostCode,
    List<BasketItem> Items);