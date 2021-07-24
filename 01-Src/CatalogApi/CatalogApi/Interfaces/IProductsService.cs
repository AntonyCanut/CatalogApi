using CatalogApi.Models;
using System.Collections.Generic;

namespace CatalogApi.Interfaces
{
    public interface IProductsService
    {
        public IEnumerable<Product> GetAllProducts();

        public bool InsertProduct(Product product);
    }
}