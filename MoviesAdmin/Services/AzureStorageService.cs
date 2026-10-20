using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace MoviesAdmin.Services
{
    public class AzureStorageService : IAzureStorageService
    {
        private readonly IConfiguration _configuration;

        private readonly BlobServiceClient blobServiceClient;
        private string containerName;

        public AzureStorageService(IConfiguration configuration)
        {
            _configuration = configuration; //reads from appsettings

            containerName = _configuration["AzureStorage:ContainerName"] ?? throw new InvalidOperationException("Missing Configuration: AzureStorage ContainerName");

            string connectionString = _configuration.GetConnectionString("AzureStorage") ?? throw new InvalidOperationException("Missing Configuration: AzureStorage Connection String");

            blobServiceClient = new BlobServiceClient(connectionString);
        }
        public async Task<string> UploadFileAsync(IFormFile file, string fileName)
            {
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            var blobClient = containerClient.GetBlobClient(fileName);

            var options = new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = file.ContentType
                }
            };

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, options);
            }

            return blobClient.Uri.ToString();
        }
    }
}

