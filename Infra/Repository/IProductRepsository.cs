using API.Products.Models;

namespace API.Products.Infra.Repository
{
    public interface IProductRepsository
    {
        Task<Product> CreateProduct(Product product);
        Task<IEnumerable<Product>> GetProducts();
    }
}
