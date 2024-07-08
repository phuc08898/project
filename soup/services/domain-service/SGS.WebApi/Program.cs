using SGS.Webapi.Configurations;
using SGS.Infrastructure;
using SGS.Application;
using SGS.Webapi.Middlewares;
using Asp.Versioning.Builder;
using Asp.Versioning;
using SGS.Webapi.Extensions;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();

builder.Services.AddLogging(builder => builder.AddConsole());

builder.Services.AddControllers();

builder.Services.AddSignalR();

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfigure();
builder.Services.AddVersionApi();
builder.Services.AddCorsConfig();
builder.Services.AddJWTConfig(builder.Configuration);
builder.Services.RegisterEndpoints();
//builder.Services.AddAntiforgery();
var app = builder.Build();

ApiVersionSet versionSet = app.NewApiVersionSet()
    .HasApiVersion(ApiVersion.Default)
    .ReportApiVersions()
    .Build();

app.MapEndpoints(versionSet);

if (true) //(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CustomExceptionHandler>();
var uploadPath = Path.Combine(builder.Environment.ContentRootPath, "Uploads");
if (!Directory.Exists(uploadPath))
{
    Directory.CreateDirectory(uploadPath);
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(uploadPath),
    RequestPath = "/uploads"
});
app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();
//app.UseAntiforgery();
app.MapControllers();


app.Run();

