

using FluentValidation.Results;
using SGS.Application.Core.MediatRCustom;
using SGS.Application.DataTransferObjects;
using SGS.Domain.Common.Primitives;

namespace SGS.Application.UseCases.OrderUC.Command.CreateOrderCommand;

public class CreateOrderCommand(CreateOrderArg arg) : ICommandV2<OkResponse<CreateOrderResult>>
{
    public long Amount { get; set; } = arg.Amount;
    public string PaymentMethod { get; set; } = arg.PaymentMethod;//= "";

    // public async Task<ValidationResult> ValidateAsync(CancellationToken cancellationToken = default)
    // {
    //     CreateOrderCommandValidator validationRules = new();
    //     ValidationResult validationResult = await validationRules.ValidateAsync(this, cancellationToken);
    //     return validationResult;
    // }
}

public class CreateOrderResult
{
    public string? PayQrUrl { get; set; }
    public string Id { get; set; }

    public CreateOrderResult(string id)
    {
        Id = id;
    }

    public CreateOrderResult(string id, string? payUrl)
    {
        Id = id;
        PayQrUrl = payUrl;
    }


}
