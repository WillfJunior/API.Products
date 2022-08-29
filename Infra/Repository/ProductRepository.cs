using API.Products.Models;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Driver;
using System.Text;
using Newtonsoft;
using Newtonsoft.Json;

namespace API.Products.Infra.Repository
{
    public class ProductRepository : IProductRepsository
    {
        private readonly IMongoContext _mongoContext;
        private readonly IDistributedCache _redisContext;

        public ProductRepository(IMongoContext mongoContext, IDistributedCache redisContext)
        {
            _mongoContext = mongoContext;
            _redisContext = redisContext;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            await _mongoContext.CollectionDocuments.InsertOneAsync(product);

            return product;
        }


        public async Task<IEnumerable<Product>> GetProducts()
        {
            var productsCache = "products";
            string serialize;
            var productList = new List<Product>();

            var redisListProdeucts = await _redisContext.GetAsync(productsCache);

            if (redisListProdeucts != null)
            {
                serialize = Encoding.UTF8.GetString(redisListProdeucts);
                return productList = JsonConvert.DeserializeObject<List<Product>>(serialize);
            }
            else
            {
                productList = await _mongoContext.CollectionDocuments.Find(p => true).ToListAsync();
                serialize = JsonConvert.SerializeObject(productList);
                redisListProdeucts = Encoding.UTF8.GetBytes(serialize);

                var opt = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddMinutes(5))
                    .SetSlidingExpiration(TimeSpan.FromSeconds(120));

                await _redisContext.SetAsync(productsCache, redisListProdeucts, opt);
            }
            return productList;
        }


    }
}
