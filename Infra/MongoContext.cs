using API.Products.Models;
using MongoDB.Driver;

namespace API.Products.Infra
{
    public class MongoContext : IMongoContext
    {
        public MongoContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionStrings"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            CollectionDocuments = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
        }
        public IMongoCollection<Product> CollectionDocuments { get; }
    }
}
