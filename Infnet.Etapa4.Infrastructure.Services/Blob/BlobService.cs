using Azure.Storage.Blobs;
using Infnet.Etapa4.Domain.Model.Interfaces.Infrastructure;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Infnet.Etapa4.Infrastructure.Services.Blob
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private const string _container = "imagensamigo";

        public BlobService(string storageAccount)
        {
            _blobServiceClient = new BlobServiceClient(storageAccount);
        }

        public async Task<string> UploadAsync(Stream stream)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_container);

            if (!await containerClient.ExistsAsync())
            {
                await containerClient.CreateIfNotExistsAsync();
                await containerClient.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);
            }

            var blobClient = containerClient.GetBlobClient($"{Guid.NewGuid()}.jpg");
            
            await blobClient.UploadAsync(stream, true);

            return blobClient.Uri.ToString();
        }

        public async Task DeleteAsync(string BlobName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_container);

            var blob = new BlobClient(new Uri(BlobName));

            var blobClient = containerClient.GetBlobClient(blob.Name);

            await blobClient.DeleteIfExistsAsync();
        }


    }
}
