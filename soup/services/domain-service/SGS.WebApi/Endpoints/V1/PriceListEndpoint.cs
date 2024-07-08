using Asp.Versioning.Builder;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SGS.Application.DataTransferObjects;
using SGS.Application.UseCases.PriceListUC.Command;
using SGS.Application.UseCases.PriceListUC.Command.CreatePriceListCommand;
using SGS.Application.UseCases.PriceListUC.Command.UpdatePriceListCommand;
using SGS.Application.UseCases.PriceListUC.Queries;
using SGS.Application.UseCases.ProductUC.Command;
using SGS.Application.UseCases.ProductUC.Queries;
using SGS.Webapi.Extensions;
using SGS.Webapi.Types;

namespace SGS.WebApi.Endpoints.V1;

public class PriceListEndpoint : IEndpoint
{
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints, ApiVersionSet version)
    {
        var pricelistGroup = endpoints
       .MapGroup($"{EndpointExntensions.BASE_ROUTE}/pricelist")
       .WithDisplayName("PriceList")
       .WithApiVersionSet(version)
       .HasApiVersion(1);

        pricelistGroup.MapPost("/", PriceListPostAsync);
        pricelistGroup.MapGet("/{id}", PriceListGetAsync).WithName("GetPriceList");
        pricelistGroup.MapGet("/", PriceListAllAsync);
        pricelistGroup.MapPut("/{id}", PriceListUpdateAsync).WithName("PutPriceList");
        pricelistGroup.MapDelete("/{id}", PriceListDeleteAsync).WithName("DeletePriceList");
        return endpoints;
    }

    private async Task<IResult> PriceListPostAsync(
        [FromServices] IMediator mediator, CreatePriceListArg arg)
        => (await mediator.Send(new CreatePriceListCommand(arg)))
        .ToOk(e => Results.Ok(e));
    private async Task<IResult> PriceListGetAsync(
        [FromServices] IMediator mediator, string id, APIRequest request)
        => (await mediator.Send(new GetPriceListQuery(id,
            request.TryParseSList(),
            request.Joins)))
            .ToOk(e => Results.Ok(e));

    private async Task<IResult> PriceListAllAsync(
        [FromServices] IMediator mediator,
        APIPageRequest request)
    {
        var newRequest = new AllPriceListQuery(
            request.Limit,
            request.Offset,
            request.Page,
            request.TryParseSList(),
            request.Joins);
        var response = await mediator.Send(newRequest);

        return response.ToOk(e => Results.Ok(e));
    }

    private async Task<IResult> PriceListUpdateAsync(
        [FromServices] IMediator mediator, string id, UpdatePriceListArg arg)
        => (await mediator.Send(new UpdatePriceListCommand(id,arg)))
            .ToOk(e => Results.Ok(e));

    private async Task<IResult> PriceListDeleteAsync(
        [FromServices] IMediator mediator, string Id)
    {
        var DeleteCommand = new DeletePriceListCommand(Id);
        var response = await mediator.Send(DeleteCommand);

        return response.ToOk(e => Results.Ok(e));
    }


}
