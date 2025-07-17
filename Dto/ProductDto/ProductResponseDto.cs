namespace Saas_Sexshop.Dto.ProductDto
{
    public class ProductResponseDto{
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public string? ProductDescription { get; set; }
        public string? ImageUrl { get; set; }
        public int CatalogId { get; set; }
    }
}
