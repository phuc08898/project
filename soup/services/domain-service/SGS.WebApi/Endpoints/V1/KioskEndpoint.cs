using Asp.Versioning.Builder;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SGS.Application.DataTransferObjects;
using SGS.Application.UseCases.KioskUC.Command.CreateKioskCommand;
using SGS.Application.UseCases.KioskUC.Command.UpdateKioskCommand;
using SGS.Webapi.Extensions;
using SGS.Webapi.Types;
using SGS.Application.UseCases.KioskUC.Queries.GetKiosk;
using SGS.Application.UseCases.KioskUC.Queries.AllKiosk;
using SGS.Application.UseCases.CategoryUC.Command.DeleteCategoryCommand;
using SGS.Application.UseCases.KioskUC.Command.DeleteKioskCommand;
using SGS.Application.UseCases.KioskUC.Command.AutoCreateKioskCommand;

namespace SGS.WebApi.Endpoints.V1
{
    public class KioskEndpointV1 : IEndpoint
    {
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints, ApiVersionSet version)
        {
            var kioskGroup = endpoints
                .MapGroup($"{EndpointExntensions.BASE_ROUTE}/kiosks")
                .WithDisplayName("Kiosks")
                .WithApiVersionSet(version)
                .HasApiVersion(1);

            //kioskGroup.MapPost("/", KioskPostAsync);
            kioskGroup.MapPost("/", KioskAutoPostAsync);
            kioskGroup.MapGet("/{id}", KioskGetAsync).WithName("GetKiosk");
            kioskGroup.MapGet("/", KioskAllAsync);
            kioskGroup.MapPut("/{id}", KioskPutAsync);
            kioskGroup.MapDelete("/{id}", KioskDeleteAsync);
            return endpoints;
        }

        // private method 
        private async Task<IResult> KioskPostAsync(
            [FromServices] IMediator mediator, CreateKioskArg arg)
            => (await mediator.Send(new CreateKioskCommand(arg)))
            .ToOk(e => Results.Ok(e));

        private async Task<IResult> KioskAutoPostAsync(
            [FromServices] IMediator mediator)
            => (await mediator.Send(new AutoCreateKioskCommand()))
            .ToOk(e => Results.Ok(e));

        private async Task<IResult> KioskGetAsync(
        [FromServices] IMediator mediator, string id, APIRequest request)
        => (await mediator.Send(new GetKioskQuery(id,
            request.TryParseSList(),
            request.Joins)))
            .ToOk(e => Results.Ok(e));

        private async Task<IResult> KioskAllAsync(
            [FromServices] IMediator mediator,
            APIPageRequest request)
        {
            var newRequest = new AllKioskQuery(
                request.Limit,
                request.Offset,
                request.Page,
                request.TryParseSList(),
                request.Joins);
            var response = await mediator.Send(newRequest);

            return response.ToOk(e => Results.Ok(e));
        }

        private async Task<IResult> KioskPutAsync(
        [FromServices] IMediator mediator, string id, UpdateKioskArg arg)
        => (await mediator.Send(new UpdateKioskCommand(id, arg)))
            .ToOk(e => Results.Ok(e));

        private async Task<IResult> KioskDeleteAsync(
        [FromServices] IMediator mediator, string id)
        => (await mediator.Send(new DeleteKioskCommand(id)))
            .ToOk(e => Results.Ok(e));
    }
}
