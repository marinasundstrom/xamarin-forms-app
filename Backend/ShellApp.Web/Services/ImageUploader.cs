using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using ShellApp.Application.Common.Interfaces;

namespace ShellApp.Web.Services
{
    public class ImageUploader : IImageUploader
    {
        private BlobContainerClient blobContainerClient;
        private readonly IConfiguration configuration;

        public ImageUploader(IConfiguration configuration)
        {
            BlobClientOptions options = new BlobClientOptions(BlobClientOptions.ServiceVersion.V2019_07_07);

            blobContainerClient = new BlobContainerClient(
                configuration.GetConnectionString("Azure:Storage"), "images", options);
            this.configuration = configuration;
        }

        public async Task<string> UploadImageAsync(string name, Stream stream, CancellationToken cancellationToken)
        {
            var response = await blobContainerClient.UploadBlobAsync(name, stream, cancellationToken);
            return CreateBlobUri(name);
        }

        private static string CreateBlobUri(string name)
        {
            // TODO: Define somewhere else
            return $"http://127.0.0.1:10000/devstoreaccount1/images/{name}";
        }
    }
}
