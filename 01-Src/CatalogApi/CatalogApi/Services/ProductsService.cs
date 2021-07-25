using CatalogApi.Exceptions;
using CatalogApi.Interfaces;
using CatalogApi.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Services
{
    public enum ProductRequest { GetAllProducts, GetProduct, insertProduct }

    public class ProductsService : IProductsService
    {
        private readonly IDatabaseService<Product> _databaseService;

        public ProductsService(IDatabaseService<Product> databaseService)
        {
            this._databaseService = databaseService;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _databaseService.RequestDatabase(ProductRequest.GetAllProducts.ToString());
        }

        public bool InsertProduct(Product product)
        {
            if (string.IsNullOrEmpty(product.Code) || string.IsNullOrEmpty(product.Name))
                throw new ProductException(ProductErrorType.SomethingIsNull, product);

            if (product.StartDate.CompareTo(product.EndDate) >= 0)
                throw new ProductException(ProductErrorType.StartDateMustBeforeEndDate, product);

            if (_databaseService.RequestDatabase(ProductRequest.GetProduct.ToString(), product).Any())
                throw new ProductException(ProductErrorType.CodeNoUnique, product);

            try
            {
                _databaseService.RequestDatabase(ProductRequest.insertProduct.ToString(), product);
            }
            catch (SqlException ex)
            {
                throw new ProductException(ProductErrorType.SQLServer, product, ex.StackTrace);
            }
            return true;
        }
    }
}
