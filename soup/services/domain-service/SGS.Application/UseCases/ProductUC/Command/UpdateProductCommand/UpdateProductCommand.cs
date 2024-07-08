using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using SGS.Application.Core.MediatRCustom;
using SGS.Application.DataTransferObjects;
using SGS.Domain.Common.Primitives;

namespace SGS.Application.UseCases.ProductUC.Command.UpdateProductCommand;

public class UpdateProductCommand(string id, UpdateProductArg arg) : ICommandV2<OkResponse>
{
    public string Id { get; set; } = id;
    public string Name { get; set; } = arg.Name;
    public string CategoryId { get; set; } = arg.CategoryId;
    public int? Quantity { get; set; } = arg.Quantity;
    public string Slug { get; set; } = arg.Slug;

//        public List<CreateVariantArg> CreateVariants { get; set; } = arg.VariantsCreate ?? [];
    public List<UpdateVariantArg> UpdateVariants { get; set; } = arg.VariantsUpdate ?? [];

    /* public Task<ValidationResult> ValidateAsync(CancellationToken cancellationToken = default) 
     {
         return Task.FromResult(new ValidationResult());
     }*/
}