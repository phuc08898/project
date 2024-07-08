using SGS.Application.Core.MediatRCustom;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Common.Utils;
using SGS.Domain.Entities;
using SGS.Infrastructure.Common.Data;


namespace SGS.Application.UseCases.PriceListUC.Queries;

public class AllPriceListQueryHandler(
IUnitOfWork unitOfWork) :
IQueryHandler<AllPriceListQuery, ResultV2<OkPageResponse<PriceList[]>>>
{
    public async Task<ResultV2<OkPageResponse<PriceList[]>>> Handle(AllPriceListQuery request, CancellationToken cancellationToken)
    {
        var priceListRepos = unitOfWork.GetRepository<PriceList>();
        var newQuery = priceListRepos.HandleLinqQueryRequestV2(request);
        var (executeQuery, totalRecords, totalPages) = newQuery.HandleLinqQueryPageRequestV2(request);

        var data = await priceListRepos.ToArrayAsync(executeQuery, cancellationToken);

        return new OkPageResponse<PriceList[]>(
            data,
            totalRecords,
            totalPages,
            request.Page,
            request.Offset);
    }
}
