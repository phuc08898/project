using SGS.Application.Core.MediatRCustom;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Common.Utils;
using SGS.Domain.Entities;
using SGS.Infrastructure.Common.Data;


namespace SGS.Application.UseCases.KioskUC.Queries.AllKiosk
{
    public class AllKioskQueryHandler(
        IUnitOfWork unitOfWork) :
        IQueryHandler<AllKioskQuery, ResultV2<OkPageResponse<Kiosk[]>>>
    {
        public async Task<ResultV2<OkPageResponse<Kiosk[]>>> Handle(AllKioskQuery request, CancellationToken cancellationToken)
        {
            var kioskRepos = unitOfWork.GetRepository<Kiosk>();
            var newQuery = kioskRepos.HandleLinqQueryRequestV2(request);
            var (executeQuery, totalRecords, totalPages) = newQuery.HandleLinqQueryPageRequestV2(request);

            var data = await kioskRepos.ToArrayAsync(executeQuery, cancellationToken);

            return new OkPageResponse<Kiosk[]>(
                data,
                totalRecords,
                totalPages,
                request.Page,
                request.Offset);
        }
    }
}
