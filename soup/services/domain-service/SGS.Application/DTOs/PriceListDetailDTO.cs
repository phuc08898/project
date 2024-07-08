using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGS.Application.DataTransferObjects;

public class CreatePriceListDetailArg
{
    public string VariantId {  get; set; } = string.Empty;
    public string ProductId { get; set; } = string.Empty;
    public int DiscountPercent { get; set; }
    public long Price { get; set; }
}

public class UpdatePriceListDetailArg
{
    public string? Id {  get; set; }
    public string VariantId { get; set; } = string.Empty;
    public string ProductId { get; set; } = string.Empty;
    public int DiscountPercent { get; set; }
    public long Price { get; set; }
}