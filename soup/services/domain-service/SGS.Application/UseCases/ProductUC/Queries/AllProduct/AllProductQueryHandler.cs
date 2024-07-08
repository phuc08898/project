using SGS.Application.Core.MediatRCustom;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Common.Utils;
using SGS.Domain.Entities;
using SGS.Infrastructure.Common.Data;



namespace SGS.Application.UseCases.ProductUC.Queries;
public class AllProductQueryHandler(
IUnitOfWork unitOfWork) :
IQueryHandler<AllProductQuery, ResultV2<OkPageResponse<Product[]>>>
{
    public async Task<ResultV2<OkPageResponse<Product[]>>> Handle(AllProductQuery request, CancellationToken cancellationToken)
    {
        var productRepos = unitOfWork.GetRepository<Product>();
        var newQuery = productRepos.HandleLinqQueryRequestV2(request);
        var (executeQuery, totalRecords, totalPages) = newQuery.HandleLinqQueryPageRequestV2(request);

        var data = await productRepos.ToArrayAsync(executeQuery, cancellationToken);

        return new OkPageResponse<Product[]>(
            data,
            totalRecords,
            totalPages,
            request.Page,
            request.Offset);
    }
}
