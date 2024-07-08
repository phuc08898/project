namespace SGS.WebApi.Endpoints.V1;

using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SGS.Webapi.Extensions;
public class UploadsEndpoint : IEndpoint
{
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints, ApiVersionSet version)
    {
        var imageUploadGroup = endpoints
           .MapGroup($"{EndpointExntensions.BASE_ROUTE}/upload")
           .WithDisplayName("Upload")
           .WithApiVersionSet(version)
           .HasApiVersion(1);

        imageUploadGroup.MapPost("/image", UploadImageAsync).WithName("UploadImage").DisableAntiforgery();

        return endpoints;
    }
    private async Task<IResult> UploadImageAsync(
            [FromServices] IWebHostEnvironment env,
            [FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
            return Results.BadRequest("No file uploaded.");

        var uploadPath = Path.Combine(env.ContentRootPath, "Uploads");

        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(uploadPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var relativePath = Path.Combine("Uploads", fileName);
        return Results.Ok(new { Message = relativePath });
    }
}
