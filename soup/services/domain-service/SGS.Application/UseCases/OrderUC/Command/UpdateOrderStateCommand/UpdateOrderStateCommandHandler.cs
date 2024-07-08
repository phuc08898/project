
using SGS.Application.Core.MediatRCustom;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Entities;
using SGS.Domain.Enums;
using SGS.Infrastructure.Common.Data;

namespace SGS.Application.UseCases.OrderUC.Command.UpdateOrderStateCommand;

public class UpdateOrderStateCommandHandler(
    IUnitOfWork unitOfWork) : ICommandHandlerV2<UpdateOrderStateCommand, OkResponse>
{
    public async Task<ResultV2<OkResponse>> Handle(UpdateOrderStateCommand request, CancellationToken cancellationToken)
    {

        var orderRepository = unitOfWork.GetRepository<Order>();
        var paymentRepository = unitOfWork.GetRepository<Transaction>();

        await unitOfWork.BeginTransactionAsync(cancellationToken);

        var order = await orderRepository.GetByIdAsync(request.Arg.OrderId);
        if (order == null)
            return ResultV2.NotFound(
                new InvalidOperationException($"Order {request.Arg.OrderId} not found!"));

        order.State = request.Arg.State.Current;

        var payment = await paymentRepository.GetByIdAsync(request.Arg.RequestId);
        if (payment is null)
            return ResultV2.NotFound(
                new InvalidOperationException($"Payment {request.Arg.RequestId} not found!"));
        payment.State = TransactionStates.SUCCEED;

        order.TransId = request.Arg.RequestId;

        return ResultV2.Success();
    }
}