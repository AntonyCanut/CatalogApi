using CatalogApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Exceptions
{
    public enum ProductErrorType { CodeNoUnique, StartDateMustBeforeEndDate, SomethingIsNull, SQLServer }

    public class ProductException : Exception
    {
        private string _message;
        public override string Message => _message;

        private string _stackTrace;
        public override string StackTrace => _stackTrace;

        public ProductException(ProductErrorType productErrorEnum, Product product, string stackTrace = null)
        {
            switch (productErrorEnum)
            {
                case ProductErrorType.CodeNoUnique:
                    _message = $"{product.Code} Code already exist";
                break;
                case ProductErrorType.SomethingIsNull:
                    _message = "Values cannot be null";
                break;
                case ProductErrorType.StartDateMustBeforeEndDate:
                    _message = "Start Date cannot be after End Date";
                break;
                case ProductErrorType.SQLServer:
                    _message = "Sql Server encounter an error not handle";
                    _stackTrace = stackTrace;
                    break;
            }
        }
    }
}
