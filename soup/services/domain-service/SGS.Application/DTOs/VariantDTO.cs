using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGS.Application.DataTransferObjects;

public class CreateVariantArg
{
    public string Name { get; set; } = string.Empty;
    public long Price { get; set; }
}

public class UpdateVariantArg
{
    public string? Id { get; set; } 
    public string Name { get; set; } = string.Empty;
    public long Price { get; set; }
}
