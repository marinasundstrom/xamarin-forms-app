using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShellApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly BlobContainerClient blobContainerClient;

        public ImagesController(BlobContainerClient blobContainerClient)
        {
            this.blobContainerClient = blobContainerClient;
        }

        [HttpGet("{id}")]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Get(string id)
        {
            var client = blobContainerClient.GetBlobClient(id);

            Console.WriteLine(client.Uri);

            var result = await client.DownloadContentAsync();

            var content = result.Value.Content;
            var details = result.Value.Details;

            Console.WriteLine(details.ContentType);

            return File(content.ToStream(), details.ContentType);
        }
    }
}