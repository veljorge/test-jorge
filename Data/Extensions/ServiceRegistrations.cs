using Data.Configurations;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Extensions
{
    public static class ServiceRegistrations
    {
        private static string _clientId;
        private static string _clientSecret;
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("Secret", GetKeyVaultSecrets(configuration)));
            var configurationSecrets = new ConfigurationBuilder().AddInMemoryCollection(list).Build();

            services.Configure<ConnectionStrings>(configuration.GetSection(nameof(ConnectionStrings)));
            services.Configure<Secrets>(configurationSecrets);

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        private static string GetKeyVaultSecrets(IConfiguration config)
        {
            var keyVaultUri = config.GetSection("KeyVault").GetSection("KeyVaultUri").Value;
            var secretName = config.GetSection("KeyVault").GetSection("SecretName").Value; ;
            var secretIdentifier = config.GetSection("KeyVault").GetSection("SecretIdentifier").Value;
            _clientId = config.GetSection("KeyVault").GetSection("ClientId").Value;
            _clientSecret = config.GetSection("KeyVault").GetSection("ClientSecret").Value;

            var azureServiceProvider = new AzureServiceTokenProvider();
            var keyVaultClient = new KeyVaultClient(
                new KeyVaultClient.AuthenticationCallback(azureServiceProvider.KeyVaultTokenCallback)
                );

            //var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(GetToken));

            var azureSecret = keyVaultClient.GetSecretAsync(secretIdentifier).Result;
            //var azureSecret = keyVaultClient.GetSecretAsync(keyVaultUri, secretName).Result;
            return azureSecret?.Value;
        }

        private static async Task<string> GetToken(string authority, string resource, string scope)
        {
            var authContext = new AuthenticationContext(authority);
            var clientCred = new ClientCredential(_clientId, _clientSecret);

            var result = await authContext.AcquireTokenAsync(resource, clientCred);

            if (result == null)
            {
                throw new System.Exception("error");
            }
            return result.AccessToken;
        }
    }
}
