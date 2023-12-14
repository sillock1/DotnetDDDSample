using MediatR;

namespace Ordering.API.Apis;

public class OrderService(IMediator mediator, ILogger<OrderService> logger)
{
    public IMediator Mediator { get; set; } = mediator;
    public ILogger<OrderService> Logger { get; set; } = logger;
}