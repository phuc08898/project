using SGS.Application.Core.MediatRCustom;
using SGS.Domain.Common.Primitives;

namespace SGS.Application.UseCases.PriceListUC.Command;

public class DeletePriceListCommand(string id) : ICommandV2<OkResponse>
{
    public string Id { get; set; } = id;
}
