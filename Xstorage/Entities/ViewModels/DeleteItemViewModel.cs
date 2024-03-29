namespace Xstorage.Entities.ViewModels
{
    public class DeleteItemViewModel
    {
        public required string StorageId { get; set; }
        public required string CurrentPath { get; set; }
        public required string PathOfDelete { get; set; }
        public required bool IsDirectory { get; set; }
    }
}
