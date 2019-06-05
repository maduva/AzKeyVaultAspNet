# Introduction 
Demo para recuperar datos de Azure Key Vault

# Getting Started
TODO: Guide users through getting your code up and running on their own system. In this section you can talk about:
1.	Crear un __Azure_Key_Vault__ por defecto (standard o premium)
	1. az keyvault create --name "demokeyvault0x09122018" --resource-group "DemoAzKV" --location "East US"
2.	Crear un secreto en el __Key_Vault__ creado en paso __1__
	1. az keyvault secret set --vault-name "demokeyvault0x09122018" --name "DemoSecret01" --value "Top Secret Data 0x20195030"
3.	Crear un Web Site MVC en ASP .NET tradicional o Core
4.	Instalar estos dos __NuGet__: 
	1. https://www.nuget.org/packages/Microsoft.Azure.Services.AppAuthentication
	2. https://www.nuget.org/packages/Microsoft.Azure.KeyVault
5. Usar este código para recuperar el secreto creado en el paso __2__
```
AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();
KeyVaultClient keyVaultClient = new KeyVaultClient(
									new KeyVaultClient.AuthenticationCallback(
										azureServiceTokenProvider.KeyVaultTokenCallback));

var secret = await keyVaultClient
	.GetSecretAsync("https://demokeyvault0x09122018.vault.azure.net/secrets/DemoSecret01")
	.ConfigureAwait(false);

return secret.Value;
```
6. Deployar el web site a un __Azure_App_Services__ (ej: __demoazkv0x2019__ )
7. Crear un __Managed_Identity__ de AAD para el app service creado en el paso __6__
	1. az webapp identity assign --name "demoazkv0x2019" --resource-group "DemoAzKV"
8. Dar permisos al Managed Identity del paso __7__ para listar y obtener los secretos
	1. az keyvault set-policy --name 'demokeyvault0x09122018' --object-id '<GUID DEL PRINCIPAL ID>' --secret-permissions get list
9. Probar la recuperación del secreto creado en el paso __2__ en nuestra app

# Build and Test
TODO: Describe and show how to build your code and run the tests. 

# Contribute
TODO: Explain how other users and developers can contribute to make your code better. 

If you want to learn more about creating good readme files then refer the following [guidelines](https://docs.microsoft.com/en-us/azure/devops/repos/git/create-a-readme?view=azure-devops). You can also seek inspiration from the below readme files:
- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
- [Chakra Core](https://github.com/Microsoft/ChakraCore)
