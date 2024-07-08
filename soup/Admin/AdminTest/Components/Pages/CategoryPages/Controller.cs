
using AdminTest.Models.Entity;
using AdminTest.Models.Enums;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http;


namespace AdminTest.Components.Pages.CategoryPages;

public interface ICategoryService
{
    Task<Category[]> GetAllCategory();
    Task<Category> GetCategory(string id);
    Task<BaseResponse> AddCategory(Category request);


}
public class CategoryService : ICategoryService
{
    private readonly HttpClient _httpClient;
    private Category[]? Categories { get; set; }
    private Category? Category { get; set; }
    string URL = "/api/v1/categories";

    public CategoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Category[]> GetAllCategory()
    {
        var response = await _httpClient.GetAsync(URL);
        response.EnsureSuccessStatusCode();
        var categories = await response.Content.ReadFromJsonAsync<OkPageResponse<Category[]>>();
        Categories = categories.Data ?? [];
        if (Categories.IsNullOrEmpty())
            return null;
        return Categories;
    }
    public async Task<Category> GetCategory(string id)
    {
        var getCategoryUrl = $"{URL}/{id.ToLower()}";
        var response = await _httpClient.GetAsync(getCategoryUrl);
        response.EnsureSuccessStatusCode();
        var CategoryRespone = await response.Content.ReadFromJsonAsync<OkPageResponse<Category>>();
        Category = CategoryRespone.Data ?? null;
        if (Category == null)
            return null;
        return Category;
    }
    public async Task<BaseResponse> AddCategory(Category request)
    {
        try
        {
            request.Slug = request.Name.Replace(" ", "-").ToLower().NonUnicode();
            request.Slug = request.Slug.RemoveSpecialCharacters();
            var response = await _httpClient.PostAsJsonAsync(URL, request);

            if (response.IsSuccessStatusCode)
            {
                return new OkResponse();
            }
            else
            {
                // Handle error response
                var errorMessage = await response.Content.ReadAsStringAsync();
                return new ErrorResponse(errorMessage);
            }
        }
        catch (Exception ex)
        {
            return new ErrorResponse(ex.Message);
        }

    }
}