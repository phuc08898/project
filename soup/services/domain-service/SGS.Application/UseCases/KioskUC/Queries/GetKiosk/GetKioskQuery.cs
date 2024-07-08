using SGS.Application.Core.MediatRCustom;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGS.Application.UseCases.KioskUC.Queries.GetKiosk
{
    public class GetKioskQuery(string id,
    Dictionary<string, Dictionary<string, List<SQueryItem>>>? conditions,
    string[]? joins) : IQuery<ResultV2<OkResponse<Kiosk>>>
    {
        public string Id { get; set; } = id;
        public Dictionary<string, Dictionary<string, List<SQueryItem>>>? Conditions { get; } = conditions;
        public string[]? Joins { get; } = joins;
    }
}
