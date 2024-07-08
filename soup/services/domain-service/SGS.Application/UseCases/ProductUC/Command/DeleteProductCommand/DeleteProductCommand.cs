﻿using SGS.Application.Core.MediatRCustom;
using SGS.Domain.Common.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGS.Application.UseCases.ProductUC.Command;

public class DeleteProductCommand(string id) : ICommandV2<OkResponse>
{
    public string Id { get; set; } = id;
}
