using DependencyInjection.Models;

namespace DependencyInjection.Services
{
    public class ProductService : IProductService
    {
        private List<Product> products = new List<Product>
        {
            new Product { Id = 1 , Name = "Book", Price = 20.5m},
            new Product { Id = 2 , Name = "Computer", Price = 999.99m},
            new Product { Id = 3 , Name = "Keyboard", Price = 39.99m},
            new Product { Id = 4 , Name = "Mouse", Price = 19.99m},
        };

        public ProductService()
        {

        }

        public List<Product> GetProducts()
        {
            return products;
        }

        public Product GetProductById(int id)
        {
            return products.Where(c => c.Id == id).FirstOrDefault()!;

        }

        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        public void RemoveProduct(Product product)
        {
            products.Remove(product);
        }

    }

}
