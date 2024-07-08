using MediatR;
using SGS.Application.Core.MediatRCustom;
using SGS.Application.DataTransferObjects;
using SGS.Application.DTOs;
using SGS.Domain.Common.Primitives;


namespace SGS.Application.UseCases.PriceListUC.Command.UpdatePriceListCommand;

public class UpdatePriceListCommand(string id, UpdatePriceListArg arg) : ICommandV2<OkResponse>
{
    public string Id { get; set; } = id;
    public UpdatePriceListArg Arg = arg;
}