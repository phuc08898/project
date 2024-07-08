
using Mapster;
using SGS.Application.Core.MediatRCustom;

using SGS.Domain.Common.Primitives;
using SGS.Domain.Entities;
using SGS.Domain.Enums;
using SGS.Infrastructure.Common.Data;

namespace SGS.Application.UseCases.CategoryUC.Command.CreateCategoryCommand
{
    public class CreateCategoryCommandHandler(IUnitOfWork unitOfWork) :
        ICommandHandlerV2<CreateCategoryCommand, OkResponse>
    {
        public Task<ResultV2<OkResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {

            var categoryRepository = unitOfWork.GetRepository<Category>();
            var newCategory = request.Adapt<Category>();
            newCategory.MakeSearch();
            if (newCategory == null)
                return Task.FromResult<ResultV2<OkResponse>>(ResultV2.NotFound(
                    new InvalidOperationException($"Add Category fail!")));
            categoryRepository.Add(newCategory);
            return Task.FromResult<ResultV2<OkResponse>>(ResultV2.Success());
        }
    }
}
