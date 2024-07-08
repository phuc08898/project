
using SGS.Application.Core.MediatRCustom;
using SGS.Application.UseCases.OrderUC.Queries.GetOrder;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Entities;
using SGS.Domain.Enums;
using SGS.Infrastructure.Common.Data;

namespace SGS.Application.UseCases.OrderUC.Queries;

public class GetOrderQueryHandler(
    IUnitOfWork unitOfWork) :
    IQueryHandler<GetOrderQuery, ResultV2<OkResponse<Order>>>
{
    public async Task<ResultV2<OkResponse<Order>>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var orderRepos = unitOfWork.GetRepository<Order>();
        var newQuery = request.Joins is null ?
           orderRepos.BuildQuery :
           orderRepos.BuildQueryWithInclude(request.Joins);

        var order = await orderRepos.FirstOrDefaultAsync(request.Id, newQuery, cancellationToken);

        if (order is null)
            return ResultV2.NotFound(new InvalidOperationException($"Not found order {request.Id}"));

        return new OkResponse<Order>(order);
    }

}