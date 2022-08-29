using API.Products.Models;
using MongoDB.Driver;

namespace API.Products.Infra
{
    public interface IMongoContext
    {
        IMongoCollection<Product> CollectionDocuments { get; }
    }
}
