using Mapster;
using SGS.Application.Core.MediatRCustom;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Entities;
using SGS.Infrastructure.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGS.Application.UseCases.KioskUC.Command.UpdateKioskCommand
{
    public class UpdateKioskCommandHandler(
        IUnitOfWork unitOfWork) : ICommandHandlerV2<UpdateKioskCommand, OkResponse>
    {
        public async Task<ResultV2<OkResponse>> Handle(UpdateKioskCommand request, CancellationToken cancellationToken)
        {
            var kioskRepository = unitOfWork.GetRepository<Kiosk>();
            var newKiosk = request.Adapt<Kiosk>();
            var kiosk = await kioskRepository.GetByIdAsync(request.Id);
            if (kiosk == null)
                return ResultV2.NotFound(
                    new InvalidOperationException($"Kiosk not found!"));
            kiosk.Name = request.Name ?? kiosk.Name;
            kiosk.Address = request.Address ?? kiosk.Address;
            kiosk.Phonenumber = request.Phonenumber ?? kiosk.Phonenumber;
            return ResultV2.Success();
        }
    }
}
