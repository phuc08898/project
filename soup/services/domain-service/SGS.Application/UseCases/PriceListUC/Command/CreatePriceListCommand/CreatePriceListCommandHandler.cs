using Mapster;
using SGS.Application.Core.MediatRCustom;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Entities;
using SGS.Domain.ValueObject;
using SGS.Infrastructure.Common.Data;

namespace SGS.Application.UseCases.PriceListUC.Command.CreatePriceListCommand;

public class CreatePriceListCommandHandler(IUnitOfWork unitOfWork) : ICommandHandlerV2<CreatePriceListCommand, OkResponse>
{
    public Task<ResultV2<OkResponse>> Handle(CreatePriceListCommand request, CancellationToken cancellationToken)
    {
        var pricelistRepository = unitOfWork.GetRepository<PriceList>();
        var newPriceList = request.Adapt<PriceList>();
        var listDetail = request.PriceListDetailArgs
                .Select(detail => new PriceListDetail(
                    variantId: detail.VariantId,
                    productId: detail.ProductId,
                    priceListId: newPriceList.Id, 
                    discountPc: detail.DiscountPercent, 
                    price: detail.Price))
                .ToList();
         newPriceList.PriceListDetails = listDetail;
         pricelistRepository.Add(newPriceList);
         return Task.FromResult<ResultV2<OkResponse>>(ResultV2.Success());
    }
}
