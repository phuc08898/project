using FluentValidation.Results;
using MediatR;
namespace SGS.Application.Core.MediatRCustom;
/// <summary>
/// Represents the command interface.
/// </summary>
/// <typeparam name="TTResponse">The command response type.</typeparam>
public interface ICommand<out TTResponse> : IRequest<TTResponse>
{
    // Task<ValidationResult> ValidateAsync(CancellationToken cancellationToken = default);
}