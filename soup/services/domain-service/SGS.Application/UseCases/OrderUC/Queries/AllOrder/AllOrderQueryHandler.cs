using SGS.Application.Core.MediatRCustom;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Common.Utils;
using SGS.Domain.Entities;
using SGS.Infrastructure.Common.Data;

namespace SGS.Application.UseCases.OrderUC.Queries;

public class AllOrderQueryHandler(
    IUnitOfWork unitOfWork) :
    IQueryHandler<AllOrderQuery, ResultV2<OkPageResponse<Order[]>>>
{
    public async Task<ResultV2<OkPageResponse<Order[]>>> Handle(AllOrderQuery request, CancellationToken cancellationToken)
    {
        var orderRepos = unitOfWork.GetRepository<Order>();
        var newQuery = orderRepos.HandleLinqQueryRequest(request);
        var (executeQuery, totalRecords, totalPages) = newQuery.HandleLinqQueryPageRequest(request);

        var data = await orderRepos.ToArrayAsync(executeQuery, cancellationToken);

        return new OkPageResponse<Order[]>(
            data,
            totalRecords,
            totalPages,
            request.Page,
            request.Offset);
    }
}