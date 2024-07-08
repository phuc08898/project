using SGS.Application.Core.MediatRCustom;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Entities;
using SGS.Infrastructure.Common.Data;

namespace SGS.Application.UseCases.CategoryUC.Command.UpdateCategoryCommand;

public class UpdateCategoryCommandHandler(IUnitOfWork unitOfWork) :
        ICommandHandlerV2<UpdateCategoryCommand, OkResponse>
{
    public async Task<ResultV2<OkResponse>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryRepository = unitOfWork.GetRepository<Category>();
        var Updatecategory = await categoryRepository.GetByIdAsync(request.Id);
        if (Updatecategory == null)
            return ResultV2.NotFound(
                new InvalidOperationException($"Category not found!"));
        Updatecategory.Name = request.Arg.Name;
        Updatecategory.Slug = request.Arg.Slug;
        Updatecategory.MakeSearch();
        return ResultV2.Success();
    }
}