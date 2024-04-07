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
        /// <summary>
        /// Returns extention of file with dot
        /// </summary>
        public string Extention
        {
            get
            {
                return System.IO.Path.GetExtension(Name);
            }
        }
        /// <summary>
        /// Returns icon name for file extention
        /// </summary>
        public string? IconName
        {
            get
            {
                string name = Extention.Substring(1);
                if(!File.Exists($"wwwroot/images/exts/{name}-icon.png"))
                {
                    return null;
                }
                return $"{name}-icon.png";
            }
        }
    }
}
