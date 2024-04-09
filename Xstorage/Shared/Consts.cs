namespace Xstorage.Shared
{
    public class Consts
    {
        public const string ASCIIChars = "qwertyuiopasdfghjklzxcvbnmQWERYUIOPASDFGHJKLZXCVBNM1234567890-+=~";
        public const string ASCIILetters = "qwertyuiopasdfghjklzxcvbnmQWERYUIOPASDFGHJKLZXCVBNM";
        public const string ASCIILettersWithDigits = "qwertyuiopasdfghjklzxcvbnmQWERYUIOPASDFGHJKLZXCVBNM1234567890";
        public const string UserFolders = "users_folders";
        public const int MaxApiKeyLength = 132;
        public const long Size_15GB = 16_106_127_360L;
        public const long Size_100GB = 107_374_182_400L;
        public const long Size_250GB = 268_435_456_000L;
        public const long Size_500GB = 536_870_912_000L;
        public static readonly string[] TextFileExtensions = new string[]
        {
            "txt", "log", "md", "cfg", "ini", "json", "xml", "csv", "yaml"
        };
        public static readonly string[] ImageFileExtensions = new string[]
        {
            "jpg", "jpeg", "png", "gif", "bmp", "tiff", "tif", "svg", "webp", "ico"
        };
        public static readonly string[] VideoFileExtensions = new string[]
        {
            "mp4", "avi", "mov", "mkv", "wmv", "flv", "mpeg"
        };
        public static readonly string[] AudioFileExtensions = new string[]
        {
            "jpg", "jpeg", "png", "gif", "bmp", "tiff", "tif", "svg", "webp", "ico"
        };
    }
}
