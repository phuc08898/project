using SGS.Application.Core.MediatRCustom;
using SGS.Application.UseCases.CategoryUC.Queries;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Common.Utils;
using SGS.Domain.Entities;
using SGS.Infrastructure.Common.Data;


namespace SGS.Application.UseCases.CategoryUC.Queries;

public class AllCategoryQueryHandler(
    IUnitOfWork unitOfWork) :
    IQueryHandler<AllCategoryQuery, ResultV2<OkPageResponse<Category[]>>>
{
    public async Task<ResultV2<OkPageResponse<Category[]>>> Handle(AllCategoryQuery request, CancellationToken cancellationToken)
    {
        var categoryRepos = unitOfWork.GetRepository<Category>();
        var newQuery = categoryRepos.HandleLinqQueryRequestV2(request);
        var (executeQuery, totalRecords, totalPages) = newQuery.HandleLinqQueryPageRequestV2(request);

        var data = await categoryRepos.ToArrayAsync(executeQuery, cancellationToken);

        return new OkPageResponse<Category[]>(
            data,
            totalRecords,
            totalPages,
            request.Page,
            request.Offset);
    }
}
