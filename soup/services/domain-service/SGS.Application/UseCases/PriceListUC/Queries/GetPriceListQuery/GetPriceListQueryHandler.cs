using SGS.Application.Core.MediatRCustom;
using SGS.Application.UseCases.ProductUC.Queries;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Entities;
using SGS.Infrastructure.Common.Data;

namespace SGS.Application.UseCases.PriceListUC.Queries;

public class GetPriceListQueryHandler(
IUnitOfWork unitOfWork) :
IQueryHandler<GetPriceListQuery, ResultV2<OkResponse<PriceList>>>
{
    public async Task<ResultV2<OkResponse<PriceList>>> Handle(GetPriceListQuery request, CancellationToken cancellationToken)
    {
        var priceListRepos = unitOfWork.GetRepository<PriceList>();
        var newQuery = request.Joins is null ?
           priceListRepos.BuildQuery :
           priceListRepos.BuildQueryWithInclude(request.Joins);

        var priceList = await priceListRepos
            .FirstOrDefaultAsync(request.Id, newQuery, cancellationToken);

        if (priceList is null)
            return ResultV2.NotFound(new InvalidOperationException($"Not found PriceList"));

        return new OkResponse<PriceList>(priceList);
    }
}
