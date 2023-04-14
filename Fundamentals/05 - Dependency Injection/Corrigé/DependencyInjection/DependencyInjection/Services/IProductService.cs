using DependencyInjection.Models;

namespace DependencyInjection.Services
{
    public interface IProductService
    {
        void AddProduct(Product product);
        Product GetProductById(int id);
        List<Product> GetProducts();
        void RemoveProduct(Product product);
    }
}