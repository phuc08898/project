using SGS.Application.Core.MediatRCustom;
using SGS.Application.DataTransferObjects;
using SGS.Domain.Common.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGS.Application.UseCases.KioskUC.Command.CreateKioskCommand
{
    public  class CreateKioskCommand(CreateKioskArg arg) : ICommandV2<OkResponse>
    {
        public string Name { get; set; } = arg.Name;
        public string Address { get; set; } = arg.Address;
        public string Phonenumber { get; set; } = arg.Phonenumber;
    }
}
