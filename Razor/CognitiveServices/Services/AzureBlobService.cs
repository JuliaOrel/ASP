using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CognitiveServices.Services
{
    public class AzureBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        public BlobContainerClient BlobContainerClient { get; private set; }
        public AzureBlobService(string azureStorageConnectionString, string containerName)
        {
            _blobServiceClient = new BlobServiceClient(azureStorageConnectionString);
            BlobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            BlobContainerClient.CreateIfNotExists();
        }
        public BlobClient GetBlobClient ( string blobName )
        {
            return BlobContainerClient.GetBlobClient(blobName);
        }

        //public async Task CreateBlobContainerClientIfNotExists()
        //{
        //    await BlobContainerClient.CreateIfNotExistsAsync();
        //}

    }
}
