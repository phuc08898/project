using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGS.Application.Core.MediatRCustom;
using SGS.Application.DataTransferObjects;
using SGS.Application.DTOs;
using SGS.Domain.Common.Primitives;

namespace SGS.Application.UseCases.PriceListUC.Command.CreatePriceListCommand;

public class CreatePriceListCommand(CreatePriceListArg arg) : ICommandV2<OkResponse>
{
        public string Name { get; set; } = arg.Name;
        public DateTimeOffset BeginDay { get; set; } = arg.BeginDay;
        public DateTimeOffset EndDay { get; set;} = arg.EndDay;
        public ICollection<CreatePriceListDetailArg> PriceListDetailArgs { get; set; } = arg.PriceListDetailArgs ?? [];
}
