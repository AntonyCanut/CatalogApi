using CatalogApi.Exceptions;
using CatalogApi.Interfaces;
using CatalogApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Services
{
    public enum ProductEnum { GetAllProducts, GetProduct, insertProduct }

    public class ProductsService : IProductsService
    {
        private readonly IDatabaseService<Product> _databaseService;

        public ProductsService(IDatabaseService<Product> databaseService)
        {
            this._databaseService = databaseService;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _databaseService.RequestDatabase(ProductEnum.GetAllProducts.ToString()) as IEnumerable<Product>;
        }

        public bool InsertProduct(Product product)
        {
            if (string.IsNullOrEmpty(product.Code) || string.IsNullOrEmpty(product.Name))
                throw new ProductException(ProductErrorEnum.SomethingIsNull, product);

            if (product.StartDate.CompareTo(product.EndDate) >= 0)
                throw new ProductException(ProductErrorEnum.StartDateMustBeforeEndDate, product);

            if (_databaseService.RequestDatabase(ProductEnum.GetProduct.ToString(), product).Count() > 0)
                throw new ProductException(ProductErrorEnum.CodeNoUnique, product);

            _databaseService.RequestDatabase(ProductEnum.insertProduct.ToString(), product);
            return true;
        }
    }
}
