using CatalogApi.Exceptions;
using CatalogApi.Interfaces;
using CatalogApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
        private readonly ILoggerService _logger;
        private readonly IProductsService _productsService;

        public ProductsController(ILoggerService logger, IProductsService productsService)
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
            try
            {
                if (_productsService.InsertProduct(product))
                {
                    return new HttpResponseMessage(HttpStatusCode.Created)
                    {
                        Content = new StringContent(string.Format("product with Code = {0} created", product.Code))
                    };
                }
            }
            catch (ProductException ex)
            {
                _logger.LogError($"{ex.Message} with the following product : \nCode: {product.Code}, {product.Name}," +
                    $"{product.StartDate}, {product.EndDate}");
                return new HttpResponseMessage(HttpStatusCode.Conflict)
                {
                    Content = new StringContent(ex.Message),
                    ReasonPhrase = ex.Message
                };
            }

            _logger.LogError($"Unknow error with the following product : \nCode: {product.Code}, {product.Name}," +
                    $"{product.StartDate}, {product.EndDate}");
            return new HttpResponseMessage(HttpStatusCode.NotAcceptable)
            {
                Content = new StringContent(string.Format("Unknow problem with product with Code = {0}", product.Code)),
                ReasonPhrase = "Unknow Problem"
            };
        }
    }
}
