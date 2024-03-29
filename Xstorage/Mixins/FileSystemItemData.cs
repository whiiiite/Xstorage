using Xstorage.Extentions;
using Xstorage.Shared;

namespace Xstorage.Mixins
{
    public class FileSystemItemData
    {
        public required string Name { get; set; }
        public required string Path { get; set; }
        public required bool IsDirectory { get; set; }   
        public required DateTime LastEditDate { get; set; }  
        public required long Size { get; set; }
        public string SizeInString
        {
            get
            {
                if (IsDirectory) return string.Empty;

                return Size.BytesLengthToString();
            }
        }
        public string LastEditTimeAgoString
        {
            get
            {
                return DateTime.Now.Subtract(LastEditDate).TimeAgoToString();
            }
        }
    }
}
