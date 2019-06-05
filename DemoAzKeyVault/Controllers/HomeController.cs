using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DemoAzKeyVault.Controllers
{
    public class HomeController : Controller
    {
        private const string AZ_KEYVAULT_SECRET_URL = "https://AZ_KV.vault.azure.net/secrets/DemoSecret01";
        private const string AZ_KEYVAULT_URL = "https://AZ_KV.vault.azure.net";

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public async Task<ActionResult> Contact()
        {
            ViewBag.Message = "Tell Nobody: '" + (await GetSecretAsync()) + "'";

            var cert = await GetCertificateAsync();
            ViewBag.CertificateThumbPrint = "'" + Convert.ToBase64String(cert.X509Thumbprint) + "'";

            return View();
        }

        private async Task<string> GetSecretAsync()
        {
            /* The next four lines of code show you how to use AppAuthentication library to fetch secrets from your key vault */
            AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();
            KeyVaultClient keyVaultClient = new KeyVaultClient(
                                                new KeyVaultClient.AuthenticationCallback(
                                                    azureServiceTokenProvider.KeyVaultTokenCallback));

            var secret = await keyVaultClient
                .GetSecretAsync(AZ_KEYVAULT_SECRET_URL)
                .ConfigureAwait(false);

            return secret.Value;
        }

        private async Task<CertificateBundle> GetCertificateAsync()
        {
            /* The next four lines of code show you how to use AppAuthentication library to fetch secrets from your key vault */
            AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();
            KeyVaultClient keyVaultClient = new KeyVaultClient(
                                                new KeyVaultClient.AuthenticationCallback(
                                                    azureServiceTokenProvider.KeyVaultTokenCallback));

            return await keyVaultClient
                //.GetSecretAsync(AZ_KEYVAULT_SECRET_URL)
                .GetCertificateAsync(
                    AZ_KEYVAULT_URL,
                    "democert01",
                    "7af4d5b98ae743d4885ca20afcd339b1")
                .ConfigureAwait(false);
        }
    }
}