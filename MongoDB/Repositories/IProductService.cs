using MongoDB.DTOs;
using MongoDB.Entities;

namespace MongoDB.Repositories
{
    public interface IProductService
    {
        Task<List<ProductDetails>> listProductsAsync();
        Task<ProductDetails> GetProductByIdAsync(string productId);
        Task AddProductAsync(ProductDetails product);
        Task UpdateProductAsync(string productId, updateProductDTO product);
        Task DeleteProductAsync(string productId);

    }
}
