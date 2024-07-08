using SGS.Application.Core.MediatRCustom;
using SGS.Application.DataTransferObjects;
using SGS.Domain.Common.Primitives;

namespace SGS.Application.UseCases.ProductUC.CreateProductCommand;
    public class CreateProductCommand(CreateProductArg arg) : ICommandV2<OkResponse>
    {
        public string Name { get; set; } = arg.Name;
        public string? CategoryId { get; set; } = arg.CategoryId;
        public int? Quantity { get; set; } = arg.Quantity;
        public string? ImgUrl { get; set; } = arg.ImgUrl;
        public string Slug { get; set; } = arg.Slug;
        public long Price { get; set; } = arg.Price;
        public List<CreateVariantArg> Variants { get; set; } = arg.Variants ?? [];
    }
