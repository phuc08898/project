
using SGS.Application.Core.MediatRCustom;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Entities;

namespace SGS.Application.UseCases.OrderUC.Queries;

public class AllOrderQuery(int limit, int offset, int page,
    Dictionary<string, Dictionary<string, string>>? conditions,
    string[]? joins) :
        QueryPageRequest(limit, offset, page, conditions, joins),
        IQuery<ResultV2<OkPageResponse<Order[]>>>
{
}