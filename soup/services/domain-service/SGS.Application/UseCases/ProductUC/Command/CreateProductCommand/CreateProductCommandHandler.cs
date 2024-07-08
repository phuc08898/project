using Mapster;
using SGS.Application.Core.MediatRCustom;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Entities;
using SGS.Infrastructure.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGS.Application.UseCases.ProductUC.CreateProductCommand;

public class CreateProductCommandHandler(IUnitOfWork unitOfWork) : ICommandHandlerV2<CreateProductCommand, OkResponse>
{
    public Task<ResultV2<OkResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        //await unitOfWork.BeginTransactionAsync(cancellationToken);
        var productRepository = unitOfWork.GetRepository<Product>();
        var newProduct = request.Adapt<Product>();
        var listVariant = request.Variants
            .Select(variant => new Variant(
                productId: newProduct.Id,
                name: variant.Name,
                price: variant.Price))
            .ToList();
        listVariant.Add(new Variant(productId: newProduct.Id, name: "DEFAULT", price: request.Price));
        newProduct.Variants = listVariant;
        newProduct.MakeSearch();
        productRepository.Add(newProduct);
        return Task.FromResult<ResultV2<OkResponse>>(ResultV2.Success());
    }
}
