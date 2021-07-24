using CatalogApi.Models;
using System.Collections.Generic;

namespace CatalogApi.Interfaces
{
    public interface IDatabaseService<T>
    {
        public IEnumerable<T> RequestDatabase(string nameStoreProcedure);
        public IEnumerable<T> RequestDatabase(string nameStoreProcedure, T element);
    }
}