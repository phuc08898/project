
using SGS.Application.Core.MediatRCustom;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Entities;
namespace SGS.Application.UseCases.OrderUC.Queries.GetOrder;

public class GetOrderQuery(string id,
    Dictionary<string, Dictionary<string, string>>? conditions,
    string[]? joins) : IQuery<ResultV2<OkResponse<Order>>>
{
    public string Id { get; set; } = id;
    public Dictionary<string, Dictionary<string, string>>? Conditions { get; } = conditions;
    public string[]? Joins { get; } = joins;
}