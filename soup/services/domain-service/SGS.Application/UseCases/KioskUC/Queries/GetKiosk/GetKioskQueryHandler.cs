using SGS.Application.Core.MediatRCustom;
using SGS.Application.UseCases.ProductUC.Queries;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Entities;
using SGS.Infrastructure.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGS.Application.UseCases.KioskUC.Queries.GetKiosk
{
    public class GetKioskQueryHandler(
        IUnitOfWork unitOfWork) :
        IQueryHandler<GetKioskQuery, ResultV2<OkResponse<Kiosk>>>
    {
        public async Task<ResultV2<OkResponse<Kiosk>>> Handle(GetKioskQuery request, CancellationToken cancellationToken)
        {
            var kioskRepos = unitOfWork.GetRepository<Kiosk>();
            var newQuery = request.Joins is null ?
               kioskRepos.BuildQuery :
               kioskRepos.BuildQueryWithInclude(request.Joins);

            var kiosk = await kioskRepos
                .FirstOrDefaultAsync(request.Id, newQuery, cancellationToken);

            if (kiosk is null)
                return ResultV2.NotFound(new InvalidOperationException($"Not found Kiosk!"));

            return new OkResponse<Kiosk>(kiosk);
        }
    }
}
