using Microsoft.EntityFrameworkCore;
using SGS.Application.Core.MediatRCustom;
using SGS.Application.UseCases.ProductUC.Command;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Entities;
using SGS.Domain.Enums;
using SGS.Infrastructure.Common.Data;

namespace SGS.Application.UseCases.PriceListUC.Command;

public class DeletePriceListCommandHandler(IUnitOfWork unitOfWork) :
ICommandHandlerV2<DeletePriceListCommand, OkResponse>
{
    public async Task<ResultV2<OkResponse>> Handle(DeletePriceListCommand request, CancellationToken cancellationToken)
    {
        var pricelistRepository = unitOfWork.GetRepository<PriceList>();
/*        var priceListDetailRepository = unitOfWork.GetRepository<PriceListDetail>();
        var existingDetails = await priceListDetailRepository.BuildQuery.Where(p => p.PriceListId == request.Id).ToListAsync();*/
        var DeletePricelist = await pricelistRepository.GetByIdAsync(request.Id);
        if (DeletePricelist == null)
            return ResultV2.NotFound(
                new InvalidOperationException($"PriceList not found!"));
        pricelistRepository.Remove(DeletePricelist);
/*        DeletePricelist.Status = CommonStatuses.DELETED;
        DeletePricelist.DeletedOn = DateTimeOffset.UtcNow;
        foreach( var deleteDetail in existingDetails)
        {
            priceListDetailRepository.Remove(deleteDetail);
        }*/
        return ResultV2.Success();
    }
}
