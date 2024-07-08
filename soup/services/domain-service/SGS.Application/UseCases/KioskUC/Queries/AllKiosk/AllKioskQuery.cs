using SGS.Application.Core.MediatRCustom;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGS.Application.UseCases.KioskUC.Queries.AllKiosk
{
    public class AllKioskQuery(int limit, int offset, int page,
    Dictionary<string, Dictionary<string, List<SQueryItem>>>? conditions,
    string[]? joins) :
        QueryPageRequestV2(limit, offset, page, conditions, joins),
        IQuery<ResultV2<OkPageResponse<Kiosk[]>>>
    {
    }
}
