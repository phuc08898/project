using Asp.Versioning.Builder;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SGS.Application.UseCases.ProductUC.Queries;
using SGS.Webapi.Types;
using SGS.Application.DataTransferObjects;
using SGS.Webapi.Extensions;
using SGS.Application.UseCases.ProductUC.CreateProductCommand;
using SGS.Application.UseCases.ProductUC.Command.UpdateProductCommand;
using SGS.Application.UseCases.ProductUC.Command;
using SGS.Application.UseCases.ProductUC;


namespace SGS.WebApi.Endpoints.V1;

public class ProductEndpointV1 : IEndpoint
{

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints, ApiVersionSet version)
    {
        var productGroup = endpoints
       .MapGroup($"{EndpointExntensions.BASE_ROUTE}/product")
       .WithDisplayName("Product")
       .WithApiVersionSet(version)
       .HasApiVersion(1);

        productGroup.MapPost("/", ProductPostAsync);
        productGroup.MapGet("/{id}", ProductGetAsync).WithName("GetProduct");
        productGroup.MapGet("/", ProductAllAsync);
        productGroup.MapPut("/{id}", ProductPutAsync).WithName("PutProduct");
        productGroup.MapDelete("/{id}", ProductDeleteAsync).WithName("DeleteProduct");
        return endpoints;
    }

    private async Task<IResult> ProductPostAsync(
        [FromServices] IMediator mediator, CreateProductArg arg)
        => (await mediator.Send(new CreateProductCommand(arg)))
        .ToOk(e => Results.Ok(e));
    private async Task<IResult> ProductGetAsync(
        [FromServices] IMediator mediator, string id, APIRequest request)
        => (await mediator.Send(new GetProductQuery(id,
            request.TryParseSList(),
            request.Joins)))
            .ToOk(e => Results.Ok(e));

    private async Task<IResult> ProductAllAsync(
        [FromServices] IMediator mediator,
        APIPageRequest request)
    {
        var newRequest = new AllProductQuery(
            request.Limit,
            request.Offset,
            request.Page,
            request.TryParseSList(),
            request.Joins);
        var response = await mediator.Send(newRequest);

        return response.ToOk(e => Results.Ok(e));
    }

    private async Task<IResult> ProductPutAsync(
        [FromServices] IMediator mediator, string id, UpdateProductArg arg)
        => (await mediator.Send(new UpdateProductCommand(id, arg)))
            .ToOk(e => Results.Ok(e));

    private async Task<IResult> ProductDeleteAsync(
        [FromServices] IMediator mediator, string Id)
    {
        var DeleteComand = new DeleteProductCommand(Id);
        var response = await mediator.Send(DeleteComand);

        return response.ToOk(e => Results.Ok(e));
    }
}