using FT_ProviderSys.DTOs;
using FT_ProviderSys.Models;
using FT_ProviderSys.Services.Interfaces;

namespace FT_ProviderSys.Services
{
    public class ProductService : IProductService
    {
        public static List<Product> productSample = new List<Product>();
        private static int nextId = 1;

        public List<Product> GetAll() => productSample;

        public Product? GetById(int id)
        {
            return productSample.FirstOrDefault(x => x.ProductId == id);
        }

        public int Create(ProductCreateRequestDTO inputProduct)
        {
            var newProduct = new Product(inputProduct.Code, inputProduct.ProductName,
                                         inputProduct.Description, inputProduct.Price);
            
            newProduct.ProductId = nextId++;
            productSample.Add(newProduct);
            return newProduct.ProductId;
        }

        public bool Update(ProductUpdateRequestDTO inputProduct)
        {
            var existingProduct = productSample.Find(x => x.ProductId == inputProduct.ProductId);
            if (existingProduct == null) return false;

            var index = productSample.IndexOf(existingProduct);

            existingProduct.ProductName = inputProduct.ProductName;
            existingProduct.Code = inputProduct.Code;
            existingProduct.Description = inputProduct.Description;
            existingProduct.Price = inputProduct.Price;

            productSample[index] = existingProduct;

            return true;
        }

        public bool Delete(int id)
        {
            var existingProduct = productSample.Find(x => x.ProductId == id);
            if (existingProduct == null) return false;

            productSample.Remove(existingProduct);

            return true;
        }
    }
}
