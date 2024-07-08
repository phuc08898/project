using SGS.Application.Core.MediatRCustom;
using SGS.Domain.Common.Primitives;
using SGS.Infrastructure.Common.Data;
using SGS.Domain.Entities;
using FluentValidation.Internal;
using SGS.Domain.Enums;
using SGS.Domain.Common.Utils;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace SGS.Application.UseCases.ProductUC.Command.UpdateProductCommand
{
    public class UpdateProductCommandHandler(
        IUnitOfWork unitOfWork) : ICommandHandlerV2<UpdateProductCommand, OkResponse>
    {
        public async Task<ResultV2<OkResponse>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productRepository = unitOfWork.GetRepository<Product>();
            var variantRepository = unitOfWork.GetRepository<Variant>();
            var product = await productRepository.GetByIdAsync(request.Id);
            if (product == null)
                return ResultV2.NotFound(
                    new InvalidOperationException($"Product {request.Id} not found!"));
            product.Name = request.Name;
            product.CategoryId = request.CategoryId;
            product.Quantity = request.Quantity;
            product.Slug = request.Slug;
            product.MakeSearch();
            //got all variant in db with product id
            var existingVariants = await variantRepository.BuildQuery.Where(v => v.ProductId == request.Id).ToListAsync();
            List<string> variantIdsExist = new List<string>();
            foreach (var variantArg in request.UpdateVariants)
            {
                //update old variant 
                if (variantArg.Id != null) 
                {
                    var existingVariant = existingVariants.FirstOrDefault(v => v.Id == variantArg.Id);
                    if (existingVariant != null)
                    {
                        existingVariant.Name = variantArg.Name;
                        existingVariant.Price = variantArg.Price;
                        variantIdsExist.Add(variantArg.Id);
                    }
                }
                //add new variant
                else
                {
                    var newVariant = new Variant
                    {
                        ProductId = product.Id,
                        Name = variantArg.Name,
                        Price = variantArg.Price
                    };
                    variantIdsExist.Add(newVariant.Id);
                    existingVariants.Add(newVariant);
                }
            }
            
            foreach (var variant in existingVariants)
            {
                if (!variantIdsExist.Contains(variant.Id) && variant.Name != "DEFAULT")
                {
                    //variant.Status = CommonStatuses.DELETED;
                    //variant.DeletedOn = DateTimeOffset.UtcNow;
                    variantRepository.Remove(variant);
                }
            }
            product.Variants = existingVariants; 
            return ResultV2.Success();

        }
    }
}