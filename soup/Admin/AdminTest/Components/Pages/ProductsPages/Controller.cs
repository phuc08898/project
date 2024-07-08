using AdminTest.Models.Entity;
using AdminTest.Models.Enums;
using Azure.Core;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace AdminTest.Components.Pages.ProductsPages;


public interface IProductService
{
    Task<Product[]> GetAllProduct();
    Task<BaseResponse> AddProduct(Product request);
    Task<Product> GetProduct(string id);
    Task<BaseResponse> UploadFiles(MultipartFormDataContent content);
    Task<BaseResponse> DeleteProduct(Product request);
    Task<BaseResponse> UpdateProduct(Product request);

}

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    private Variants variant { get; set; }
    private List<Variants> variants { get; set; }
    private Category[] Categories { get; set; }
    private Product[] Products { get; set; }
    private Product Product { get; set; }
    string URL = "/api/v1/product";

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<Product[]> GetAllProduct()
    {
        string[] joins = ["variants", "categories"];
        string json = JsonBuilder.BuildJson(null, joins, 0, 0);
        var getProductUrl = URL + json;
        var response = await _httpClient.GetAsync(getProductUrl);
        response.EnsureSuccessStatusCode();
        var productrespone = await response.Content.ReadFromJsonAsync<OkPageResponse<Product[]>>();
        var products = productrespone.Data ?? [];
        if (products == null)
            return null;
        return products;
    }
    public async Task<Product> GetProduct(string id)
    {

        var getProductUrl = URL + "/" + id.ToLower();
        string[] joins = ["variants", "categories"];
        string json = JsonBuilder.BuildJson(null, joins, 0, 0);
        getProductUrl = getProductUrl + json;
        var response = await _httpClient.GetAsync(getProductUrl);
        response.EnsureSuccessStatusCode();
        var ProductRespone = await response.Content.ReadFromJsonAsync<OkPageResponse<Product>>() ?? null;
        Product = ProductRespone.Data ?? null;
        if (Product == null)
            return null;
        return Product;
    }
    public async Task<BaseResponse> AddProduct(Product request)
    {
        try
        {
            request.Slug = request.Name.Replace(" ", "-").ToLower().NonUnicode();
            request.Slug = request.Slug.RemoveSpecialCharacters();
            var response = await _httpClient.PostAsJsonAsync("/api/v1/product", request);

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
    public async Task<BaseResponse> UploadFiles(MultipartFormDataContent content)
    {
        if (content != null)
        {
            var response = await _httpClient.PostAsync("/api/v1/upload/image", content);
            if (response.IsSuccessStatusCode)
            {
                var msgResponse = await response.Content.ReadFromJsonAsync<MsgResponse>() ?? null;
                return new OkResponse(msgResponse.Message);
            }
            else
            {
                // Handle error response
                var errorMessage = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                return errorMessage;
            }
        }
        else
        {
            var errorMessage = "Content was null!";
            return new ErrorResponse(errorMessage);
        }
    }
    public async Task<BaseResponse> DeleteProduct(Product request)
    {
        try
        {
            var deleteUrl = $"{URL}/{request.Id.ToLower()}";
            var response = await _httpClient.DeleteAsync(deleteUrl);
            response.EnsureSuccessStatusCode();
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
    public async Task<BaseResponse> UpdateProduct(Product request)
    {
        try
        {
            var updateurl = $"{URL}/{request.Id.ToLower()}";
            var response = await _httpClient.PutAsJsonAsync(updateurl, request);
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
