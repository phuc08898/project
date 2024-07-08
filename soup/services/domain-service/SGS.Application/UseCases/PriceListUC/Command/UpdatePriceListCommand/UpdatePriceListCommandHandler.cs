using Mapster;
using Microsoft.EntityFrameworkCore;
using SGS.Application.Core.MediatRCustom;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Entities;
using SGS.Domain.ValueObject;
using SGS.Infrastructure.Common.Data;



namespace SGS.Application.UseCases.PriceListUC.Command.UpdatePriceListCommand;

public class UpdatePriceListCommandHandler(
    IUnitOfWork unitOfWork) : ICommandHandlerV2<UpdatePriceListCommand, OkResponse>
{
    public async Task<ResultV2<OkResponse>> Handle(UpdatePriceListCommand request, CancellationToken cancellationToken)
    {
        
        var pricelistRepository = unitOfWork.GetRepository<PriceList>();
        var priceListDetailRepository = unitOfWork.GetRepository<PriceListDetail>();
        var priceList = await pricelistRepository.GetByIdAsync(request.Id);
        if (priceList == null)
            return ResultV2.NotFound(
                new InvalidOperationException($"PriceList not found!"));
        priceList.Name = request.Arg.Name;
        priceList.BeginDay = request.Arg.BeginDay;
        priceList.EndDay = request.Arg.EndDay;

        var existingDetails = await priceListDetailRepository.BuildQuery.Where(p => p.PriceListId == request.Id).ToListAsync();
        List<string> detailIdsExist = new List<string>();
        foreach ( var item in request.Arg.UpdatePriceListDetailArgs!)
        {
            if(item.Id != null)
            {
                var existingDetail = existingDetails.FirstOrDefault(d => d.Id == item.Id);
                if(existingDetail != null)
                {
                    existingDetail.Pricre = item.Price;
                    existingDetail.DiscountPercent = item.DiscountPercent;
                    detailIdsExist.Add(existingDetail.Id);
                }
            }
            else
            {
                var newDetail = new PriceListDetail
                {
                    PriceListId = request.Id,
                    VariantId = item.VariantId,
                    ProductId = item.ProductId,
                    Pricre = item.Price,
                    DiscountPercent = item.DiscountPercent,
                };
                existingDetails.Add(newDetail);
                detailIdsExist.Add(newDetail.Id);
            }
        }
        foreach ( var detail in existingDetails)
        {
            if (!detailIdsExist.Contains(detail.Id))
            {
                existingDetails.Remove(detail);
            }
        }
        priceList.PriceListDetails = existingDetails;
        return ResultV2.Success();

    }
}