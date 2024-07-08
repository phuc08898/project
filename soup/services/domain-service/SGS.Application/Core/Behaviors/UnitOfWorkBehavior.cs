using MediatR;
using SGS.Infrastructure.Common.Data;

namespace SGS.Application.Core.Behaviors;

public class UnitOfWorkBehavior<TTRequest, TTResponse>(IUnitOfWork unitOfWork)
        : IPipelineBehavior<TTRequest, TTResponse>
        where TTRequest : notnull
        where TTResponse : notnull
{
    public async Task<TTResponse> Handle(TTRequest request,
        RequestHandlerDelegate<TTResponse> next,
        CancellationToken cancellationToken)
    {
        var response = await next();
        var inTrans = unitOfWork.IsInTransaction;
        var isSaved = unitOfWork.IsSaved;
        // check if not successfull response rollback if in transaction
        var PropIsSuccess = (response as object).GetType().GetProperty("IsSuccess");
        if (PropIsSuccess is not null && !(bool)PropIsSuccess.GetValue(response)! && inTrans)
        {
            await unitOfWork.RollbackAsync(cancellationToken);
            return response;
        }

        try
        {
            if (!isSaved)
            {
                if (IsCommand)
                    await unitOfWork.SaveChangeAsync(cancellationToken);
            }
            else
            {
                unitOfWork.SetIsSave(false);
            }
        }
        catch
        {
            if (inTrans)
                await unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }

        if (inTrans)
            await unitOfWork.CommitAsync(cancellationToken);

        return response;
    }

    private static bool IsCommand => typeof(TTRequest).Name.EndsWith("Command");

}