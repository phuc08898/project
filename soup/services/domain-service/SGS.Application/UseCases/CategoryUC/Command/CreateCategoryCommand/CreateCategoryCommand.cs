using SGS.Application.Core.MediatRCustom;
using SGS.Application.DTOs;
using SGS.Domain.Common.Primitives;



namespace SGS.Application.UseCases.CategoryUC.Command.CreateCategoryCommand;

public class CreateCategoryCommand(CreateCategoryArg arg) : ICommandV2<OkResponse>
{
    public string? ParentId { get; set; } = arg.ParentId;
    public string Name { get; set; } = arg.Name;
    public string Slug { get; set; } = arg.Slug;
}


