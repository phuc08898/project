using MediatR;
using SGS.Domain.Common.Primitives;

namespace SGS.Application.Core.MediatRCustom;

/// <summary>
/// Represents the command handler interface.
/// </summary>
/// <typeparam name="TCommand">The command type.</typeparam>
/// <typeparam name="TTResponse">The command response type.</typeparam>
public interface ICommandHandler<in TCommand, TTResponse> :
    IRequestHandler<TCommand, TTResponse>
        where TCommand : ICommand<TTResponse>
{
}