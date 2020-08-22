using Dapper;
using Data.Configurations;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly string _dbConnectionString;
        private readonly string _secretToShow;
        public UnitOfWork(IOptions<ConnectionStrings> connectionStrings, IOptions<Secrets> secrets)
        {
            _dbConnectionString = connectionStrings.Value.DB;
            _secretToShow = secrets.Value.Secret;
        }

        public async Task<IEnumerable<T>> GetAll<T>()
        {
            try
            {
                using var connection = await GetConnection();
                return await connection.GetListAsync<T>();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public string GetSecret()
        {
            return _secretToShow;
        }

        public async Task<T> Get<T>(Guid id)
        {
            using var connection = await GetConnection();
            return await connection.GetAsync<T>(id);
        }

        private async Task<IDbConnection> GetConnection()
        {
            //var azureServiceToken = new AzureServiceTokenProvider("RunAs=Developer;DeveloperTool=AzureCLI");

            AzureServiceTokenProvider azureServiceToken;

#if DEBUG
            azureServiceToken = new AzureServiceTokenProvider("RunAs=Developer;DeveloperTool=AzureCLI");
#else
            azureServiceToken = new AzureServiceTokenProvider();
#endif


            var token = await azureServiceToken.GetAccessTokenAsync("https://database.windows.net/");

            var connection = new SqlConnection()
            {
                AccessToken = token,
                ConnectionString = _dbConnectionString
            };

            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.SQLServer);

            connection.Open();
            return connection;
        }

    }
}
