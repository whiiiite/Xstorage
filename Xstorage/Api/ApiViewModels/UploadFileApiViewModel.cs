using Microsoft.Identity.Client;

namespace Xstorage.Api.ApiViewModels
{
    public class UploadFileApiViewModel
    {
        public string StorageId { get; set; }
        public string Path { get; set; }
        public IFormFile File { get; set; }
    }
}
