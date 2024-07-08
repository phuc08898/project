using MediatR;
namespace SGS.Application.Core.MediatRCustom;

/// <summary>
/// Represents the query interface.
/// </summary>
/// <typeparam name="TTResponse">The query response type.</typeparam>
public interface IQuery<out TTResponse> : IRequest<TTResponse>
{
}