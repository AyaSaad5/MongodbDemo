using Microsoft.Extensions.Options;
using MongoDB.Configurations;
using MongoDB.Driver;
using MongoDB.DTOs;
using MongoDB.Entities;

namespace MongoDB.Repositories
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<ProductDetails> productCollection;

        public ProductService(IOptions<ProductDBSettings> productDBSettings)
        {
            //create a new client and connect to server
            var mongoClient = new MongoClient(productDBSettings.Value.ConnectionString);

            var mongoDataBase = mongoClient.GetDatabase(productDBSettings.Value.DatabaseName);

             productCollection = mongoDataBase.GetCollection<ProductDetails>(productDBSettings.Value.ProductCollectionName);

        }
        public async Task AddProductAsync(ProductDetails product)
        {
            await productCollection.InsertOneAsync(product);
        }

        public async Task DeleteProductAsync(string productId)
        {
            await productCollection.DeleteOneAsync(x => x.Id == productId);
        }

        public async Task<ProductDetails> GetProductByIdAsync(string productId)
        {
            return await productCollection.Find(x => x.Id == productId).FirstOrDefaultAsync();
        }

        public async Task<List<ProductDetails>> listProductsAsync()
        {
            return await productCollection.Find(_ => true).ToListAsync();
        }

        public async Task UpdateProductAsync(string productId, updateProductDTO product)
        {
            var filter = Builders<ProductDetails>.Filter.Eq(p => p.Id, productId);

            var update = Builders<ProductDetails>.Update
                .Set(p => p.ProductName, product.ProductName)
                .Set(p => p.ProductDescription, product.ProductDescription)
                .Set(p => p.ProductPrice, product.ProductPrice)
                .Set(p => p.ProductStock, product.ProductStock);

            await productCollection.UpdateOneAsync(filter, update);
        }
    }
}
