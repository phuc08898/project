
using FluentValidation.Results;
using SGS.Application.Core.MediatRCustom;
using SGS.Application.DataTransferObjects;
using SGS.Domain.Common.Primitives;

namespace SGS.Application.UseCases.OrderUC.Command.UpdateOrderStateCommand;

public class UpdateOrderStateCommand(UpdateOrderStateArg arg) : ICommandV2<OkResponse>
{
    public UpdateOrderStateArg Arg = arg;


    /// <summary>
    /// Nothing to valid
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<ValidationResult> ValidateAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(new ValidationResult());
    }
}