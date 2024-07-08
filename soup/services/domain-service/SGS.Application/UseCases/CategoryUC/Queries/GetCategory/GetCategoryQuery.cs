using SGS.Application.Core.MediatRCustom;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Entities;

namespace SGS.Application.UseCases.CategoryUC.Queries.GetCategory;

public class GetCategoryQuery(string id,
    Dictionary<string, Dictionary<string, List<SQueryItem>>>? conditions,
    string[]? joins) : IQuery<ResultV2<OkResponse<Category>>>
{
    public string Id { get; set; } = id;
    public Dictionary<string, Dictionary<string, List<SQueryItem>>>? Conditions { get; } = conditions;
    public string[]? Joins { get; } = joins;
}

