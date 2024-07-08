using Mapster;
using SGS.Application.Core.MediatRCustom;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Entities;
using SGS.Infrastructure.Common.Data;

namespace SGS.Application.UseCases.KioskUC.Command.AutoCreateKioskCommand
{
    public class AutoCreateKioskCommandHandler(IUnitOfWork unitOfWork) :
        ICommandHandlerV2<AutoCreateKioskCommand, OkResponse>
    {
        public Task<ResultV2<OkResponse>> Handle(AutoCreateKioskCommand request, CancellationToken cancellationToken)
        {
            var kioskRepository = unitOfWork.GetRepository<Kiosk>();
            var newKiosk = request.Adapt<Kiosk>();
            kioskRepository.Add(newKiosk);
            return Task.FromResult<ResultV2<OkResponse>>(ResultV2.Success());
        }
    }
}
