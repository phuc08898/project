using Microsoft.EntityFrameworkCore;
using SGS.Application.Core.MediatRCustom;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Entities;
using SGS.Domain.Enums;
using SGS.Infrastructure.Common.Data;


namespace SGS.Application.UseCases.ProductUC.Command;

public class DeleteProductCommandHandler(IUnitOfWork unitOfWork) :
    ICommandHandlerV2<DeleteProductCommand, OkResponse>
{
    public async Task<ResultV2<OkResponse>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var productRepository = unitOfWork.GetRepository<Product>();
        var deleteProduct = await productRepository.GetByIdAsync(request.Id);
        if (deleteProduct == null)
            return ResultV2.NotFound(
                new InvalidOperationException($"Product not found!"));
        productRepository.Remove(deleteProduct);
        return ResultV2.Success();
    }

}
