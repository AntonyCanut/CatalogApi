using CatalogApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Exceptions
{
    public enum ProductErrorEnum { CodeNoUnique, StartDateMustBeforeEndDate, SomethingIsNull }

    [Serializable]
    public class ProductException : Exception
    {
        private string _message;
        public override string Message
        {
            get { return _message; }
        }

        public ProductException(ProductErrorEnum productErrorEnum, Product product)
        {
            switch (productErrorEnum)
            {
                case ProductErrorEnum.CodeNoUnique:
                    _message = $"{product.Code} Code already exist";
                break;
                case ProductErrorEnum.SomethingIsNull:
                    _message = "Values cannot be null";
                break;
                case ProductErrorEnum.StartDateMustBeforeEndDate:
                    _message = "Start Date cannot be after End Date";
                break;
            }
        }
    }
}
