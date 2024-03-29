namespace Xstorage.Entities.ViewModels
{
    public class UploadFileViewModel
    {
        public required string StorageId { get; set; }
        public required string Path { get; set; }
        public required string FileName { get; set; }
        public List<IFormFile>? Files { get; set; }
    }
}
