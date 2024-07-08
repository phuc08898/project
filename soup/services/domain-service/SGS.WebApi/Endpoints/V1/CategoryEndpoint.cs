using Asp.Versioning.Builder;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SGS.Application.UseCases.CategoryUC.Command.CreateCategoryCommand;
using SGS.Webapi.Extensions;
using SGS.Webapi.Types;
using SGS.Application.DTOs;
using SGS.Application.UseCases.CategoryUC.Queries.GetCategory;
using SGS.Application.UseCases.CategoryUC.Queries;

using SGS.Application.UseCases.CategoryUC.Command.DeleteCategoryCommand;
using SGS.Application.UseCases.CategoryUC.Command.UpdateCategoryCommand;


namespace SGS.WebApi.Endpoints.V1
{
    public class CategoryEndpointV1 : IEndpoint
    {
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints, ApiVersionSet version)
        {
            var categoryGroup = endpoints
                .MapGroup($"{EndpointExntensions.BASE_ROUTE}/categories")
                .WithDisplayName("Categories")
                .WithApiVersionSet(version)
                .HasApiVersion(1);

            categoryGroup.MapPost("/", CategoryPostAsync);
            categoryGroup.MapGet("/{id}", CategoryGetAsync).WithName("GetCategory");
            categoryGroup.MapGet("/", CategoryAllAsync);
            categoryGroup.MapPut("/{id}", CategoryPutAsync);
            categoryGroup.MapDelete("/{id}", CategoryDeleteAsync);
            return endpoints;
        }

        // private method 
        private async Task<IResult> CategoryPostAsync(
            [FromServices] IMediator mediator, CreateCategoryArg arg)
            => (await mediator.Send(new CreateCategoryCommand(arg)))
            .ToOk(e => Results.Ok(e));
        private async Task<IResult> CategoryGetAsync(
        [FromServices] IMediator mediator, string id, APIRequest request)
        => (await mediator.Send(new GetCategoryQuery(id,
            request.TryParseSList(),
            request.Joins)))
            .ToOk(e => Results.Ok(e));

        private async Task<IResult> CategoryAllAsync(
            [FromServices] IMediator mediator,
            APIPageRequest request)
        {
            var newRequest = new AllCategoryQuery(
                request.Limit,
                request.Offset,
                request.Page,
                request.TryParseSList(),
                request.Joins);
            var response = await mediator.Send(newRequest);

            return response.ToOk(e => Results.Ok(e));
        }
        private async Task<IResult> CategoryPutAsync(
        [FromServices] IMediator mediator, string id, UpdateCategoryNameArg arg)
        => (await mediator.Send(new UpdateCategoryCommand(id, arg)))
            .ToOk(e => Results.Ok(e));
        private async Task<IResult> CategoryDeleteAsync(
        [FromServices] IMediator mediator, string id)
        => (await mediator.Send(new DeleteCategoryCommand(id)))
            .ToOk(e => Results.Ok(e));
    }
}
