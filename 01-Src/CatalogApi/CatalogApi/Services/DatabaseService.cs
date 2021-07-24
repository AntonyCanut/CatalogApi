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

namespace CatalogApi.Services
{
    public class DatabaseService<T> : IDatabaseService<T>
    {
        private readonly IConfiguration _configuration;
        private readonly ILoggerService _logger;

        public DatabaseService(ILoggerService logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;
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
                try
                {
                    return connection.Query<T>(nameStoreProcedure, element, commandType: CommandType.StoredProcedure);
                }
                catch (SqlException ex)
                {
                    _logger.LogError(ex.Message);
                    throw new Exception("Error SQL SERVER");
                }
            }
        }
    }
}
