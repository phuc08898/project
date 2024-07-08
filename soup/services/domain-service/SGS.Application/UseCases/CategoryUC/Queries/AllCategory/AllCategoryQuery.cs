using SGS.Application.Core.MediatRCustom;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Entities;


namespace SGS.Application.UseCases.CategoryUC.Queries;

public class AllCategoryQuery(int limit, int offset, int page,
Dictionary<string, Dictionary<string, List<SQueryItem>>>? conditions,
string[]? joins) :
QueryPageRequestV2(limit, offset, page, conditions, joins),
IQuery<ResultV2<OkPageResponse<Category[]>>>
{
}