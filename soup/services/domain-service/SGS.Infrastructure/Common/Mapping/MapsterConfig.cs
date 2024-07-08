using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace SGS.Infrastructure.Core.Mapping
{
    public static class MapsterConfig
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            // TypeAdapterConfig<User, UserDTO>.NewConfig()
            //     .Map(dest => dest.DepartmentName, src => src.Department == null ? string.Empty : src.Department.Name);

            // TypeAdapterConfig<User, EmployeeOfDepartmentDTO>.NewConfig()
            //     .Map(dest => dest.PositionName, e => e.IdNavigation != null ? e.IdNavigation.Position!.Name : string.Empty)
            //     .Map(dest => dest.Name, e => e.IdNavigation == null ? string.Empty : $"{e.IdNavigation!.LastName} {e.IdNavigation!.FirstName} ");

            // TypeAdapterConfig<Department, DepartmentUserDTO>.NewConfig()
            //     .Map(dest => dest.Employees, src => src.Users.Adapt<List<EmployeeOfDepartmentDTO>>());

            // TypeAdapterConfig<Document, CreateDocCommandArgs>.NewConfig()
            //     .Ignore(dest => dest.SubFiles!);
            // TypeAdapterConfig<CreateSubFilesArgs, DocumentSubject>.NewConfig()
            //     .Map(dest => dest.DocumentId, src => MapContext.Current != null ? MapContext.Current.Parameters["DocumentId"] : 0);
        }
    }
}
