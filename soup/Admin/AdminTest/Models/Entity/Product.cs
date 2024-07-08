namespace AdminTest.Models.Entity
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? ImgUrl { get; set; }
        public string? CategoryId { get; set; }
        public Category? Category { get; set; } 
        public int? Quantity { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Search { get; set; } = string.Empty;
        public  long? Price{ get; set; } = 0;
        public List<Variants>? Variants { get; set; }

    }
}
