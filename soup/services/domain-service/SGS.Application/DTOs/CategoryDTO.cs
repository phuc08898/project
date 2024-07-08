using SGS.Domain.Entities;
using SGS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGS.Application.DTOs
{
    public class CreateCategoryArg
    {
        public string? ParentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
    }
    public record UpdateCategoryNameArg
    {
        public required string Name { get; init; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
    }

}
