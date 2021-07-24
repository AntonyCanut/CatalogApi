using CatalogApi.Interfaces;
using CatalogApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CatalogApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductsService _productsService;

        public ProductsController(ILogger<ProductsController> logger, IProductsService productsService)
        {
            _logger = logger;
            _productsService = productsService;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _productsService.GetAllProducts();
        }

        [HttpPost]
        public HttpResponseMessage Insert(Product product)
        {
            if (_productsService.InsertProduct(product))
            {
                return new HttpResponseMessage(HttpStatusCode.Created)
                {
                    Content = new StringContent(string.Format("product with Code = {0} created", product.Code))
                };
            }

            return new HttpResponseMessage(HttpStatusCode.NotAcceptable)
            {
                Content = new StringContent(string.Format("Unknow problem with product with Code = {0}", product.Code)),
                ReasonPhrase = "Unknow Problem"
            };
        }
    }
}
