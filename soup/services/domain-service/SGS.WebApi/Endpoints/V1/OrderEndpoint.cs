using Asp.Versioning.Builder;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SGS.Application.DataTransferObjects;
using SGS.Application.UseCases.OrderUC.Command.CreateOrderCommand;
using SGS.Application.UseCases.OrderUC.Queries;
using SGS.Application.UseCases.OrderUC.Queries.GetOrder;
using SGS.Webapi.Extensions;
using SGS.Webapi.Types;

namespace SGS.Webapi.Endpoints.V1;

public class OrderEndpointV1 : IEndpoint
{
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints, ApiVersionSet version)
    {
        var orderGroup = endpoints
            .MapGroup($"{EndpointExntensions.BASE_ROUTE}/order")
            .WithDisplayName("Orders")
            .WithApiVersionSet(version)
            .HasApiVersion(1);

        orderGroup.MapPost("/", OrderPostAsync);
        orderGroup.MapGet("/{id}", OrderGetAsync).WithName("GetOrder");
        orderGroup.MapGet("/", OrderAllAsync);

        return endpoints;
    }

    // private method 
    private async Task<IResult> OrderPostAsync(
        [FromServices] IMediator mediator, CreateOrderArg arg)
        => (await mediator.Send(new CreateOrderCommand(arg)))
        .ToOk(e => Results.CreatedAtRoute("GetOrder", new { id = e.Data!.Id }, e));

    private async Task<IResult> OrderGetAsync(
        [FromServices] IMediator mediator, string id, APIRequest request)
        => (await mediator.Send(new GetOrderQuery(id,
            request.TryParseS(),
            request.Joins)))
            .ToOk(e => Results.Ok(e));

    private async Task<IResult> OrderAllAsync(
        [FromServices] IMediator mediator,
        APIPageRequest request)
    {
        var newRequest = new AllOrderQuery(
            request.Limit,
            request.Offset,
            request.Page,
            request.TryParseS(),
            request.Joins);
        var response = await mediator.Send(newRequest);

        return response.ToOk(e => Results.Ok(e));
    }
}
