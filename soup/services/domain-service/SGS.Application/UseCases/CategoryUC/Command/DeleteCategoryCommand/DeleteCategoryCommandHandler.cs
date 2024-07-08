using SGS.Application.Core.MediatRCustom;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Entities;
using SGS.Domain.Enums;
using SGS.Infrastructure.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGS.Application.UseCases.CategoryUC.Command.DeleteCategoryCommand
{
    public class DeleteCategoryCommandHandler(IUnitOfWork unitOfWork) :
        ICommandHandlerV2<DeleteCategoryCommand, OkResponse>
    {
        public async Task<ResultV2<OkResponse>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryRepository = unitOfWork.GetRepository<Category>();
            var DeleteCategory = await categoryRepository.GetByIdAsync(request.Id);
            if (DeleteCategory == null) 
                return ResultV2.NotFound(
                    new InvalidOperationException($"Category not found!"));
            categoryRepository.Remove(DeleteCategory);
            return ResultV2.Success();
        }
        
    }
}
