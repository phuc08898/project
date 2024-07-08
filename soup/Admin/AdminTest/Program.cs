using Radzen;
using AdminTest.Components;
using AdminTest.Components.Pages.CategoryPages;
using AdminTest.Components.Pages.ProductsPages;

namespace AdminTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            // Add services to the container.
            builder.Services.AddRazorComponents().AddInteractiveServerComponents();
            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5005") });
            builder.Services.AddScoped<DialogService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddTransient<DialogService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddRadzenComponents();

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();
            app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
            app.Run();
        }
    }
}
