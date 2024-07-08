using SGS.Application.Core.MediatRCustom;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Entities;

namespace SGS.Application.UseCases.KioskUC.Command.AutoCreateKioskCommand
{
    public class AutoCreateKioskCommand() : ICommandV2<OkResponse>
    {
        public string Name { get; set; } = "Kiosk";
        public string Address { get; set; } = "123";
        public string Phonenumber { get; set; } = "456";
    }
}
