using SGS.Application.Core.MediatRCustom;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Entities;
using SGS.Infrastructure.Common.Data;

namespace SGS.Application.UseCases.CategoryUC.Queries.GetCategory;

public class GetCategoryQueryHandler(
IUnitOfWork unitOfWork) :
IQueryHandler<GetCategoryQuery, ResultV2<OkResponse<Category>>>
{
    public async Task<ResultV2<OkResponse<Category>>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var categoryRepos = unitOfWork.GetRepository<Category>();
        var newQuery = request.Joins is null ?
           categoryRepos.BuildQuery :
           categoryRepos.BuildQueryWithInclude(request.Joins);

        var category = await categoryRepos.FirstOrDefaultAsync(request.Id, newQuery, cancellationToken);


        if (category is null)
            return ResultV2.NotFound(new InvalidOperationException($"Not found Product {request.Id}"));

        return new OkResponse<Category>(category);
    }
}

