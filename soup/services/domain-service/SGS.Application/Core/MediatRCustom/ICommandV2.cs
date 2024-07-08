
using SGS.Domain.Common.Primitives;
namespace SGS.Application.Core.MediatRCustom;

public interface ICommandV2<TTResponse> : ICommand<ResultV2<TTResponse>>
{

}
