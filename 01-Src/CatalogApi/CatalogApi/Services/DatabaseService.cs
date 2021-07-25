using CatalogApi.Interfaces;
using CatalogApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using CatalogApi.Exceptions;

namespace CatalogApi.Services
{
    public class DatabaseService<T> : IDatabaseService<T>
    {
        private readonly IConfiguration _configuration;

        public DatabaseService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<T> RequestDatabase(string nameStoreProcedure)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                return connection.Query<T>(nameStoreProcedure, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<T> RequestDatabase(string nameStoreProcedure, T element)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                return connection.Query<T>(nameStoreProcedure, element, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
