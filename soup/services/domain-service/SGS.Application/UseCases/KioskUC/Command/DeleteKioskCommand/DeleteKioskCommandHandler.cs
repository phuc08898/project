using SGS.Application.Core.MediatRCustom;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Entities;
using SGS.Domain.Enums;
using SGS.Infrastructure.Common.Data;

namespace SGS.Application.UseCases.KioskUC.Command.DeleteKioskCommand
{
    internal class DeleteKioskCommandHandler(IUnitOfWork unitOfWork) :
    ICommandHandlerV2<DeleteKioskCommand, OkResponse>
    {
        public async Task<ResultV2<OkResponse>> Handle(DeleteKioskCommand request, CancellationToken cancellationToken)
        {
            var kioskRepository = unitOfWork.GetRepository<Kiosk>();
            var deleteKiosk = await kioskRepository.GetByIdAsync(request.Id);
            if (deleteKiosk == null)
                return ResultV2.NotFound(
                    new InvalidOperationException($"Kiosk not found!"));
            //deleteKiosk.Status = CommonStatuses.DELETED;
            deleteKiosk.DeletedOn = DateTimeOffset.UtcNow;
            return ResultV2.Success();

        }
    }
}
