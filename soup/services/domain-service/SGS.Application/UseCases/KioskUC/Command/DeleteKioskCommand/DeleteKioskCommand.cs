using SGS.Application.Core.MediatRCustom;
using SGS.Domain.Common.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGS.Application.UseCases.KioskUC.Command.DeleteKioskCommand
{
    public class DeleteKioskCommand(string id) : ICommandV2<OkResponse>
    {
        public string Id { get; set; } = id;
    }
}
