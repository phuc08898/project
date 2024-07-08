 
namespace AdminTest.Models.Entity;

public sealed class Category
{
    public string Id { get; set; }
    public string? ParentId { get; set; }
    public Category? Parent { get; set; }
    public string Name { get; set;} = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Search { get; set; } = string.Empty;


}
