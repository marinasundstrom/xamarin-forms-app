using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ShellApp.Web.Services
{
    public interface IImageUploader
    {
        Task<string> UploadImageAsync(string name, Stream stream, CancellationToken cancellationToken);
    }
}