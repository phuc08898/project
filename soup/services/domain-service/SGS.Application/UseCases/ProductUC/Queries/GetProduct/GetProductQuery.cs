using SGS.Application.Core.MediatRCustom;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGS.Application.UseCases.ProductUC.Queries;

public class GetProductQuery(string id,
Dictionary<string, Dictionary<string, List<SQueryItem>>>? conditions,
string[]? joins) : IQuery<ResultV2<OkResponse<Product>>>
{
    public string Id { get; set; } = id;
    public Dictionary<string, Dictionary<string, List<SQueryItem>>>? Conditions { get; } = conditions;
    public string[]? Joins { get; } = joins;
}


