using Azure;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Domain.Repositories;
using Microsoft.Extensions.Configuration;

namespace Persistance.Repositories;

public class BlobStorageRepository : IBlobStorageRepository
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly IConfiguration _configuration;

    public BlobStorageRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        string azureStorageConnectionString = configuration["AzureStorage:connectionString"];
        _blobServiceClient = new BlobServiceClient(azureStorageConnectionString);
    }

    public async Task<bool> UploadAsync(string blobName, Stream content)
    {
        try
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(_configuration["AzureStorage:containerName"]);
            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            Response<BlobContentInfo> response = await blobClient.UploadAsync(content, overwrite: true);

            return response.GetRawResponse().Status == 201;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<Stream> DownloadFileAsync(string blobName)
    {
        BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(_configuration["AzureStorage:containerName"]);
        BlobClient blobClient = containerClient.GetBlobClient(blobName);

        BlobDownloadInfo download = await blobClient.DownloadAsync();
        return download.Content;
    }

    public async Task DeleteFileAsync(string blobName)
    {
        BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(_configuration["AzureStorage:containerName"]);
        BlobClient blobClient = containerClient.GetBlobClient(blobName);

        await blobClient.DeleteIfExistsAsync();
    }
}