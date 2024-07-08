

using SGS.Domain.Entities;

namespace SGS.Application.DataTransferObjects;

public class CreateProductArg
{
    public string Name { get; set; } = string.Empty;
    public string CategoryId { get; set; } = string.Empty;
    public int? Quantity { get; set; }
    public string? ImgUrl { get; set; }
    public string Slug { get; set; } = string.Empty;
    public long Price { get; set; }
    public List<CreateVariantArg>? Variants{ get; set; }
}
public class UpdateProductArg
{
    public string Name { get; set; } = string.Empty;
    public string CategoryId { get; set; } = string.Empty;
    
    public int? Quantity { get; set; }
    public string? ImgUrl { get; set; }
    public string Slug { get; set;} = string.Empty;
    public List<UpdateVariantArg>? VariantsUpdate { get; set; }
//    public List<CreateVariantArg>? VariantsCreate { get; set; }

}