
using Mapster;
using SGS.Application.Core.MediatRCustom;
using SGS.Application.DataTransferObjects;
using SGS.Application.IIntegrationService;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Entities;
using SGS.Domain.Enums;
using SGS.Infrastructure.Common.Data;

namespace SGS.Application.UseCases.OrderUC.Command.CreateOrderCommand;

public class CreateOrderCommandHandler(IUnitOfWork unitOfWork,
    IPaymentRPCService paymentRPCService) :
    ICommandHandlerV2<CreateOrderCommand, OkResponse<CreateOrderResult>>
{
    public async Task<ResultV2<OkResponse<CreateOrderResult>>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        // var valiResult = await request.ValidateAsync(cancellationToken);
        // if (!valiResult.IsValid)
        //     return ResultV2.BadRequest(new ValidationException(isValid.Errors));
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        // add order
        var orderRepository = unitOfWork.GetRepository<Order>();
        var newOrder = request.Adapt<Order>();
        orderRepository.Add(newOrder);

        // add transaction
        var paymentRepository = unitOfWork.GetRepository<Transaction>();
        var newTrans = new Transaction(
                orderId: newOrder.Id,
                amount: newOrder.Amount,
                paymentMethod: request.PaymentMethod);
        paymentRepository.Add(newTrans);

        // switch payment method
        return request.PaymentMethod switch
        {
            PaymentMethods.MOMO => await HandleMonoMethodAsync(newOrder, newTrans.Id, cancellationToken),
            _ => ResultV2.Success(new CreateOrderResult(newOrder.Id))
        };
    }


    /// <summary>
    /// handle momo method 
    /// </summary>
    /// <param name="newOrder"></param>
    /// <param name="transId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<ResultV2<OkResponse<CreateOrderResult>>> HandleMonoMethodAsync
        (Order newOrder, string transId, CancellationToken cancellationToken = default)
    {
        var momoResponse = await paymentRPCService.CreateMomoTransactionAsync
                     (new CreateMomoRequestToPaymentHub(
                         newOrder.Id, newOrder.Amount, "created by SGS", "", transId
                     ), cancellationToken);

        return momoResponse.Match(
            success => ResultV2.Success(new CreateOrderResult(newOrder.Id, success.PayUrl)),
            fal => ResultV2.Failure<OkResponse<CreateOrderResult>>(fal));
    }

}