using CatalogApi.Exceptions;
using CatalogApi.Interfaces;
using CatalogApi.Models;
using CatalogApi.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CatalogApiTests
{
    public class ProductsServiceTests
    {
        private IDatabaseService<Product> _databaseService;
        private IProductsService _productsService;

        [SetUp]
        public void Setup()
        {
            _databaseService = Mock.Of<IDatabaseService<Product>>();
            _productsService = new ProductsService(_databaseService);
        }

        [Test]
        public void Should_Return_Empty_Table_When_Empty_From_Products_Services()
        {
            Assert.AreEqual(0, _productsService.GetAllProducts().Count());
        }

        [Test]
        public void Should_Return_A_Data_When_Database_Contains_Only_One_Value()
        {
            Mock.Get(_databaseService).Setup(m => m.RequestDatabase(ProductRequest.GetAllProducts.ToString())).Returns(new List<Product>()
            {
                new Product()
            });
            Assert.AreEqual(1, _productsService.GetAllProducts().Count());
        }

        [Test]
        public void Should_Return_Multiple_Data_When_The_Database_Contains_Multiple_Values()
        {
            Mock.Get(_databaseService).Setup(m => m.RequestDatabase(ProductRequest.GetAllProducts.ToString())).Returns(new List<Product>()
            {
                new Product(),
                new Product(),
                new Product()
            });
            Assert.AreEqual(3, _productsService.GetAllProducts().Count());
        }

        [Test]
        public void Should_Be_Able_To_Add_Data_To_The_Database()
        {
            var product = new Product()
            {
                Code = "UT01",
                Name = "Unit Test 01",
                StartDate = System.DateTime.Now,
                EndDate = System.DateTime.Now.AddDays(5)
            };
            Assert.IsTrue(_productsService.InsertProduct(product));
        }

        [Test]
        public void Shouldnt_Be_Able_To_Add_Data_To_The_Database_Without_Code()
        {
            var product = new Product()
            {
                Code = "",
                Name = "Unit Test 01",
                StartDate = System.DateTime.Now,
                EndDate = System.DateTime.Now.AddDays(5)
            };
            Assert.Throws<ProductException>(() => _productsService.InsertProduct(product));
        }

        [Test]
        public void Shouldnt_Be_Able_To_Add_Data_To_The_Database_Without_Name()
        {
            var product = new Product()
            {
                Code = "UT01",
                Name = "",
                StartDate = System.DateTime.Now,
                EndDate = System.DateTime.Now.AddDays(5)
            };
            Assert.Throws<ProductException>(() => _productsService.InsertProduct(product));
        }

        [Test]
        public void Shouldnt_Be_Able_To_Add_Multiple_Data_With_The_Same_Code()
        {
            Mock.Get(_databaseService).Setup(m => m.RequestDatabase(ProductRequest.GetProduct.ToString())).Returns(new List<Product>()
            {
                new Product()
                {
                    Code = "UT01"
                }
            });
            var product = new Product()
            {
                Code = "UT01",
                Name = "",
                StartDate = System.DateTime.Now,
                EndDate = System.DateTime.Now.AddDays(5)
            };
            Assert.Throws<ProductException>(() => _productsService.InsertProduct(product));
        }

        [Test]
        public void Shouldnt_Be_Able_To_Add_Data_With_EndDate_Before_StartDate()
        {
            var product = new Product()
            {
                Code = "UT01",
                Name = "",
                StartDate = System.DateTime.Now,
                EndDate = System.DateTime.Now.AddDays(-5)
            };
            Assert.Throws<ProductException>(() => _productsService.InsertProduct(product));
        }
    }
}