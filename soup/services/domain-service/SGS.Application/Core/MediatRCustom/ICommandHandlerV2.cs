using MediatR;
using SGS.Domain.Common.Primitives;

namespace SGS.Application.Core.MediatRCustom;

/// <summary>
/// Represents the command handler interface.
/// </summary>
/// <typeparam name="TCommand">The command type.</typeparam>
/// <typeparam name="TTResponse">The command response type.</typeparam>
public interface ICommandHandlerV2<in TCommand, TTResponse> :
    ICommandHandler<TCommand, ResultV2<TTResponse>>
        where TCommand : ICommandV2<TTResponse>
{
}