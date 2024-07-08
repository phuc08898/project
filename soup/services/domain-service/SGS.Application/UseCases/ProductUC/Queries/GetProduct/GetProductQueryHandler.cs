
using SGS.Application.Core.MediatRCustom;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Entities;
using SGS.Domain.Enums;
using SGS.Infrastructure.Common.Data;

namespace SGS.Application.UseCases.ProductUC.Queries;

public class GetProductQueryHandler(
IUnitOfWork unitOfWork) :
IQueryHandler<GetProductQuery, ResultV2<OkResponse<Product>>>
{
    public async Task<ResultV2<OkResponse<Product>>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var productRepos = unitOfWork.GetRepository<Product>();
        var newQuery = request.Joins is null ?
           productRepos.BuildQuery :
           productRepos.BuildQueryWithInclude(request.Joins);

        var product = await productRepos
            .FirstOrDefaultAsync(request.Id, newQuery, cancellationToken);

        if (product is null)
            return ResultV2.NotFound(new InvalidOperationException($"Not found Product {request.Id}"));

        return new OkResponse<Product>(product);
    }
}

