using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGS.Application.DataTransferObjects
{
    public class CreatePriceListArg
    {
        public string Name { get; set; } = string.Empty;
        public DateTimeOffset BeginDay { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset EndDay { get; set; } = DateTimeOffset.MaxValue;
        public ICollection<CreatePriceListDetailArg>? PriceListDetailArgs { get; set; }
    }
    public class UpdatePriceListArg
    {
        public string Name { get; set; } = string.Empty;
        public DateTimeOffset BeginDay { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset EndDay { get; set; } = DateTimeOffset.MaxValue;
        public DateTimeOffset ModifineOn { get; set; } = DateTimeOffset.UtcNow;
        public ICollection<UpdatePriceListDetailArg> UpdatePriceListDetailArgs { get; set; } = [];
    }
    public class DeletePriceListArg
    {
        public string Status { get; set; } = string.Empty;
        public DateTimeOffset DeleteOn { get; set; } = DateTimeOffset.UtcNow;
    }
}
