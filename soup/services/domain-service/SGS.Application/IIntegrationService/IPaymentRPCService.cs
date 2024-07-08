using SGS.Application.DataTransferObjects;
using SGS.Domain.Common.Primitives;

namespace SGS.Application.IIntegrationService;

public interface IPaymentRPCService
{
    Task<ResultV2<CreateMomoResponseFromPaymentHub>> CreateMomoTransactionAsync(
        CreateMomoRequestToPaymentHub request, CancellationToken cancellationToken = default
    );
}