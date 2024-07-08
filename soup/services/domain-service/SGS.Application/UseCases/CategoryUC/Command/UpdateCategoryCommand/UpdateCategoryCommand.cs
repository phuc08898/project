
using MediatR;
using SGS.Application.Core.MediatRCustom;
using SGS.Application.DataTransferObjects;
using SGS.Application.DTOs;
using SGS.Application.UseCases.CategoryUC.Command.CreateCategoryCommand;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Entities;
using SGS.Infrastructure.Common.Data;

namespace SGS.Application.UseCases.CategoryUC.Command.UpdateCategoryCommand;

public class UpdateCategoryCommand(string id, UpdateCategoryNameArg arg) : ICommandV2<OkResponse>
{
        public string Id { get; set; } = id;
        public UpdateCategoryNameArg Arg = arg;
        
}


