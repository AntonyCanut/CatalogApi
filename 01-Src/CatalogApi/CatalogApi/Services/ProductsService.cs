using CatalogApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IDatabaseService _databaseService;

        public ProductsService(IDatabaseService databaseService)
        {
            this._databaseService = databaseService;
        }
    }
}
