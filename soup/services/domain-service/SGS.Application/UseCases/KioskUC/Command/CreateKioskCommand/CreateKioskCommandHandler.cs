using Mapster;
using SGS.Application.Core.MediatRCustom;
using SGS.Application.DTOs;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Entities;
using SGS.Infrastructure.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SGS.Application.UseCases.KioskUC.Command.CreateKioskCommand
{
    public class CreateKioskCommandHandler(IUnitOfWork unitOfWork) :
            ICommandHandlerV2<CreateKioskCommand, OkResponse>
    {
        public Task<ResultV2<OkResponse>> Handle(CreateKioskCommand request, CancellationToken cancellationToken)
        {
            var kioskRepository = unitOfWork.GetRepository<Kiosk>();
            var newKiosk = request.Adapt<Kiosk>();
            kioskRepository.Add(newKiosk);
            return Task.FromResult<ResultV2<OkResponse>>(ResultV2.Success());
        }
    }
}
