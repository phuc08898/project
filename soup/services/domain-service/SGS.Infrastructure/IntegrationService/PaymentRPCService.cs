using Grpc.Core;
using Grpc.Net.Client;
using Mapster;
using Microsoft.Extensions.Configuration;
using SGS.Application.DataTransferObjects;
using SGS.Application.IIntegrationService;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Common.Utils;
using SGS.Domain.Enums;
using SGS.Infrastructure.Common.Exceptions;

namespace SGS.Infrastructure.IntegrationService;

public class PaymentRPCService : IPaymentRPCService
{
    private readonly PaymentRPC.PaymentRPCClient _paymentClient;

    public PaymentRPCService(IConfiguration configuration)
    {
        string host = configuration.GetConnectionString("GRpc") ?? "http://localhost:5002";
        _paymentClient = new PaymentRPC.PaymentRPCClient(GrpcChannel.ForAddress(host));
    }

    public async Task<ResultV2<CreateMomoResponseFromPaymentHub>> CreateMomoTransactionAsync(
        CreateMomoRequestToPaymentHub request,
        CancellationToken cancellationToken = default)
    {
        MomoCreateRequest momoCreateRequest = request.ObjectClone<MomoCreateRequest>();
        try
        {
            var response = await _paymentClient.CreateMomoTransactionAsync(
                momoCreateRequest,
                null,
                null,
                cancellationToken);


            if (response.ResultCode == 0)
            {
                return response.Adapt<CreateMomoResponseFromPaymentHub>();
            }

            return ResultV2.UnProcess(new ProcessException(response.Message));
        }
        catch (RpcException ex)
        {
            return ResultV2.Failure(
                new ProcessException(ex.Message),
                ErrorCodes.Unavailable);
        }
    }
}